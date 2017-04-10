using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class TAView : MonoBehaviour {

    private int totalSaves;
    private int highest;
    private const string head = "Header";
    private const string player1 = "P1";
    private const string player2 = "P2";
    private const string player3 = "P3";
    private const string general = "Gen";
    private readonly string[] view = {"Student Name: ", "\nHighest Level: ", "\nLast Completed Level: ", "\nCompletion Time", "\nLevel 1: ", "\nLevel 2: ", "\nLevel 3: ", 
                                       "\nStudents Registered: ", "\nHighest Level Reached Overall: ", "No students currently registered!"};

    public void Start ()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/SaveData.gd"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/SaveData.gd", FileMode.Open);
            SaveData.current = (SaveData)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            GameObject.Find(head).GetComponentInChildren<Text>().text = view[9];
        }

        ShowStats();                                                      
    }
   
    public void ShowStats()
    {
        if (SaveData.current.saves[0] != null)
        {
            totalSaves++;
      
            if (SaveData.current.saves[0].highestLevel > highest)
                highest = SaveData.current.saves[0].highestLevel;

            GameObject.Find(player1).GetComponentInChildren<Text>().text = view[0]  + SaveData.current.saves[0].saveName + view[1] + SaveData.current.saves[0].highestLevel +
                                                                         view[2] + SaveData.current.saves[0].lastLevel + view[3] + view[4] + SaveData.current.saves[0].time[0] +
                                                                         view[5] + SaveData.current.saves[0].time[1] +  view[6] + SaveData.current.saves[0].time[2];
        }
        if (SaveData.current.saves[1] != null)
        {
            totalSaves++;

            if (SaveData.current.saves[1].highestLevel > highest)
                highest = SaveData.current.saves[1].highestLevel;

            GameObject.Find(player2).GetComponentInChildren<Text>().text = view[0] + SaveData.current.saves[1].saveName + view[1] + SaveData.current.saves[1].highestLevel +
                                                                         view[2] + SaveData.current.saves[1].lastLevel + view[3] + view[4] + SaveData.current.saves[1].time[0] +
                                                                         view[5] + SaveData.current.saves[1].time[1] + view[6] + SaveData.current.saves[1].time[2];
        }
        if (SaveData.current.saves[2] != null)
        {
            totalSaves++;

            if (SaveData.current.saves[2].highestLevel > highest)
                highest = SaveData.current.saves[2].highestLevel;

            GameObject.Find(player3).GetComponentInChildren<Text>().text = view[0] + SaveData.current.saves[2].saveName + view[1] + SaveData.current.saves[2].highestLevel +
                                                                         view[2] + SaveData.current.saves[2].lastLevel + view[3] + view[4] + SaveData.current.saves[2].time[0] +
                                                                         view[5] + SaveData.current.saves[2].time[1] + view[6] + SaveData.current.saves[2].time[2];
        }
      
        GameObject.Find(general).GetComponentInChildren<Text>().text = view[7] + totalSaves + view[8] + highest;
    }
}
