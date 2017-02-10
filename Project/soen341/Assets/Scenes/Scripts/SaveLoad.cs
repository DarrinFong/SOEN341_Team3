using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoad : MonoBehaviour {

     public void NewGame(InputField name)
     {
        SaveData.current = new SaveData(name.text);
        Debug.Log(SaveData.current.saveName);
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
        bf.Serialize(file, SaveData.current);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            SaveData.current = (SaveData)bf.Deserialize(file);
            Debug.Log(SaveData.current.saveName);
            file.Close();

        }
    }
}