using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    public Monster currentTarget;
    public Monster currentCaster;
    public int currentValue;
    public Sprite sprite;
    public Color color = Color.white;

    public abstract void apply ( Monster target , Monster caster , int value );

    public abstract void remove ( Monster target , Monster caster , int value );

    public abstract void update ( Monster target , Monster caster , int increment );
}
