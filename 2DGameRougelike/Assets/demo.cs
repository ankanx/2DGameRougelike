using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class demo : MonoBehaviour {

    public string myname  = "Ankan";
    public static demo Instance { get; set; }
    private GameObject mainmenu;
    private InputField playernameinput;
    // Use this for initialization
    void Start () {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
        mainmenu = GameObject.FindGameObjectWithTag("MainMenu");
        playernameinput = mainmenu.transform.Find("PlayerName").GetComponent<InputField>();

    }

    private void OnLevelWasLoaded(int level)
    {
        mainmenu = GameObject.FindGameObjectWithTag("MainMenu");
        playernameinput = mainmenu.transform.Find("PlayerName").GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update () {
        
        if(playernameinput.text != "")
        {
            myname = playernameinput.text;
        }

    }
}
