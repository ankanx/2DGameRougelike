using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class SaveDataItem : MonoBehaviour {

    public Text SaveName;
    public Text CharacterName;
    public Text SaveTime;
    public Button ConnectButton;

    public void SetData(string _SaveName, DateTime _Time, string _CharacterName, UnityAction callback)
    {
        SaveName.text = _SaveName;
        SaveTime.text = _Time.ToString();
        CharacterName.text = _CharacterName;
        ConnectButton.onClick.AddListener(callback);
    }
}


