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

    public void StartLocalHost()
    {
        manager.StartHost();
    }

    public void JoinGame()
    {
        NetworkManager.singleton.networkPort = 7777;
        NetworkManager.singleton.networkAddress = "127.0.0.1";
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
        mainmenu.transform.Find("Start Host Button").GetComponent<Button>().onClick.AddListener(StartLocalHost);

        mainmenu.transform.Find("Join Game Button").GetComponent<Button>().onClick.RemoveAllListeners();
        mainmenu.transform.Find("Join Game Button").GetComponent<Button>().onClick.AddListener(JoinGame);
    }

    public void SetupPlayerMenuButtons()
    {
        if (!NetworkManager.singleton.IsClientConnected())
        {
            Debug.Log("I am the host");
            mainmenu.transform.Find("Return Button").GetComponent<Button>().onClick.RemoveAllListeners();
            mainmenu.transform.Find("Return Button").GetComponent<Button>().onClick.AddListener(NetworkManager.singleton.StopHost);
        }else
        {
            Debug.Log("I not am the host");
            mainmenu.transform.Find("Return Button").GetComponent<Button>().onClick.RemoveAllListeners();
            mainmenu.transform.Find("Return Button").GetComponent<Button>().onClick.AddListener(NetworkManager.singleton.StopClient);
        }
        mainmenu.transform.Find("Exit Button").GetComponent<Button>().onClick.RemoveAllListeners();
        mainmenu.transform.Find("Exit Button").GetComponent<Button>().onClick.AddListener(ExitApplication);
    }

}
