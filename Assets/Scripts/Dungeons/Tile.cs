using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Point pos { get; private set; }

    [Header ( "Params" )]
    public bool isEnabled;
    public int moveCost = 1;
    public bool walkable = true;
    public TerrainType.terrainTypes terrain;

    [Header ( "Objects" )]
    public SpriteRenderer tileSprite;
    public Monster content;
    public TMPro.TMP_Text debugText;

    public int attractiveness // Used by AI's to choose where to move
    {
        get
        { return _attractiveness; }
        set
        {
            _attractiveness = value; debugText.text = "" + value;
        }
    }
    int _attractiveness;

    private void Awake ()
    {
        pos = new Point ( transform.position );
        name = "Tile " + pos.x + ',' + pos.y;
        setColor ();
        debugText.enabled = Board.instance.debugMode;
        Board.instance.addTile ( this );
        if ( Board.instance.debugMode )
            debugText.text = "" + pos.x + ',' + pos.y;
    }

    private void OnDisable ()
    {
        Board.instance.removeTile ( this );
    }

    public void setContent ( Monster d )
    {
        content = d;
        walkable = content == null;
    }

    public void setTerrain ( TerrainType.terrainTypes tt )
    {
        terrain = tt;
        tileSprite.color = TerrainType.getColor ( tt );
    }

    public bool canMoveThrough ( Board.moveTypes t )
    {
        List<Board.moveTypes> list = TerrainType.getAllowedMoveTypes ( terrain );
        if ( content != null )
            foreach ( Board.moveTypes mt in content.revokedMoveTypes )
                try { list.Remove ( mt ); }
                catch { }

        return list.Contains ( t );
    }

    /// <summary>
    /// Enable or Disable the Tile based on it's current state
    /// </summary>
    public void switchTile ()
    {
        if ( isEnabled )
            disable ();
        else
            enable ();

#if UNITY_EDITOR
        UnityEditor.PrefabUtility.RecordPrefabInstancePropertyModifications ( this );
#endif
    }

    /// <summary>
    /// Set the Pos of this Tile
    /// </summary>
    /// <param name="p"></param>
    public void setPos ( Point p )
    {
        pos = p;
        transform.position = DungeonParams.pointY ? new Vector3 ( p.x , p.y , 0 ) : new Vector3 ( p.x , 0 , p.y );
        setColor ();
    }

    public void enable ()
    {
        isEnabled = true;
        setColor ();
    }

    public void disable ()
    {
        isEnabled = false;
        setColor ();
    }

    void setColor ()
    {
        tileSprite.color *= isEnabled ? DungeonParams.tileOnColor : DungeonParams.tileOffColor;
    }
}
