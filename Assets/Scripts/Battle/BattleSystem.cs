using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class BattleSystem : MonoBehaviour
{
    public static BattleSystem instance;
    public bool DEBUG = true;
    public Room DEBUGMap;

    [Header ( "Objects" )]
    public Canvas battleCanvas;
    public GameObject leftLimit;
    public GameObject rightLimit;
    public GameObject tileHighlight;
    public GameObject placementPhaseHelp;

    [Header ( "Prefabs" )]
    public GameObject abilityLayoutPrefab;
    public DiceCase diceCasePrefab;
    public DestinyDiceCase destinyDiceCasePrefab;

    [Header ( "Participants" )]
    public Room activeMap;
    public List<Monster> monsters = new List<Monster> ();
    public List<Monster> ennemies = new List<Monster> ();
    public List<DestinyDiceCase> destinyDices = new List<DestinyDiceCase> ();

    Dictionary<Monster , GameObject> abilityLayouts = new Dictionary<Monster , GameObject> ();

    private void Awake ()
    {
        instance = this;
    }

    private void Update ()
    {
        // Place les layouts au dessus des monstres associés
        foreach ( var m in abilityLayouts )
            m.Value.transform.position = Camera.main.WorldToScreenPoint ( m.Key.transform.position ) + new Vector3 ( 0 , m.Key.abilitiesOffset , 0 );

        StartCoroutine ( moveTileHighlight () );
    }

    IEnumerator moveTileHighlight ()
    {
        while ( true )
        {
            if ( Board.currentTile == null )
            {
                tileHighlight.SetActive ( false );
                while ( Board.currentTile == null )
                    yield return null;
            }
            else
            {
                tileHighlight.SetActive ( true );
                while ( Board.currentTile != null )
                {
                    tileHighlight.transform.position = Board.currentTile.transform.position;
                    yield return null;
                }
            }
        }
    }

    private void OnEnable ()
    {
        if ( DEBUG )
        {
            List<Monster> playerUnits = new List<Monster> ( monsters );
            List<Monster> ennemyUnits = new List<Monster> ( ennemies );
            startBattle ( playerUnits , ennemyUnits , DEBUGMap );
            DEBUGMap.gameObject.SetActive ( false );
        }
    }

    // Instancier la map
    // Instancier les combattant et leur assigner leur case
    public void startBattle ( List<Monster> player , List<Monster> opponent , Room map )
    {
        activeMap = Instantiate ( map.gameObject ).GetComponent<Room> ();
        activeMap.gameObject.SetActive ( true );
        leftLimit.SetActive ( true );
        leftLimit.transform.position = new Vector3 ( activeMap.leftLimit - 0.5f , 0 , 0 );
        rightLimit.SetActive ( true );
        rightLimit.transform.position = new Vector3 ( activeMap.rightLimit + 0.5f , 0 , 0 );
        monsters.Clear ();
        ennemies.Clear ();

        foreach ( Monster m in player )
        {
            monsters.Add ( m );
            m.team = GlobalGameValues.Team.player;
            placeMonster ( m );
        }
        foreach ( Monster m in opponent )
        {
            ennemies.Add ( m );
            m.team = GlobalGameValues.Team.enemy;
            placeMonster ( m );
        }
        StartCoroutine ( placementPhase () );
    }

    void placeMonster ( Monster m )
    {
        int minPos;
        Tile t;

        if ( m.team == GlobalGameValues.Team.player )
            minPos = activeMap.leftLimit - m.preferedStartingPos - 1;
        else
            minPos = activeMap.rightLimit + m.preferedStartingPos + 1;

        if ( m.team == GlobalGameValues.Team.player )
            t = Board.instance.getTile ( Random.Range (
                Mathf.Max ( minPos - 2 , Board.leftBound ) , Mathf.Max ( minPos + 1 , Board.leftBound ) ) ,
                Random.Range ( Board.topBound , Board.bottomBound ) );
        else
            t = Board.instance.getTile ( Random.Range (
                 Mathf.Min ( minPos , Board.rightBound ) , Mathf.Min ( minPos + 3 , Board.rightBound ) ) ,
                 Random.Range ( Board.topBound , Board.bottomBound ) );

        if ( t != null && t.content == null )
            Board.setTile ( m , t );
        else
            placeMonster ( m );
    }

    IEnumerator placementPhase ()
    {
        placementPhaseHelp.SetActive ( true );
        while ( true )
        {
            // if clic, start dragging
            if ( Input.GetAxis ( "Fire1" ) != 0 && Board.currentTile?.content?.team == GlobalGameValues.Team.player )
            {
                Monster m = Board.currentTile.content;
                while ( Input.GetAxis ( "Fire1" ) != 0 )
                {
                    m.transform.position = Board.mouseWorldPos + new Vector3 ( 0 , 0.25f , 0 );
                    m.spriteRenderer.transform.rotation = Quaternion.Euler ( new Vector3 (
                        m.spriteRenderer.transform.rotation.eulerAngles.x ,
                        m.spriteRenderer.transform.rotation.eulerAngles.y ,
                        Mathf.Sin ( Time.time * 20 ) * 25 ) );
                    yield return null;
                }
                if ( Board.currentTile?.content == null && Board.currentTile?.pos.x < activeMap.leftLimit )
                    Board.setTile ( m , Board.currentTile );
                else
                    m.transform.position = m.tile.transform.position;
                m.spriteRenderer.transform.rotation = Quaternion.Euler ( new Vector3 ( m.spriteRenderer.transform.rotation.eulerAngles.x , m.spriteRenderer.transform.rotation.eulerAngles.y , 0 ) );
            }
            yield return null;
        }
    }

    public void endPlacementPhase ()
    {
        StopAllCoroutines ();
        leftLimit.SetActive ( false );
        rightLimit.SetActive ( false );
        placementPhaseHelp.SetActive ( value: false );
        StartCoroutine ( movementPhase () );
    }

    int movingUnits;
    IEnumerator movementPhase ()
    {
        yield return new WaitForSeconds ( 1 );
        Debug.Log ( "Starting movement phase" );
        foreach ( Monster m in monsters )
            StartCoroutine ( movement ( m ) );
        foreach ( Monster m in ennemies )
            StartCoroutine ( movement ( m ) );
        yield return null;
        while ( movingUnits > 0 )
            yield return null;
        Debug.Log ( "Ending movement phase" );
    }

    IEnumerator movement ( Monster m )
    {
        movingUnits++;
        m.moveLeft = m.movement;

        while ( m.moveLeft > 0 )
        {
            // Obtenir toutes les cases à portée de déplacement
            List<Tile> tiles = Board.instance.getTilesInRange ( m.tile , m.moveLeft , true , true , true );

            // Déterminer l'attractivité des cases sur laquelle le monstre veut aller
            foreach ( Tile t in tiles )
                t.attractiveness = 0;
            foreach ( AIBrain.component c in m.brain.moveBefore )
                tiles = c.aic.affinity ( tiles , m , c.want , null );

            // Find the best Tile
            Tile bestTile = tiles[ 0 ];
            foreach ( Tile t in tiles )
                if ( t.attractiveness > bestTile.attractiveness )
                    bestTile = t;

            // Si on est déjà sur la bonne case, fin du mouvement
            if ( bestTile == m.tile )
                break;
            // Sinon déplacement d'une case
            yield return StartCoroutine ( moveUnit ( m , Board.instance.ASTARSEARCH ( m.tile , bestTile , 1 ) ) );


            yield return null;
        }

        movingUnits--;
    }

    IEnumerator moveUnit ( Monster unit , List<Tile> tiles )
    {
        unit.moveLeft -= ( tiles.Count - 1 );
        for ( int i = 0 ; i < tiles.Count ; i++ )
        {
            // Orient the sprite in the right direction
            if ( tiles[ i ].transform.position.x > unit.transform.position.x )
                unit.spriteRenderer.transform.localScale = new Vector3 ( Mathf.Abs ( unit.spriteRenderer.transform.localScale.x ) , unit.spriteRenderer.transform.localScale.y , unit.spriteRenderer.transform.localScale.z );
            else if ( tiles[ i ].transform.position.x < unit.transform.position.x )
                unit.spriteRenderer.transform.localScale = new Vector3 ( Mathf.Abs ( unit.spriteRenderer.transform.localScale.x ) * -1 , unit.spriteRenderer.transform.localScale.y , unit.spriteRenderer.transform.localScale.z );

            // Move the character
            while ( Vector3.Distance ( unit.transform.position , tiles[ i ].transform.position ) > 0.05f )
            {
                unit.transform.position += ( tiles[ i ].transform.position - unit.transform.position ).normalized * GlobalGameValues.moveSpeed * Time.deltaTime;
                unit.spriteRenderer.transform.localPosition = new Vector3 ( 0 , 1 , 0 ) * Mathf.Abs ( Mathf.Sin ( Time.time * unit.movement * unit.movement ) ) / 3;
                yield return null;
            }

            // End the movement
            unit.transform.position = tiles[ i ].transform.position;
            unit.spriteRenderer.transform.localPosition = Vector3.zero;
            unit.tile.setContent ( null );
            unit.tile = tiles[ i ];
            unit.tile.setContent ( unit );
            yield return null;
        }
    }

    public void targetingPhase ()
    {
    }

    public void playerPhase ()
    {
    }

    public void abilityPhase ()
    {

    }

    void startOfTurn ()
    {
        // Récupérer la liste des Abilities
        Dictionary<Monster , List<Ability>> abilities = new Dictionary<Monster , List<Ability>> ();
        foreach ( Monster m in monsters )
        {
            abilities.Add ( m , new List<Ability> () );
            foreach ( Ability a in m.abilities )
                abilities[ m ].Add ( a );
        }
        foreach ( Monster m in ennemies )
        {
            abilities.Add ( m , new List<Ability> () );
            foreach ( Ability a in m.abilities )
                abilities[ m ].Add ( a );
        }

        float length = 1;
        // Instancier les layouts
        foreach ( var m in abilities )
        {
            GameObject go = Instantiate ( abilityLayoutPrefab , battleCanvas.transform );
            abilityLayouts.Add ( m.Key , go );

            // Instancier les Ability
            foreach ( Ability a in m.Value )
            {
                DiceCase diceCase = Instantiate ( diceCasePrefab , go.transform );
                StartCoroutine ( diceRolling ( a.dice , diceCase , length ) );
                length += 0.1f;
            }
        }

        foreach ( DestinyDiceCase d in destinyDices )
        {
            StartCoroutine ( destinyDiceRolling ( d.dice , d , length ) );
            length += 0.1f;
        }
    }

    public void displayFace ( Dice dice , DiceCase dcase , DiceFace face )
    {
        dcase.dice = dice;
        dcase.amount = face.value;
        dcase.face = face;
        dcase.amountDisplay.text = face.value > 0 ? "" + face.value : "";

        if ( face.effect != null )
        {
            dcase.effect = face.effect;
            dcase.spriteDisplay.sprite = face.effect.sprite;
            dcase.spriteDisplay.color = face.effect.color;
        }
        else
        {
            dcase.spriteDisplay.color = new Color ( 0 , 0 , 0 , 0 );
        }
    }

    IEnumerator diceRolling ( Dice dice , DiceCase dcase , float length )
    {
        int i = 0;
        while ( length > 0 )
        {
            i++;
            displayFace ( dice , dcase , dice.faces[ i % dice.faces.Length ] );
            length -= 0.1f;
            yield return new WaitForSeconds ( 0.1f );
        }
        displayFace ( dice , dcase , dice.faces[ Random.Range ( 0 , dice.faces.Length ) ] );
    }

    IEnumerator destinyDiceRolling ( Dice dice , DestinyDiceCase dcase , float length )
    {
        int i = 0;
        while ( length > 0 )
        {
            i++;
            destinyDisplayFace ( dice , dcase , dice.faces[ i % dice.faces.Length ] );
            length -= 0.1f;
            yield return new WaitForSeconds ( 0.1f );
        }
        destinyDisplayFace ( dice , dcase , dice.faces[ Random.Range ( 0 , dice.faces.Length ) ] );
    }

    public void destinyDisplayFace ( Dice dice , DestinyDiceCase dcase , DiceFace face )
    {
        dcase.dice = dice;
        dcase.amount = face.value;
        dcase.face = face;

        switch ( face.mode )
        {
            case DiceFace.modes.add:
                dcase.amountDisplay.text = face.value > 0 ? "+" + face.value : "";
                break;
            case DiceFace.modes.multiply:
                dcase.amountDisplay.text = face.value > 0 ? "x" + face.value : "";
                break;
            case DiceFace.modes.divide:
                dcase.amountDisplay.text = face.value > 0 ? "/" + face.value : "";
                break;
            case DiceFace.modes.substract:
                dcase.amountDisplay.text = face.value > 0 ? "-" + face.value : "";
                break;
            default:
                dcase.amountDisplay.text = face.value > 0 ? "" + face.value : "";
                break;
        }
    }

    public static List<Monster> getTeam ( GlobalGameValues.Team team )
    {
        switch ( team )
        {
            case GlobalGameValues.Team.player:
                return instance.monsters;
            case GlobalGameValues.Team.enemy:
                return instance.ennemies;
        }
        return new List<Monster> ();
    }

    public static List<Monster> getEnnemyTeam ( GlobalGameValues.Team team )
    {
        switch ( team )
        {
            case GlobalGameValues.Team.enemy:
                return instance.monsters;
            case GlobalGameValues.Team.player:
                return instance.ennemies;
        }
        return new List<Monster> ();
    }
}
