using System.Collections;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public static SaveData current;
    public string saveName;

    public SaveData(string saveName)
    {
        this.saveName = saveName;
    }
}