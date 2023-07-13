using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu ( fileName = "New Ability" , menuName = "Ability" )]
public class Ability : ScriptableObject
{
    public string _name;
    public string description;
    public targetingModes targetingMode;
    public Dice dice;
    public int priority;

    public enum targetingModes { ennemy, ally, self }
}
