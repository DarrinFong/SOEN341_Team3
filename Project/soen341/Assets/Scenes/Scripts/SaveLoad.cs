using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SaveLoad : MonoBehaviour
{
  /**  public void NewGame(SaveInfo save)
    {
        if (name.text == "" || name.text == null)
        {
            Debug.Log("Please enter player name!");
            return;
        }
        else
        {
            SaveData.save1 = new SaveInfo(name.text);
            SaveData.active = SaveData.save1;
            Debug.Log(SaveData.active.saveName);
            GameObject.Find("Load").GetComponentInChildren<Text>().text = "Load " + SaveData.active.saveName;
        }
    }**/

    public void saveLoad(InputField name, int saveNum)
    {
        if (name.text == "" || name.text == null)
        {
            Debug.Log("Please enter player name!");
            return;
        }
        else
        {
            switch (saveNum)
            {
                case 1:
                    SaveData.current.save1 = new SaveInfo(name.text);
                    SaveData.current.active = SaveData.current.save1;
                    GameObject.Find("Save1").GetComponentInChildren<Text>().text = "Load " + SaveData.current.active.saveName;
                    break;
                case 2:
                    SaveData.current.save2 = new SaveInfo(name.text);
                    SaveData.current.active = SaveData.current.save2;
                    GameObject.Find("Save2").GetComponentInChildren<Text>().text = "Load " + SaveData.current.active.saveName;
                    break;
                case 3:
                    SaveData.current.save3 = new SaveInfo(name.text);
                    SaveData.current.active = SaveData.current.save3;
                    GameObject.Find("Save3").GetComponentInChildren<Text>().text = "Load " + SaveData.current.active.saveName;
                    break;
                default:
                    Debug.Log("Error in saveLoad. Invalid save number.");
                    break;
            }

            Debug.Log(SaveData.current.active.saveName);
            Save();
        }
        
    }

    public void saveLoad1(InputField name)
    {
        saveLoad(name, 1);
    }

    public void saveLoad2(InputField name)
    {
        saveLoad(name, 2);
    }

    public void saveLoad3(InputField name)
    {
        saveLoad(name, 3);
    }

    public string getSavePath()
    {
        return Application.persistentDataPath + "/SaveData.gd";
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(getSavePath());
        bf.Serialize(file, SaveData.current);
        file.Close();
    }

    public void Load()
    {       

    }

    public void Start()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(getSavePath()))
        {
            FileStream file = File.Open(getSavePath(), FileMode.Open);
            SaveData.current = (SaveData)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            FileStream file = File.Create(getSavePath());
            if (SaveData.current != null)
                bf.Serialize(file, SaveData.current);
            else
                bf.Serialize(file, new SaveData());
            file.Close();
        }

        if(SaveData.current.save1 != null)
            GameObject.Find("Save1").GetComponentInChildren<Text>().text = "Load " + SaveData.current.save1.saveName ?? "Save Slot 1";
        if(SaveData.current.save2 != null)
            GameObject.Find("Save2").GetComponentInChildren<Text>().text = "Load " + SaveData.current.save2.saveName ?? "Save Slot 2";
        if(SaveData.current.save3 != null)
            GameObject.Find("Save3").GetComponentInChildren<Text>().text = "Load " + SaveData.current.save3.saveName ?? "Save Slot 3";
    }
}