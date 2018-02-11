using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExpandedNetworkManagerHUD : NetworkManagerHUD {

    private GameObject mainmenu;

    private void Awake()
    {
        mainmenu = GameObject.FindGameObjectWithTag("MainMenu");
        this.manager = this.GetComponent<NetworkManager>();
        SetupMainMenuButtons();

    }

    public static void StartLocalHost()
    {
        NetworkManager.singleton.StartHost();
    }

    public static void JoinGame()
    {
        // seems to be the only way to fix this?
        GameObject mainmenu = GameObject.FindGameObjectWithTag("MainMenu");
        InputField ip = mainmenu.transform.Find("IP").GetComponent<InputField>();
        NetworkManager.singleton.networkPort = 7777;
        
        if (ip.text != "")
        {
            Debug.Log("ex");
            NetworkManager.singleton.networkAddress = ip.text;
        }
        else
        {
            NetworkManager.singleton.networkAddress = "127.0.0.1";
        }
        NetworkManager.singleton.StartClient();
    }

    public void StopLocalHost()
    {
        manager.OnStopHost();
    }

    public void ExitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            manager.StopHost();
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if(level == 0)
        {

            mainmenu = GameObject.FindGameObjectWithTag("MainMenu");
            SetupMainMenuButtons();
        }else
        {
            mainmenu = GameObject.FindGameObjectWithTag("PlayerMenu");
            Debug.Log(mainmenu.name);
            SetupPlayerMenuButtons();
        }


    }


    public void SetupMainMenuButtons()
    {
        mainmenu.transform.Find("Start Host Button").GetComponent<Button>().onClick.RemoveAllListeners();
        mainmenu.transform.Find("Start Host Button").GetComponent<Button>().onClick.AddListener(ExpandedNetworkManagerHUD.StartLocalHost);

        mainmenu.transform.Find("Join Game Button").GetComponent<Button>().onClick.RemoveAllListeners();
        mainmenu.transform.Find("Join Game Button").GetComponent<Button>().onClick.AddListener(ExpandedNetworkManagerHUD.JoinGame);
    }

    public void SetupPlayerMenuButtons()
    {
        if (!NetworkManager.singleton.IsClientConnected())
        {
            Debug.Log("im client");
            mainmenu.transform.Find("Return Button").GetComponent<Button>().onClick.RemoveAllListeners();
            mainmenu.transform.Find("Return Button").GetComponent<Button>().onClick.AddListener(NetworkManager.singleton.StopClient);
        }else
        {
            Debug.Log("im Host");
            mainmenu.transform.Find("Return Button").GetComponent<Button>().onClick.RemoveAllListeners();
            mainmenu.transform.Find("Return Button").GetComponent<Button>().onClick.AddListener(NetworkManager.singleton.StopHost);
        }
        mainmenu.transform.Find("Exit Button").GetComponent<Button>().onClick.RemoveAllListeners();
        mainmenu.transform.Find("Exit Button").GetComponent<Button>().onClick.AddListener(ExitApplication);
    }

}
