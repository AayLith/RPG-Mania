using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu ( fileName = "Ai Distance" , menuName = "AI/Distance" )]
    public class AICDistance : AIComponent
    {
        public override List<Tile> affinity ( List<Tile> tiles , Monster c , int want , Ability abi )
        {
            Tile start = c.tile;
            foreach ( Tile t in tiles )
                t.attractiveness += want * Point.distance ( start , t );
            return tiles;
        }
    }
