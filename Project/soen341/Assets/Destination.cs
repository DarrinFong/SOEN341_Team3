using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour {

    public Vector3 WinningCoordinates;
    public string level;
    public string nextLevel;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SetDestination(Vector3 position, int level) {
        if(position != null)
        {
            this.transform.position = position;
            WinningCoordinates = position;
            this.level = "Level" + level;
            this.nextLevel = "Level" + ++level;
        }
    }
}
