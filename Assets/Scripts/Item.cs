using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu ( fileName = "new Item" , menuName = "Item" )]
public class Item : ScriptableObject
{
    public string _name;
    public Sprite sprite;
    public Color color = Color.white;
    public string description;
}
