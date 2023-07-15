using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [System.Serializable]
    public class AIBrain
    {
        [System.Serializable]
        public struct component
        {
            public AIComponent aic;
            public int want;
        }

        public List<component> moveBefore = new List<component> (); // Position itself to best use it's abilites
        public List<component> targeting = new List<component> (); // Which target is the best target
    }