using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Container {

    GameObject character;


    public override IEnumerator executeBlock()
    {
        foreach (Block b in elements)
        {
            //print("Calling block in character coroutine");
            b.StartCoroutine("executeBlock");
            yield return new WaitForSeconds(0);
        }

        CharacterActions charController = GameObject.FindObjectOfType<CharacterActions>();
        charController.runCode();

    }

    public override void changeType()
    {

        setContainerType(0);

        switch(type)
        {

            case 0:
                setText("Main Character");
                break;
            default:
                setText("No text set");
                break;

        }
    }
	
}
