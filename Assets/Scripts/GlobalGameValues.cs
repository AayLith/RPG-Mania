using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public static class GlobalGameValues
    {
        public static float moveSpeed = 3;

        public enum triggerTime { StartOfTurn, EndOfTurn, Spawn, Death, takeDamage }

        public enum Team { player, ally, enemy, neutral, obtacle }

        public static Color getTeamColor ( Team team )
        {
            switch ( team )
            {
                case Team.player:
                    return Color.green;
                case Team.enemy:
                    return Color.red;
                case Team.ally:
                    return Color.blue;
                case Team.neutral:
                    return Color.grey;
            }
            return Color.white;
        }
    }