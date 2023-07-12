using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    public Image sprite;
    public TMP_Text amount;
    public Item item;

    public void onClic ()
    {
        InventorySystem.instance.selectItem ( this );
    }
}
