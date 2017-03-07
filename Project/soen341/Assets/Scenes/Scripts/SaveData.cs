using System.Collections;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public static SaveData current;
    public SaveInfo active;
    public SaveInfo save1;
    public SaveInfo save2;
    public SaveInfo save3;

    public SaveData() { }
}

[System.Serializable]
public class SaveInfo
{
    public string saveName;
    public int lastLevel;
    public int highestLevel;

    public SaveInfo(string saveName)
    {
        this.saveName = saveName;
    }
}