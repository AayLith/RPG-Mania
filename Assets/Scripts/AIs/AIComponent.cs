using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public abstract class AIComponent : ScriptableObject
    {
        public abstract List<Tile> affinity ( List<Tile> tiles, Monster c , int want , Ability abi );
    }
