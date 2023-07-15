using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DiceFace
{
    public Effect effect;
    public int value;
    public modes mode;
    public enum modes { special, add, substract, multiply, divide }
}
