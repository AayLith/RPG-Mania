using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Character : MonoBehaviour
    {
        [Space ( 20 )]
        [Header ( "Character" )]
        [Header ( "Infos" )]
        public Sprite sprite;
        public string description;

        [Header ( "Stats" )]
        public int maxMana;
        public int curMana;
        public int maxStamina;
        public int curStamina;
        public int move;
        public int moveLeft;
        public MoveAbility moveAbility;
        public Ability weaponAbility;
        public Ability defaultWeaponAbility;
        public List<Ability> abilities = new List<Ability> ();

        [Header ( "Battle" )]
        [SerializeField] SpriteRenderer primaryCircle;
        [SerializeField] SpriteRenderer secondaryCircle;
        [HideInInspector] public bool primaryAvailable { get; protected set; } = true;

        // [Header ( "Display" )]

        private void Awake ()
        {

        }
    }
