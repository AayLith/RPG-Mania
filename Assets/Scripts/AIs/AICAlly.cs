using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu ( fileName = "Ai Contains Ally" , menuName = "AI/Contains Ally" )]
public class AICAlly : AIComponent
{
    public override List<Tile> affinity ( List<Tile> tiles , Monster c , int want , Ability abi )
    {
        foreach ( Tile t in tiles )
            if ( t.content != null && t.content.team == c.team )
                t.attractiveness += want;
        return tiles;
    }
}
