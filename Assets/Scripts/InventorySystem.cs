using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventorySystem : Menu
{
    public static InventorySystem instance;



    //Dictionnaire contenant les clefs des items de l'inventaire
    Dictionary<Item,int> content = new Dictionary<Item,int>();

    //Button inventory
    public InventoryButton inventoryButtonPrefab;
    public Transform inventoryLayout;

    //liste des boutons
    public Dictionary<Item, InventoryButton> lesBoutons=new Dictionary<Item, InventoryButton>();

    //Crée un liste d'item
    public Item[] leTest;

    //val max de l'inventaire
    int maximum=99;

    //active ou désactive le mode debuggage
    bool DEBUG=true;

    //permet la modification de Titre (TMP)
    public TMP_Text affichageNomObjet;
    //permet la modification de Description (TMP)
    public TMP_Text affichageDescritionObjet;

    //Récupère l'objet cliqué
    public Item selectedItem;

    //popUp de destruction d'objet
    public GameObject popup;
    //input du nombre d'objets a détruire
    public TMP_InputField nbreObjetDetruire;

    //canvas de l'inventaire
    public GameObject canvas;


    private void Awake()
    {
        instance = this;
    }

    public override void updateInput(Dictionary<InputDispatcher.inputs, InputDispatcher.input> inputList)
    {
        throw new System.NotImplementedException();
    }

    public override void fixedUpdateInput()
    {
        throw new System.NotImplementedException();
    }

    public override void open()
    {
        //Ouvre l'inventaire
        canvas.SetActive(true);
        foreach (var button in  lesBoutons)
        {
            Destroy(button.Value.gameObject);
        }
        lesBoutons.Clear();
        foreach (var item in content)
        {
            createItemButton(item.Key, item.Value);
            Debug.Log(item);
        }

    }

    public override void close()
    {
        //Ferme l'inventaire
        canvas.SetActive(false);
        Debug.Log("appel de close");
    }


    // Start is called before the first frame update
    void Start()
    //Sera effectué au lancement du jeu
    //Ajouter les tests
    {
        if (DEBUG){
            foreach (var item in leTest)
            {
                addItem(item, Random.Range(1, 99));
            }
            open();
        }

#if UNITY_EDITOR
#endif

        //Fait spawn les bouton


        popup.SetActive(false);
    }

    // Sera effectué à chaque frame du jeu
    void Update()
    {
        
    }

    private void createItemButton(Item item, int amount)
    {
        InventoryButton leBouton = Instantiate(inventoryButtonPrefab.gameObject, inventoryLayout).GetComponent<InventoryButton>();
        leBouton.sprite.sprite = item.sprite;
        leBouton.sprite.color = item.color;
        leBouton.amount.text = "" + amount;
        leBouton.item = item;
        lesBoutons.Add(item, leBouton);
    }

    // Augmente la quantité de l'item ciblé, retourne le nombre d'objet effectivement ajouté
    int addItem (Item item, int amount)
    {
        //si l'item n'existe pas rajoute la clef et la quantité

        //Si la quantité rajouté est inférieure a 0, ne change rien
        if (amount < 0)
        {
            return 0;
        }
        //Si l'item n'existe pas, ajoute la clef et initalise à 0
        if (!content.ContainsKey(item))
        {
            content.Add(item, 0);
            createItemButton(item, 0);
        }

        
        //Augmente la quantité de l'item
        content[item]+=amount;

        //Si trop d'objets, en rajoute la maximum et renvoit un message décrivant l'ajout
        if (content[item]>maximum){
            int itemAjouter=(content[item]-amount)+maximum;
            content[item] = maximum;
            Debug.Log("Impossible d'avoir plus de "+maximum+" objet d'un même type. "+itemAjouter+"/"+amount+" de "+item.name+" on été ajoutés");    
            return itemAjouter;
        }
        updateItem(item);
        return amount;
    }

    // Réduit la quantité de l'item ciblé, retourne le nombre d'objet effectivement retirés
    int removeItem (Item item, int amount)
    {
        //Si l'objet n'existe dans l'inventaire, ne fait rien, retourne 0
        if (!content.ContainsKey(item)){
            Debug.Log("L'objet n'existe pas dans l'inventaire");
            return 0;
        }
        //Réduit la quantité de l'objet
        content[item]-=amount;

        //Si nombre d'object < 0, set l'objet a 0, renvoit un message détaillant le nombre d'objet effectivement réduit
        if (content[item]<0){
            int detruit=amount+content[item];
            content.Remove(item);
            Debug.Log("Impossible de retirer plus d'objet d'un même type que le joueur n'en possède. "+detruit+" objets on effectivement été détruits");
            updateItem(item);
            return detruit;
        }
        updateItem(item);
        return amount;

    }

    void updateItem(Item item)
    {
        if (content.ContainsKey(item))
        {
            lesBoutons[item].amount.text = "" + content[item];
        }
        else
        {
            Destroy(lesBoutons[item].gameObject);
            lesBoutons.Remove(item);
        }
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
        //affiche le nom et la description dans les onglets prévu a cet effet
        //l'item devra être stocké et si click sur supprimer ouvre un pop up pour demander combien on en supprime
        affichageNomObjet.text = button.item._name;
        affichageDescritionObjet.text = button.item.description;
        selectedItem = button.item;

    }

    public void destroyItem()
    {
        popup.SetActive(true);
        nbreObjetDetruire.text = "0";
    }

    public void annulerSuppression()
    {
        popup.SetActive(false);
    }

    public void confirmerSuppresion()
    {
        int amount = int.Parse(nbreObjetDetruire.text);
        if (amount > 0)
        {
            removeItem(selectedItem, amount);
        }
        popup.SetActive(false );
    }







    //lors de la récupation, si nom=vide de l'objet récupère non du fichier
}
