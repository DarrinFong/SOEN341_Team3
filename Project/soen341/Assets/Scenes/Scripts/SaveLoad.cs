using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class SaveLoad : MonoBehaviour
{
    private static GameObject usernamePanel;
    private static GameObject savesPanel;
    private static bool hasStarted;
    private int currentSaveNum;

    public void saveLoad(int saveNum)
    {
        currentSaveNum = saveNum;

        if (SaveData.current.saves[currentSaveNum] != null)
        {
            SaveData.current.active = SaveData.current.saves[currentSaveNum];
            SaveData.current.active.saveNum = currentSaveNum;
            loadSave();
        }
        else
            promptUsername();

    }

    public void promptUsername()
    {
        SaveLoad.savesPanel.gameObject.SetActive(false);
        SaveLoad.usernamePanel.gameObject.SetActive(true);
    }

    public void cancelUsername(InputField name)
    {
        name.text = null;
        SaveLoad.usernamePanel.gameObject.SetActive(false);
        SaveLoad.savesPanel.gameObject.SetActive(true);
        populateLoadButtons();
    }

    public void saveUsername(InputField name)
    {
        string userName = name.text.Trim();
        if (userName != "" && userName != null)
        {
            SaveData.current.saves[currentSaveNum] = new SaveInfo(userName);
            SaveData.current.active = SaveData.current.saves[currentSaveNum];
            loadSave();
        }
        else
            Debug.Log("Input a valid username");
    }

    public void loadSave()
    {
        Save();
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Loading : " + SaveData.current.active.saveName);
        //change scene to main menu, using data from SavaData.current.active
    }

    public string getSavePath()
    {
        return Application.persistentDataPath + "/SaveData.gd";
    }

    public void populateLoadButtons()
    {
        if (SaveData.current.saves[0] != null)
            GameObject.Find("Save0").GetComponentInChildren<Text>().text = "Load " + SaveData.current.saves[0].saveName;
        if (SaveData.current.saves[1] != null)
            GameObject.Find("Save1").GetComponentInChildren<Text>().text = "Load " + SaveData.current.saves[1].saveName;
        if (SaveData.current.saves[2] != null)
            GameObject.Find("Save2").GetComponentInChildren<Text>().text = "Load " + SaveData.current.saves[2].saveName;
        
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(getSavePath());
        bf.Serialize(file, SaveData.current);
        file.Close();
    }

    public void Start()
    {
        if(!hasStarted)
        {
            SaveLoad.hasStarted = true;
            SaveLoad.savesPanel = GameObject.Find("SavesPanel");
            SaveLoad.usernamePanel = GameObject.Find("UsernamePanel");
            SaveLoad.usernamePanel.gameObject.SetActive(false);
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
                SaveData.current = new SaveData();
            }

            populateLoadButtons();
        }
    }
}