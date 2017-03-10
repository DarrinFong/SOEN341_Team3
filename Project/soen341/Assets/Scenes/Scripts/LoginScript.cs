using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class LoginScript : MonoBehaviour{

    public void Check(InputField pass)
    {
        if (pass.text.Trim() == "soen341")
        {
            SceneManager.LoadScene("TAView");
        }
        
    }

    public void NewGame(string newGameLevel)
    {
        SceneManager.LoadScene(newGameLevel);
    }


}
