using System.Collections;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public static SaveData current;
    public SaveInfo active;
    public SaveInfo[] saves = new SaveInfo[3];

    public SaveData() { }
}

[System.Serializable]
public class SaveInfo
{
    public string saveName;
    public int lastLevel;
    public int highestLevel;
    public int saveNum;
    public int gender;
    public long timelvl1;
    public long timelvl2;
    public long timelvl3;


    public SaveInfo(string saveName)
    {
        this.saveName = saveName;
    }
}