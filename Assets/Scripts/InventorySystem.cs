using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem instance;
    private void Awake()
    {
        instance = this;
    }


    Dictionary<Item,int> content = new Dictionary<Item,int>();
    //Button inventory
    public InventoryButton inventoryButtonPrefab;
    public Transform inventoryLayout;
    //liste des boutons
    public List<InventoryButton> lesBoutons;
    //test 1 objet
    public Item[] leTest;

    //val max de l'inventaire
    int maximum=99;
    //active ou désactive le mode debuggage
    bool DEBUG=true;
    // Start is called before the first frame update
    void Start()
    {
        if (DEBUG){
            foreach (var item in leTest)
            {
                addItem(item, Random.Range(2, 200));
            }
        }
        //mettre les tests
#if UNITY_EDITOR
#endif

        //Fait spawn les bouton
        foreach (var item in content)
        {
            InventoryButton leBouton = Instantiate(inventoryButtonPrefab.gameObject, inventoryLayout).GetComponent<InventoryButton>();
            leBouton.sprite.sprite = item.Key.sprite;
            leBouton.sprite.color=item.Key.color;
            leBouton.amount.text=""+item.Value;
            leBouton.item=item.Key;
            Debug.Log(item);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Ajoute un objet à l'inventaire si il n'y est pas présent, sinon augmente sa quantité, retourne le nombre d'objets effectivement ajoutés
    int addItem (Item item, int amount)
    {
        //si l'item n'existe pas rajoute la clef et la quantité
        if (amount < 0)
        {
            return 0;
        }
        if (!content.ContainsKey(item))
            content.Add(item, 0);
        content[item]+=amount;

        if (content[item]>maximum){
            int itemAjouter=(content[item]-amount)+maximum;
            content[item] = maximum;
            Debug.Log("Impossible d'avoir plus de "+maximum+" objet d'un même type. "+itemAjouter+"/"+amount+" de "+item.name+" on été ajoutés");    
            return itemAjouter;
        }

        return amount;
    }

    // Retire une quantité d'un objet de l'inventaire si il y est présent, retourne le nombre d'objets effectivement détruits
    int removeItem (Item item, int amount)
    {
        if (!content.ContainsKey(item)){
            Debug.Log("L'objet n'existe pas dans l'inventaire");
            return 0;
        }

        content[item]-=amount;

        if (content[item]<0){
            int detruit=amount+content[item];
            content[item] = 0;
            Debug.Log("Impossible de retirer plus d'objet d'un même type que le joueur n'en possède. "+detruit+" objets on effectivement été détruits");
            return detruit;
        }

        return amount;

    }

    // Donne la quantité d'un objet dans l'inventaire
    int countItem (Item item)
    {
        if (content.ContainsKey(item))
            return content[item];
        else
            return 0;
    }
    
    public void selectItem(InventoryButton button)
    {
        //fera quelque chose
    }
}
