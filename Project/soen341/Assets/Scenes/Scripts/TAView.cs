using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class TAView : MonoBehaviour {
    public int totalSaves;
    public int average;

	// Use this for initialization
	void Start () {

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
        if (SaveData.current.save1 != null) {
            totalSaves++;
            average += SaveData.current.save1.highestLevel;
            GameObject.Find("P1").GetComponentInChildren<Text>().text = "Student Name: " + SaveData.current.save1.saveName +
                                                                         "\nHighest Level: " + SaveData.current.save1.highestLevel +
                                                                         "\nCurrent Level:" + SaveData.current.save1.lastLevel + "\nTotal play time:";
        }
        if (SaveData.current.save2 != null) {
            totalSaves++;
            average += SaveData.current.save2.highestLevel;
            GameObject.Find("P2").GetComponentInChildren<Text>().text = "Student Name: " + SaveData.current.save2.saveName +
                                                                         "\nHighest Level: " + SaveData.current.save2.highestLevel +
                                                                         "\nCurrent Level:" + SaveData.current.save2.lastLevel + "\nTotal play time:";
        }
        if (SaveData.current.save3 != null) {
            totalSaves++;
            average += SaveData.current.save3.highestLevel;
            GameObject.Find("P3").GetComponentInChildren<Text>().text = "Student Name: " + SaveData.current.save3.saveName +
                                                                         "\nHighest Level: " + SaveData.current.save3.highestLevel +
                                                                         "\nCurrent Level:" + SaveData.current.save3.lastLevel + "\nTotal play time:";
        }

        average = average/totalSaves;
        //Add more
        GameObject.Find("Gen").GetComponentInChildren<Text>().text = "\nStudents Registered: " + totalSaves + "\nAverage Level Reached: ";

    }

    //to do
    public void ShowGraph()
    {

    }
    
        



}
