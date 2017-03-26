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
            promptPasswordInput();
        }
        else
            promptUsername();

    }

    public void promptPasswordInput()
    {
        SaveLoad.savesPanel.gameObject.SetActive(false);
        SaveLoad.passwordPanel.gameObject.SetActive(true);
    }

    public void authenticatePassword(InputField pass)
    {
        if (SaveData.current.active.AuthPassword(pass.text))
            loadSave();
        else
            Debug.Log("Invalid password");
    }

    public void cancelPassword(InputField pass)
    {
        pass.text = null;
        SaveLoad.passwordPanel.gameObject.SetActive(false);
        SaveLoad.savesPanel.gameObject.SetActive(true);
        populateLoadButtons();
    }

    public void promptUsername()
    {
        SaveLoad.savesPanel.gameObject.SetActive(false);
        SaveLoad.usernamePanel.gameObject.SetActive(true);
        GameObject.Find("Male").GetComponent<Image>().color = Color.white;
        GameObject.Find("Female").GetComponent<Image>().color = Color.white;
    }

    public void cancelUsername(InputField name)
    {
        name.text = null;
        SaveLoad.usernamePanel.gameObject.SetActive(false);
        SaveLoad.savesPanel.gameObject.SetActive(true);
        populateLoadButtons();
    }

    public void changeSex(string sex)
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

    public bool choseSex()
    {
        var maleColor = GameObject.Find("Male").GetComponent<Image>().color;
        var femaleColor = GameObject.Find("Male").GetComponent<Image>().color;

        return (maleColor == Color.green || femaleColor == Color.green);
    }

    public void saveUsername(InputField name)
    {
        string userName = name.text;
        string pass = GameObject.Find("PasswordInput").GetComponentInChildren<InputField>().text;
        string repass = GameObject.Find("PasswordCheck").GetComponentInChildren<InputField>().text;

        if (validateUsername(userName))
        {
            if (checkPasswords(pass, repass))
            {
                if (choseSex())
                {
                    SaveData.current.saves[currentSaveNum] = new SaveInfo(userName);
                    SaveData.current.active = SaveData.current.saves[currentSaveNum];
                    SaveData.current.active.SetPassword(pass);
                    SaveData.current.active.gender = SaveLoad.sex;
                    loadSave();
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

    public bool validateUsername(string name)
    {
        return name.All(char.IsLetter);
    }

    public bool checkPasswords(string password, string password2)
    {
        return password == password2 && password.Length > 5 && !password.Any(x => x == ' ');
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
            SaveLoad.passwordPanel = GameObject.Find("PasswordPanel");
            SaveLoad.usernamePanel.gameObject.SetActive(false);
            SaveLoad.passwordPanel.gameObject.SetActive(false);
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