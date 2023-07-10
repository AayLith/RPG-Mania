using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceFace : MonoBehaviour
{
    public Sprite sprite;
    public int value;
    public modes mode;
    public enum modes { add,substract,multiply,divide,special}
}
