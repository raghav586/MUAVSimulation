using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crawler : MonoBehaviour {

	public int w,h;

//	public int numSteps;			//if you want it to run only a given N number of steps.. 
//	public int stepCount = 0;
	public float stepTime = 0.2f;


	// Use this for initialization
	void Start () {
		StartCoroutine ("crawl");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//This is an absolute crawler. Starts from 0,0 and then goes all the way to w/h
	IEnumerator crawl(){
		//an initial delay, while the playfield is registered
		yield return new WaitForSeconds (1);

		for(int i = 0; i<w;i++){
			for(int j = 0; j<h;j++){
				//--- Crawl --- 
				if(i%2 == 0) 
					this.transform.position = new Vector3(i, j, -1); //for 0 or even rows
				else 
					this.transform.position = new Vector3(i, h-1-j, -1); //for odd rows. 

				//--- hit the button ---
				Playfield.elements[i,j].OnMouseUpAsButton();

				//increment the counter, break if it reaches the limit
//				stepCount++;
//				if (stepCount == numSteps)
//					yield break;

				//--- finally, wait for a few seconds for the step --- 
				yield return new WaitForSeconds(stepTime);
			}//j
		}//i
	}


}
