using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu ( fileName = "Ai Mêlée" , menuName = "AI/Mêlée" )]
    public class AICMelee : AIComponent
    {
        public override List<Tile> affinity ( List<Tile> tiles , Monster c , int want , Ability abi )
        {
            Tile next;
            foreach ( Tile t in tiles )
                foreach ( Point p in Point.dirs )
                {
                    next = Board.instance.getTile ( t.pos + p );
                    if ( next == null || next.content == null )
                        continue;
                    if ( next.content.team != c.team )
                    {
                        t.attractiveness += want;
                        continue;
                    }
                }
            return tiles;
        }
    }
