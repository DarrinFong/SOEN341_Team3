using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class TAView : MonoBehaviour {

    public int totalSaves;
    public int average;

	void Start ()
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
            GameObject.Find("Header").GetComponentInChildren<Text>().text = "No students currently registered!";
        }

        ShowStats();



                                                                       
    }
   
    public void ShowStats()
    {
        if (SaveData.current.saves[0] != null)
        {
            totalSaves++;
            average += SaveData.current.saves[0].highestLevel;

            GameObject.Find("P1").GetComponentInChildren<Text>().text = "Student Name: " + SaveData.current.saves[0].saveName +
                                                                         "\nHighest Level: " + SaveData.current.saves[0].highestLevel +
                                                                         "\nCurrent Level: " + SaveData.current.saves[0].lastLevel +
                                                                         "\nCompletion Time" +
                                                                         "\nLevel 1: " + SaveData.current.saves[0].time[0] +
                                                                         "\nLevel 2: " + SaveData.current.saves[0].time[1] + 
                                                                         "\nLevel 3: " + SaveData.current.saves[0].time[2];
        }
        if (SaveData.current.saves[1] != null)
        {
            totalSaves++;
            average += SaveData.current.saves[1].highestLevel;

            GameObject.Find("P2").GetComponentInChildren<Text>().text = "Student Name: " + SaveData.current.saves[1].saveName +
                                                                         "\nHighest Level: " + SaveData.current.saves[1].highestLevel +
                                                                         "\nCurrent Level:" + SaveData.current.saves[1].lastLevel +
                                                                         "\nCompletion Time" +
                                                                         "\nLevel 1: " + SaveData.current.saves[1].time[0] +
                                                                         "\nLevel 2: " + SaveData.current.saves[1].time[1] +
                                                                         "\nLevel 3: " + SaveData.current.saves[1].time[2];
        }
        if (SaveData.current.saves[2] != null)
        {
            totalSaves++;
            average += SaveData.current.saves[2].highestLevel;

            GameObject.Find("P3").GetComponentInChildren<Text>().text = "Student Name: " + SaveData.current.saves[2].saveName +
                                                                         "\nHighest Level: " + SaveData.current.saves[2].highestLevel +
                                                                         "\nCurrent Level:" + SaveData.current.saves[2].lastLevel +
                                                                         "\nCompletion Time" +
                                                                         "\nLevel 1: " + SaveData.current.saves[2].time[0] +
                                                                         "\nLevel 2: " + SaveData.current.saves[2].time[1] +
                                                                         "\nLevel 3: " + SaveData.current.saves[2].time[2];
        }

        average = average/totalSaves;

        GameObject.Find("Gen").GetComponentInChildren<Text>().text = "\nStudents Registered: " + totalSaves + "\nAverage Level Reached: ";

    }

}
