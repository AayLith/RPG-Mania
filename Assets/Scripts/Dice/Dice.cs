using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu ( fileName = "New Dice" , menuName = "Dice" )]
[System.Serializable]
public class Dice
{
    public string _name;
    public Sprite diceSprite;
    public DiceFace[] faces;
}
