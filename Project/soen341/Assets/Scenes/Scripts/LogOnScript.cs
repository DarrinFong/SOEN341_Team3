using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class LogOnScript : MonoBehaviour{

    public void Check(InputField pass)
    {
        if (pass == null)
        {
            throw new System.IO.IOException("person");
        }

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
