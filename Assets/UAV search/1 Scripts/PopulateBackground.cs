using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateBackground : MonoBehaviour {

	public PopulateGrid pg;
	public GameObject emptyElement;
	public float emptyZpos = 2; 		//depth at which emepties get drawn


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PopulateEmptyBackground(){
		if ((null == pg)||(null == emptyElement))
			return;

		for (int i = 0; i < pg.gridItems.Count; i++) {
			//instantiate element
			var spawnPos = pg.gridItems[i].transform.position;
			spawnPos.z = emptyZpos; 
			//setup hierchy etc
			GameObject go = GameObject.Instantiate (emptyElement,spawnPos,Quaternion.identity);
			go.transform.parent = this.transform;
		}//for
	}//fn
}
