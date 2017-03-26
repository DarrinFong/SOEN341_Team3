using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour {

    public Vector3 WinningCoordinates;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SetDestination(Vector3 position) {
        if(position != null)
        {
            this.transform.position = position;
            WinningCoordinates = position;
        }
    }
}
