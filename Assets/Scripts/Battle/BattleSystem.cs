using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public static BattleSystem instance;

    [Header ( "Objects" )]
    public List<Monster> monsters = new List<Monster> ();
    public List<DestinyDiceCase> destinyDices = new List<DestinyDiceCase> ();
    public Canvas battleCanvas;

    [Header ( "Prefabs" )]
    public GameObject abilityLayoutPrefab;
    public DiceCase diceCasePrefab;
    public DestinyDiceCase destinyDiceCasePrefab;

    Dictionary<Monster , GameObject> abilityLayouts = new Dictionary<Monster , GameObject> ();

    private void Update ()
    {
        // Place les layouts au dessus des monstres associés
        foreach ( var m in abilityLayouts )
            m.Value.transform.position = Camera.main.WorldToScreenPoint ( m.Key.transform.position ) + new Vector3 ( 0 , m.Key.abilitiesOffset , 0 );
    }

    private void Start ()
    {
        startOfTurn ();
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

        float length = 2;
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
                length += 0.25f;
            }
        }

        foreach ( DestinyDiceCase d in destinyDices )
        {
            StartCoroutine ( destinyDiceRolling ( d.dice , d , length ) );
            length += 0.25f;
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
}
