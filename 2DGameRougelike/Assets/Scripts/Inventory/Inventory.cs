using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    GameObject slotPanel;
    ItemDatabase database;
    public GameObject inventorySlot;
    public GameObject inventoryItem;

    // Fix
    int slotAmount;

    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

    private void Start()
    {

        database = GetComponent<ItemDatabase>();

        slotAmount = 20;
        slotPanel = GameObject.FindGameObjectWithTag("InventoryUi");

        //replace plz
        for(int i =0; i< slotAmount; i++)
        {
            items.Add(new Item());
            slots.Add(Instantiate(inventorySlot));
            slots[i].transform.SetParent(slotPanel.transform,false);
        }

        

    }

    public void AddItem(string objectSlug)
    {
        Item itemToAdd = database.GetItem(objectSlug);
        Debug.Log(itemToAdd.ItemName);
        for(int i = 0; i < items.Count; i++)
        {
            if (items[i].ObjectSlug == "new")
            {
                items[i] = itemToAdd;
                GameObject itemObj = Instantiate(inventoryItem);
                itemObj.transform.SetParent(slots[i].transform);
                itemObj.transform.localPosition = new Vector2(25 ,-25);
                break;
            }
        }
    }

}
