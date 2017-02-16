using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SaveLoad : MonoBehaviour
{
    public SaveData[] saves = new SaveData[3];
    public void initializeSaveFiles()
    {
        string[] saveFiles = Directory.GetFiles(Application.persistentDataPath, "*.gd");
        for (int i = 0; i < saves.Count() && i < saveFiles.Count(); i++)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(saveFiles[i], FileMode.Open);
            saves[i] = (SaveData)bf.Deserialize(file);
            file.Close();
        }
        SaveData.current = saves[0];
    }

    public void NewGame(InputField name)
    {
        initializeSaveFiles();

        if (name.text == "" || name.text == null) {
            Debug.Log("You gotta enter a name m8");
            return;
        }
        int newIndex = -1;
        for (int i=0; i<saves.Count(); i++)
        {
            if (saves[i].saveName == "")
            {
                newIndex = i;
                break;
            }
        }
        if (newIndex < 0)
        {
            Debug.Log("No more save spots!");
        }
        else
        {
            SaveData.current = new SaveData(name.text);
            saves[newIndex] = SaveData.current;
            Debug.Log(SaveData.current.saveName);
            Save();
        }
    }

    public string getCurrentFile()
    {
        return Application.persistentDataPath + "/" + SaveData.current.saveName + ".gd";
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(getCurrentFile());
        bf.Serialize(file, SaveData.current);
        file.Close();
    }

    public void Load()
    {
        string curFile = getCurrentFile();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(curFile, FileMode.Open);
        SaveData.current = (SaveData)bf.Deserialize(file);
        Debug.Log(SaveData.current.saveName);        
    }
}