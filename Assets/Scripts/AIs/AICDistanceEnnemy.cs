using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu ( fileName = "Ai Distance Ennemy" , menuName = "AI/Distance Ennemy" )]
public class AICDistanceEnnemy : AIComponent
{
    public override List<Tile> affinity ( List<Tile> tiles , Monster c , int want , Ability abi )
    {
        float d;
        // Get the closest opponent
        Monster target = BattleSystem.getEnnemyTeam ( c.team )[ 0 ];
        foreach ( Monster cha in BattleSystem.getEnnemyTeam ( c.team ) )
        {
            if ( Point.distance ( cha.tile , c.tile ) < Point.distance ( c.tile , target.tile ) )
                target = cha;
        }

        foreach ( Tile t in tiles )
        {
            d = 0;
            d = Mathf.Max ( d , Point.distance ( target.tile , t ) );
            t.attractiveness += want * ( int ) ( d );
        }
        return tiles;
    }
}
