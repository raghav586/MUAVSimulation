using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositionCamera : MonoBehaviour {

	public PopulateGrid pg;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void RePositionCamera(){
		if ((null == pg))
			return;

		var z = this.transform.position.z;
		this.transform.position = new Vector3 (pg.width/2, pg.height/2, z);
	}
}
