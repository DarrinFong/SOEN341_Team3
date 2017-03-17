﻿using System.Collections;
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
    public string password;
    public int lastLevel;
    public int highestLevel;

    public SaveInfo(string saveName, string password)
    {
        this.saveName = saveName;
        this.password = password;
    }
}