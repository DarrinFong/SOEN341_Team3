using System.Collections;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public static SaveData current;
    public SaveInfo active;
    public SaveInfo[] saves = new SaveInfo[3];

}

[System.Serializable]
public class SaveInfo
{
    public string saveName;
    private string password;
    public int lastLevel;
    public int highestLevel;
    public int saveNum;
    public int gender;
    public long[] time =  new long[3];

    public SaveInfo(string saveName)
    {
        this.saveName = saveName;
    }

    public bool AuthPassword(string password)
    {
        return this.password == password;
    }

    public void SetPassword(string password)
    {
        this.password = password;
    }
}