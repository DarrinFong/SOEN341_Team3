using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : Block {

    public Transform shape;

	// Use this for initialization
	void Start () {
        size = new Vector3(shape.localScale.x, shape.localScale.y, 0);
        print("size of action block " + size);
	}
	
	// Update is called once per frame
	//void Update () {
		
	//}

}
