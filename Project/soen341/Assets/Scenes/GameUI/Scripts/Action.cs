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

    public override void removeSelf()
    {
        print("deleting action");
        Destroy(this.gameObject);
    }

    public override IEnumerator executeBlock()
    {
        CharacterActions charController = GameObject.FindObjectOfType<CharacterActions>();

        switch (type)
        {
            case 0:
                print("Forward");
                charController.forward();
                break;
            case 1:
                print("Backward");
                charController.backward();
                break;
            case 2:
                print("Left");
                charController.left();
                break;
            case 3:
                print("Right");
                charController.right();
                break;
            default:
                yield return new WaitForSeconds(0);
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
