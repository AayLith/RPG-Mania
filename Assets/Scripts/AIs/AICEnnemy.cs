using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu ( fileName = "Ai Contains Ennemy" , menuName = "AI/Contains Ennemy" )]
public class AICEnnemy : AIComponent
{
    public override List<Tile> affinity ( List<Tile> tiles , Monster c , int want , Ability abi )
    {
        foreach ( Tile t in tiles )
            if ( t.content != null && t.content.team != c.team )
                t.attractiveness += want;
        return tiles;
    }
}
