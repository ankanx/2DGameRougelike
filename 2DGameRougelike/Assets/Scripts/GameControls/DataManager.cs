using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class DataManager : MonoBehaviour {

    public Transform content = null;
    public GameObject SavePrefab = null;

    public List<SaveData> LoadAllExistingSaves()
    {
        List<SaveData> allSaves = new List<SaveData>();
        if (!Directory.Exists(Application.persistentDataPath + "/Saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Saves");
        }
        var info = new DirectoryInfo(Application.persistentDataPath + "/Saves/");
        var fileInfo = info.GetFiles();
        foreach (var file in fileInfo)
        {
            Debug.Log(file.Name.ToString());
            allSaves.Add(Load(Application.persistentDataPath + "/Saves/" + file.Name.ToString()));
        }

        return allSaves;
    }

    public void Save(SaveData data)
    {
        data.LastSave = System.DateTime.Now; 
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Saves/playerInfo.dat");

        bf.Serialize(file, data);
        file.Close();
    }

    public SaveData Load(String save)
    {
        SaveData data = new SaveData();

        if (File.Exists(save))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(save, FileMode.Open);
            data = (SaveData)(bf.Deserialize(file));
            file.Close();
        }else
        {
            data = null;
        }
        return data;
    }

    public void updateUI(List<SaveData> SaveList)
    {

        foreach (SaveData save in SaveList)
        {
            string SaveName = save.SaveName;
            DateTime Time = save.LastSave;
            string CharacterName = save.CharacterName;
            GenerateSaveEntry(SaveName,Time,CharacterName, () =>
            {
                // load the data <<
            });
        }
    }

    public void GenerateSaveEntry(string SaveName,DateTime Time, string CharacterName, UnityEngine.Events.UnityAction callback)
    {
        var Save = Instantiate(SavePrefab);
        Save.transform.SetParent(content);
        var SaveContent = Save.GetComponent<SaveDataItem>();
        if (SaveContent != null)
            SaveContent.SetData(SaveName,Time,CharacterName, callback);

    }


}
