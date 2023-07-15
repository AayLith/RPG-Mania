using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu ( fileName = "Ai Range" , menuName = "AI/Range" )]
public class AICRange : AIComponent
{
    public override List<Tile> affinity ( List<Tile> tiles , Monster c , int want , Ability abi )
    {
        Tile start = c.tile;
        int range = abi.range;
        foreach ( Tile t in tiles )
        {
            if ( Point.distance ( start , t ) <= range )
                t.attractiveness += want;
        }
        return tiles;
    }
}
