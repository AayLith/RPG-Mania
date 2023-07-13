using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DestinyDiceCase : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public static DiceCase currentDice = null;

    public Dice dice;
    public DiceFace face;
    public int amount;
    public TMP_Text amountDisplay;
    public Image dicefaceDisplay;

    Vector2 startPos;
    bool beingDragged = false;

    public void OnBeginDrag ( PointerEventData eventData )
    {
        startPos = transform.position;
        beingDragged = true;
        transform.localScale = Vector3.one / 2;
    }

    public void OnEndDrag ( PointerEventData eventData )
    {
        beingDragged = false;
        if ( currentDice == null || currentDice.amount == 0 )
        {
            cancelDrag ();
            return;
        }

        switch ( face.mode )
        {
            case DiceFace.modes.add:
                currentDice.amount += face.value;
                break;
            case DiceFace.modes.substract:
                currentDice.amount -= face.value;
                break;
            case DiceFace.modes.divide:
                currentDice.amount /= face.value;
                break;
            case DiceFace.modes.multiply:
                currentDice.amount *= face.value;
                break;
        }
        currentDice.amountDisplay.text = "" + currentDice.amount;
        Destroy ( gameObject );
    }

    void cancelDrag ()
    {
        transform.position = startPos;
        transform.localScale = Vector3.one;
    }

    public void OnDrag ( PointerEventData eventData )
    {
        if ( !beingDragged )
            return;
        transform.position = Input.mousePosition;
    }
}
