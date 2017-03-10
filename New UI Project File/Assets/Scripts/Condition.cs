using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition : Container {

    enum Type {ifBlock, whileBlock}

    // Use this for initialization

    public override IEnumerator executeBlock()
    {
        int until = 0;

        switch(type)
        {

            case 0:
                break;
            case 1:
                until = 2;
                break;
            case 2:
                until = 5;
                break;
            case 3:
                until = 10;
                break;
            default:
                print("Default execute block reached");
                break;

        }

        if (until > 0) {
            for (int c = 0; c < until; c++)
            {
                foreach (Block b in elements)
                {
                   // print("Calling block in condition coroutine");
                    b.StartCoroutine("executeBlock");
                    yield return new WaitForSeconds(1);
                }
            }
        }

    }


    public override void changeType()
    {
        setContainerType(1);
        //IF = 0
        //For 5 = 1
        //For 10 = 2
        switch (type)
        {
            case 0:
                setText("IF");
                break;
            case 1:
                setText("For 2 times");
                break;
            case 2:
                setText("For 5 times");
                break;
            case 3:
                setText("For 10 times");
                break;
            default:
                setText("Text not set");
                break;
        }
    }

}
