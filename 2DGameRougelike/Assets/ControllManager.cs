using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);	
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public void DisplayControllerChange()
    {
        string[] names = Input.GetJoystickNames();
        foreach (var controller in names)
        {
            Debug.Log(controller);
        }
    }
}
