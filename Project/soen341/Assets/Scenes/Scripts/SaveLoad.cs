using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveLoad : MonoBehaviour
{
    private static GameObject usernamePanel;
    private static GameObject savesPanel;
    private static GameObject passwordPanel;
    private static bool hasStarted;
    private static int sex;
    private int currentSaveNum;

    public void saveLoad(int saveNum)
    {
        currentSaveNum = saveNum;

        if (SaveData.current.saves[currentSaveNum] != null)
        {
            SaveData.current.active = SaveData.current.saves[currentSaveNum];
            SaveData.current.active.saveNum = currentSaveNum;
            PromptPasswordInput();
        }
        else
            PromptUsername();

    }

    public void PromptPasswordInput()
    {
        SaveLoad.savesPanel.gameObject.SetActive(false);
        SaveLoad.passwordPanel.gameObject.SetActive(true);
    }

    public void AuthenticatePassword(InputField pass)
    {
        if (SaveData.current.active.AuthPassword(pass.text))
            LoadSave();
        else
            Debug.Log("Invalid password");
    }

    public void CancelPassword(InputField pass)
    {
        pass.text = null;
        SaveLoad.passwordPanel.gameObject.SetActive(false);
        SaveLoad.savesPanel.gameObject.SetActive(true);
        PopulateLoadButtons();
    }

    public void PromptUsername()
    {
        SaveLoad.savesPanel.gameObject.SetActive(false);
        SaveLoad.usernamePanel.gameObject.SetActive(true);
        GameObject.Find("Male").GetComponent<Image>().color = Color.white;
        GameObject.Find("Female").GetComponent<Image>().color = Color.white;
    }

    public void CancelUsername(InputField name)
    {
        name.text = null;
        SaveLoad.usernamePanel.gameObject.SetActive(false);
        SaveLoad.savesPanel.gameObject.SetActive(true);
        PopulateLoadButtons();
    }

    public void ChangeSex(string sex)
    {
        switch (sex)
        {
            case "M":
                GameObject.Find("Male").GetComponent<Image>().color = Color.green;
                GameObject.Find("Female").GetComponent<Image>().color = Color.white;
                SaveLoad.sex = 0;
                break;
            case "F":
                GameObject.Find("Male").GetComponent<Image>().color = Color.white;
                GameObject.Find("Female").GetComponent<Image>().color = Color.green;
                SaveLoad.sex = 1;
                break;
        }
    }

    public bool ChoseSex()
    {
        var maleColor = GameObject.Find("Male").GetComponent<Image>().color;
        var femaleColor = GameObject.Find("Female").GetComponent<Image>().color;

        return (maleColor == Color.green || femaleColor == Color.green);
    }

    public void SaveUsername(InputField name)
    {
        string userName = name.text;
        string pass = GameObject.Find("PasswordInput").GetComponentInChildren<InputField>().text;
        string repass = GameObject.Find("PasswordCheck").GetComponentInChildren<InputField>().text;

        if (ValidateUsername(userName))
        {
            if (CheckPasswords(pass, repass))
            {
                if (ChoseSex())
                {
                    SaveData.current.saves[currentSaveNum] = new SaveInfo(userName);
                    SaveData.current.active = SaveData.current.saves[currentSaveNum];
                    SaveData.current.active.SetPassword(pass);
                    SaveData.current.active.gender = SaveLoad.sex;
                    LoadSave();

                }
                else
                    Debug.Log("Must choose a sex");
            }
            else
                Debug.Log("Passwords must match, may not have spaces and may not be less than 6 characters!");
        }
        else
            Debug.Log("Must input a valid username");
    }

    public bool ValidateUsername(string name)
    {
        return name.All(char.IsLetter);
    }

    public bool CheckPasswords(string password, string password2)
    {
        return password == password2 && password.Length > 5 && !password.Any(x => x == ' ');
    }

    public void LoadSave()
    {
        Save();
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Loading : " + SaveData.current.active.saveName);
        //change scene to main menu, using data from SavaData.current.active
    }

    public string GetSavePath()
    {
        return Application.persistentDataPath + "/SaveData.gd";
    }

    public void PopulateLoadButtons()
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
        FileStream file = File.Create(GetSavePath());
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
            SaveLoad.passwordPanel = GameObject.Find("PasswordPanel");
            SaveLoad.usernamePanel.gameObject.SetActive(false);
            SaveLoad.passwordPanel.gameObject.SetActive(false);
            BinaryFormatter bf = new BinaryFormatter();
            if (File.Exists(GetSavePath()))
            {
                FileStream file = File.Open(GetSavePath(), FileMode.Open);
                SaveData.current = (SaveData)bf.Deserialize(file);
                file.Close();
            }
            else
            {
                FileStream file = File.Create(GetSavePath());
                if (SaveData.current != null)
                    bf.Serialize(file, SaveData.current);
                else
                    bf.Serialize(file, new SaveData());
                file.Close();
                SaveData.current = new SaveData();
            }

            PopulateLoadButtons();
        }
    }
}