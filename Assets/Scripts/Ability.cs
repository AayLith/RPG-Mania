using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu ( fileName = "New Ability" , menuName = "Ability" )]
public class Ability : ScriptableObject
{
    public string _name;
    public string description;

    [Header ( "Stats" )]
    public bool priority = false;
    public targetingModes targetingMode;
    public int range = 1;
    public int aoeSize = 1;
    public bool aoeLos = true;
    public bool hasLineOfSight = true;
    public bool dontAddIfOccupied;
    public bool stopSearchIfOccupied;

    [Header ( header: "Dice" )]
    public Dice dice;

    public enum targetingModes { ennemy, ally, self }

    public virtual void init ( Character c )
    {

    }

    public virtual void end ()
    {

    }

    public virtual List<Tile> getTilesInRange ( Tile start )
    {
        List<Tile> tiles = Board.instance.getTilesInRange ( start , range , dontAddIfOccupied , stopSearchIfOccupied , true );
        tiles.RemoveAt ( 0 );
        return tiles;
    }

    public virtual List<Tile> getTargetableTiles ( List<Tile> tiles , Monster origin )
    {
        return tiles;
    }

    protected virtual IEnumerator execution ( Monster origin , List<Tile> tiles )
    {
        yield return null;
    }
}
