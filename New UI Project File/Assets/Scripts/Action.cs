using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : Block {

    public Transform shape;

    enum Type {forward, backward, left, right}

	// Use this for initialization
	void Start () {
        size = new Vector3(shape.localScale.x, shape.localScale.y, 0);
        //print("size of action block " + size);
	}

    public override IEnumerator executeBlock()
    {
        switch (type)
        {
            case 0:
                print("Forward");
                break;
            case 1:
                print("Backward");
                break;
            case 2:
                print("Left");
                break;
            case 3:
                print("Right");
                break;
            default:
                yield return new WaitForSeconds(0.01f);
                print("Text not set");
                break;
        }
        
    }

    public override void changeType()
    {

        switch (type)
        {
            case 0:
                setText("Forward");
                break;
            case 1:
                setText("Backward");
                break;
            case 2:
                setText("Left");
                break;
            case 3:
                setText("Right");
                break;
            default:
                setText("Text not set");
                break;
        }

    }

	// Update is called once per frame
	//void Update () {
		
	//}
}
