using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour {

    private static GameObject level1;
    private static GameObject level2;
    private static GameObject level3;
    // Use this for initialization
    void Start () {
        level1 = GameObject.Find("Level1");
        level2 = GameObject.Find("Level2");
        level3 = GameObject.Find("Level3");
        switch(SaveData.current.active.highestLevel)
        {            
            case 1:
                level1.SetActive(true);
                level2.SetActive(true);
                level3.SetActive(false);
                break;
            case 2: //Do the same as case 3, which is activate all buttons.
            case 3:
                level1.SetActive(true);
                level2.SetActive(true);
                level3.SetActive(true);
                break;
            default:
                level1.SetActive(true);
                level2.SetActive(false);
                level3.SetActive(false);
                break;

        }
    }
}
