using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu ( fileName = "Ai Closeness to Ennemy" , menuName = "AI/Closeness to Ennemy" )]
public class AICCloseToEnnemy : AIComponent
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

        int dist = Point.distance ( target.tile , c.tile );
        foreach ( Tile t in tiles )
        {
            d = 0;
            d = Mathf.Max ( d , Point.distance ( target.tile , t ) );
            t.attractiveness += want * ( dist - ( int ) ( d ) );
        }
        return tiles;
    }
}
