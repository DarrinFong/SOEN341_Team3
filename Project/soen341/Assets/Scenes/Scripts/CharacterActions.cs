using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterActions : MonoBehaviour {

    static char[] actionSequence = new char[1000];
    static int actionPointer = 0;
    long startTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

    Destination levelDestination;

    public void right()
    {
        print("dickbutt right, pointer: " + actionPointer);
        actionSequence[actionPointer] = 'r';
        actionPointer++;
    }

    public void left()
    {
        print("dickbutt left, pointer: " + actionPointer);
        actionSequence[actionPointer] = 'l';
        actionPointer++;
    }

    public void forward()
    {
        print("dickbutt forward, pointer: " + actionPointer);
        actionSequence[actionPointer] = 'f';
        actionPointer++;

    }

    public void backward()
    {
        print("dickbutt backward, pointer: " + actionPointer);
        actionSequence[actionPointer] = 'b';
        actionPointer++;
    }

    public void runCode()
    {
        StartCoroutine(runActions(1));
    }

    public void clearActions()
    {
        actionSequence = new char[1000];
        actionPointer = 0;
    }
    
    public System.Collections.IEnumerator runActions(int Whatever)
    {
        SimpleCharacterControl dickButt = GameObject.FindObjectOfType<SimpleCharacterControl>();
        levelDestination = GameObject.FindObjectOfType<Destination>();

        // m_animator.SetTrigger("Jump");
        // m_animator.SetTrigger("Land");
        Vector3 initialPosition = transform.position;
        Vector3 initialRotation = transform.eulerAngles;
        for (int action = 0; action < actionPointer; action++)
        {
            Vector3 positionBeforeAction = transform.position;
            Vector3 rotationBeforeAction = transform.eulerAngles;

            switch (actionSequence[action])
            {
                case 'f':
                    while (System.Math.Abs(positionBeforeAction.x - transform.position.x) < 0.9889f && System.Math.Abs(positionBeforeAction.z - transform.position.z) < 0.9889f)
                    {
                        dickButt.setV(1.0f);
                        yield return new WaitForSeconds(0);
                    }
                    dickButt.setV(0.0f);
                    print("position - x: " + (float)(System.Math.Round(transform.position.x, 0)) + ", z: " + (float)(System.Math.Round(transform.position.z, 0)));
                    transform.position = new Vector3((float)(System.Math.Round(transform.position.x, 0)), -0.5f, (float)(System.Math.Round(transform.position.z, 0)));
                    break;
                case 'b':
                    while (System.Math.Abs(positionBeforeAction.x - transform.position.x) < 0.9889f && System.Math.Abs(positionBeforeAction.z - transform.position.z) < 0.9889f)
                    {
                        dickButt.setV(-1.0f);
                        yield return new WaitForSeconds(0);
                    }
                    dickButt.setV(0.0f);
                    print("position - x: " + (float)(System.Math.Round(transform.position.x, 0)) + ", z: " + (float)(System.Math.Round(transform.position.z, 0)));
                    transform.position = new Vector3((float)(System.Math.Round(transform.position.x, 0)), -0.5f, (float)(System.Math.Round(transform.position.z, 0)));
                    break;
                case 'r':
                    while (System.Math.Abs(transform.eulerAngles.y - rotationBeforeAction.y) < 89)
                    {
                        dickButt.setH(1.0f);
                        yield return new WaitForSeconds(0);
                    }
                    dickButt.setH(0.0f);
                    print("angle: " + (float)(System.Math.Round(transform.eulerAngles.y / 100, 1) * 100));
                    transform.eulerAngles = new Vector3(0, (float)(System.Math.Round(transform.eulerAngles.y / 100, 1) * 100), 0);
                    break;
                case 'l':
                    if (System.Math.Round(rotationBeforeAction.y, 1) == 0)
                        while (System.Math.Abs(System.Math.Round(transform.eulerAngles.y, 1) - System.Math.Round(rotationBeforeAction.y, 1)) > 271 || (System.Math.Round(transform.eulerAngles.y, 1) == 0))
                        {
                            dickButt.setH(-1.0f);
                            yield return new WaitForSeconds(0);
                        }
                    else
                        while (System.Math.Abs(transform.eulerAngles.y - rotationBeforeAction.y) < 89)
                        {
                            dickButt.setH(-1.0f);
                            yield return new WaitForSeconds(0);
                        }
                    dickButt.setH(0.0f);
                    print("angle: " + (float)(System.Math.Round(transform.eulerAngles.y / 100, 1) * 100));
                    transform.eulerAngles = new Vector3(0, (float)(System.Math.Round(transform.eulerAngles.y / 100, 1) * 100), 0);
                    break;
                default:
                    dickButt.setV(0.0f);
                    dickButt.setH(0.0f);
                    break;
            }
            dickButt.setV(0.0f);
            dickButt.setH(0.0f);
        }
        yield return new WaitForSeconds(1);
        print("end of actions");
        if (isDestinationReached(transform)) { WinLevel(); print("win level 2"); }
        transform.position = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z);
        transform.eulerAngles = initialRotation;
        
    }

    private bool isDestinationReached(Transform transform)
    {
        //here we difine the coordinates necessary for a win level scenario
        print("x Position: " + transform.position.x);
        print("z Position: " + transform.position.z);
        if (transform.position.x == levelDestination.WinningCoordinates.x && transform.position.z == levelDestination.WinningCoordinates.z)
            return true;
        else return false;
    }
    private void WinLevel()
    {
<<<<<<< HEAD
        SceneChanger sceneChange = new SceneChanger();
=======
        

        //Save data
>>>>>>> 61a6a6786ea444d4c3862a129d9e971981735e3e
        long endTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        int level = levelDestination.lvl;

        SaveData.current.active.lastLevel = level;
        SaveData.current.active.time[level-1] = (endTime - startTime)/1000;

        if (level > SaveData.current.active.highestLevel)
            SaveData.current.active.highestLevel = level;

        SaveData.current.saves[SaveData.current.active.saveNum] = SaveData.current.active;
        Save();
<<<<<<< HEAD

        sceneChange.NewGame(levelDestination.nextLevel);
=======
        //create a new scene named scene 3 to be able to change to the new level
        SceneManager.LoadScene(levelDestination.nextLevel);
        
>>>>>>> 61a6a6786ea444d4c3862a129d9e971981735e3e
    }

    private void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SaveData.gd");
        bf.Serialize(file, SaveData.current);
        file.Close();
    }
}