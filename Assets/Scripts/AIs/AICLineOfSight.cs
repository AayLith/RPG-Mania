using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu ( fileName = "Ai Line of Sight" , menuName = "AI/Line of Sight" )]
    public class AICLineOfSight : AIComponent
    {
        public override List<Tile> affinity ( List<Tile> tiles , Monster c , int want , Ability abi )
        {
            // foreach (tile t in tiles
            // if (astar(c.pos, t).count > Point.distance (c.pod, t.pos)) 
            // t.affinity -= want
            // else t.affinity += want
            return tiles;
        }
    }
