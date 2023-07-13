using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DiceCase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Dice dice;
    public DiceFace face;
    public Effect effect;
    public int amount;
    public TMP_Text amountDisplay;
    public Image spriteDisplay;
    public Image dicefaceDisplay;

    public void OnPointerEnter ( PointerEventData eventData )
    {
        DestinyDiceCase.currentDice = this;
    }

    public void OnPointerExit ( PointerEventData eventData )
    {
        DestinyDiceCase.currentDice = null;
    }
}
