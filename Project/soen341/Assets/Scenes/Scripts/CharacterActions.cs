﻿using System;
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
    bool male = true;
    GameObject maleAvatar, femaleAvatar;

    Destination levelDestination;
    
    private void Start()
    {
        male = SaveData.current.active.gender != 1;
        femaleAvatar = GameObject.Find("FemaleModel");
		maleAvatar = GameObject.Find("MaleModel");
		maleAvatar.GetComponent<Renderer>().enabled = male;
		femaleAvatar.GetComponent<Renderer>().enabled = !male;
        Physics.IgnoreCollision(GameObject.Find("Male").GetComponent<SphereCollider>(), GameObject.Find("Female").GetComponent<SphereCollider>());
    }
    
    public void right()
    {
		print("Character right, pointer: " + actionPointer);
        actionSequence[actionPointer] = 'r';
        actionPointer++;
    }

    public void left()
    {
		print("Character left, pointer: " + actionPointer);
        actionSequence[actionPointer] = 'l';
        actionPointer++;
    }

    public void forward()
    {
        print("Character forward, pointer: " + actionPointer);
        actionSequence[actionPointer] = 'f';
        actionPointer++;

    }

    public void backward()
    {
		print("Character backward, pointer: " + actionPointer);
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
        SimpleCharacterControlGirl femaleCharacter = GameObject.FindObjectOfType<SimpleCharacterControlGirl>();
        SimpleCharacterControl maleCharacter = GameObject.FindObjectOfType<SimpleCharacterControl>();
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
					maleCharacter.setV(1.0f);
					femaleCharacter.setV(1.0f);
                        yield return new WaitForSeconds(0);
                    }
					maleCharacter.setV(0.0f);
					femaleCharacter.setV(0.0f);
                    print("position - x: " + (float)(System.Math.Round(transform.position.x, 0)) + ", z: " + (float)(System.Math.Round(transform.position.z, 0)));
					femaleCharacter.transform.position = new Vector3((float)(System.Math.Round(transform.position.x, 0)), -0.5f, (float)(System.Math.Round(transform.position.z, 0)));
					maleCharacter.transform.position = new Vector3((float)(System.Math.Round(transform.position.x, 0)), -0.5f, (float)(System.Math.Round(transform.position.z, 0)));
                    break;
                case 'b':
                    while (System.Math.Abs(positionBeforeAction.x - transform.position.x) < 0.9889f && System.Math.Abs(positionBeforeAction.z - transform.position.z) < 0.9889f)
                    {
						maleCharacter.setV(-1.0f);
						femaleCharacter.setV(-1.0f);
                        yield return new WaitForSeconds(0);
                    }
					maleCharacter.setV(0.0f);
					femaleCharacter.setV(0.0f);
                    print("position - x: " + (float)(System.Math.Round(transform.position.x, 0)) + ", z: " + (float)(System.Math.Round(transform.position.z, 0)));
					femaleCharacter.transform.position = new Vector3((float)(System.Math.Round(transform.position.x, 0)), -0.5f, (float)(System.Math.Round(transform.position.z, 0)));
					maleCharacter.transform.position = new Vector3((float)(System.Math.Round(transform.position.x, 0)), -0.5f, (float)(System.Math.Round(transform.position.z, 0)));
                    break;
                case 'r':
                    while (System.Math.Abs(transform.eulerAngles.y - rotationBeforeAction.y) < 89)
                    {
						maleCharacter.setH(1.0f);
						femaleCharacter.setH(1.0f);
                        yield return new WaitForSeconds(0);
                    }
					maleCharacter.setH(0.0f);
					femaleCharacter.setH(0.0f);
                    print("angle: " + (float)(System.Math.Round(transform.eulerAngles.y / 100, 1) * 100));
					femaleCharacter.transform.eulerAngles = new Vector3(0, (float)(System.Math.Round(transform.eulerAngles.y / 100, 1) * 100), 0);
					maleCharacter.transform.eulerAngles = new Vector3(0, (float)(System.Math.Round(transform.eulerAngles.y / 100, 1) * 100), 0);
                    break;
                case 'l':
                    if (System.Math.Round(rotationBeforeAction.y, 1) == 0)
                        while (System.Math.Abs(System.Math.Round(transform.eulerAngles.y, 1) - System.Math.Round(rotationBeforeAction.y, 1)) > 271 || (System.Math.Round(transform.eulerAngles.y, 1) == 0))
                        {
							maleCharacter.setH(-1.0f);
							femaleCharacter.setH(-1.0f);
                            yield return new WaitForSeconds(0);
                        }
                    else
                        while (System.Math.Abs(transform.eulerAngles.y - rotationBeforeAction.y) < 89)
                        {
							maleCharacter.setH(-1.0f);
							femaleCharacter.setH(-1.0f);
                            yield return new WaitForSeconds(0);
                        }
					maleCharacter.setH(0.0f);
					femaleCharacter.setH(0.0f);
                    print("angle: " + (float)(System.Math.Round(transform.eulerAngles.y / 100, 1) * 100));
					femaleCharacter.transform.eulerAngles = new Vector3(0, (float)(System.Math.Round(transform.eulerAngles.y / 100, 1) * 100), 0);
					maleCharacter.transform.eulerAngles = new Vector3(0, (float)(System.Math.Round(transform.eulerAngles.y / 100, 1) * 100), 0);
                    break;
                default:
					maleCharacter.setV(0.0f);
					maleCharacter.setH(0.0f);
					femaleCharacter.setV(0.0f);
					femaleCharacter.setH(0.0f);
                    break;
            }
			maleCharacter.setV(0.0f);
			maleCharacter.setH(0.0f);
			femaleCharacter.setV(0.0f);
			femaleCharacter.setH(0.0f);
        }
        yield return new WaitForSeconds(1);
        
        print("end of actions");
        if (isDestinationReached(transform)) { WinLevel(); print("win level 2"); }
        //transform.position = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z);
        //transform.eulerAngles = initialRotation;
		femaleCharacter.transform.position = initialPosition;
		maleCharacter.transform.position = initialPosition;
		femaleCharacter.transform.eulerAngles = initialRotation;
		maleCharacter.transform.eulerAngles = initialRotation;

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

        SceneChanger sceneChange = new SceneChanger();

        

        //Save data

        long endTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        int level = levelDestination.lvl;

        SaveData.current.active.lastLevel = level;
        SaveData.current.active.time[level-1] = (endTime - startTime)/1000;

        if (level > SaveData.current.active.highestLevel)
            SaveData.current.active.highestLevel = level;

        SaveData.current.saves[SaveData.current.active.saveNum] = SaveData.current.active;
        Save();


        sceneChange.NewGame(levelDestination.nextLevel);

        //create a new scene named scene 3 to be able to change to the new level
        SceneManager.LoadScene(levelDestination.nextLevel);
        

    }

    private void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SaveData.gd");
        bf.Serialize(file, SaveData.current);
        file.Close();
    }
}