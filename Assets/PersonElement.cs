using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonElement : MonoBehaviour {

	public bool isDiscovered = false;

	//TODO: get singleton reference to logger

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Discovered(){
		//abort criterion
		if (isDiscovered)
			return;

		//set flag as true
		isDiscovered = true;

		//log in the logger
		LogCSV.instance.DiscoveryMade ();

		//save as last known detection
		var pos = this.transform.position;
		LogCSV.instance.lastKnownDetection = pos;

	}
}
