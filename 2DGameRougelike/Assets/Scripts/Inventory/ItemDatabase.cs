using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class ItemDatabase : MonoBehaviour {
    public static ItemDatabase Instance {get; set;}
    private List<Item> Items { get; set; }
	// Use this for initialization
	void Start () {
		if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }else
        {
            Instance = this;
        }

        BuildDatabase();
	}
	
    private void BuildDatabase()
    {
        Items = JsonConvert.DeserializeObject<List<Item>>(Resources.Load<TextAsset>("Db/Items").ToString());
        Debug.Log(Items[0].Stats[1].StatName + " Level is " + Items[0].Stats[1].GetCalculatedStatValue());
        Debug.Log(Items[0].ItemName);
    }


    // 
    public Item GetItem(string itemSlug)
    {
        foreach(Item item in Items){
            if(item.ObjectSlug == itemSlug)
            {
                return item;
            }
        }
        Debug.Log("Warning could not found item" + itemSlug);
        return null;
    }
}
