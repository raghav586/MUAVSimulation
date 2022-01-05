using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulatePeople : MonoBehaviour {

	public PopulateGrid pg;
	public GameObject peopleElement;
//	public float peopleProbability = 0.15f;		//probability of having 'people'
	public float peopleZpos = 1; 				//depth at which people get drawn

	public int numberOfPeople = 5;
	public bool congregate;
	public int congregateNearDistance;
	public List<GameObject> peopleList;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PopulateRandomPeople(){
		if ((null == pg)||(null == peopleElement))
			return;

		if (congregate) {
			//spawn first 
			var x = Random.Range (0, pg.width);
			var y = Random.Range (0, pg.height);
			var spawnPos = new Vector3 (x, y, peopleZpos);
			//setup hierchy etc
			GameObject go = GameObject.Instantiate (peopleElement,spawnPos,Quaternion.identity);
			go.transform.parent = this.transform;
			//get in the list
			peopleList.Add(go);
			for (int i = 1; i < numberOfPeople; i++) {
				var newX = peopleList[0].transform.position.x + Random.Range (-congregateNearDistance, congregateNearDistance);
				var newY = peopleList[0].transform.position.y + Random.Range (-congregateNearDistance, congregateNearDistance);
				newX = (int)Mathf.Clamp (newX,0, pg.width-1);
				newY = (int)Mathf.Clamp (newY,0, pg.height-1);
				var newSpawnPos = new Vector3 (newX, newY, peopleZpos);
				//setup hierchy etc
				GameObject newGo = GameObject.Instantiate (peopleElement,newSpawnPos,Quaternion.identity);
				newGo.transform.parent = this.transform;
				peopleList.Add(newGo);
			}
			return;
		}//if was going with congregate option. Else do random

		for (int i = 0; i < numberOfPeople; i++) {
			var x = Random.Range (0, pg.width);
			var y = Random.Range (0, pg.height);
			var spawnPos = new Vector3 (x, y, peopleZpos);
			//setup hierchy etc
			GameObject go = GameObject.Instantiate (peopleElement,spawnPos,Quaternion.identity);
			go.transform.parent = this.transform;
			peopleList.Add(go);
		}

		//This was the probabilitic distribution way
//		for (int i = 0; i < pg.gridItems.Count; i++) {
//			//instantiate element
//			var spawnPos = pg.gridItems[i].transform.position;
//			spawnPos.z = peopleZpos; 
//			//setup hierchy etc
//			GameObject go = GameObject.Instantiate (peopleElement,spawnPos,Quaternion.identity);
//			go.transform.parent = this.transform;
//			//get in the list
//			peopleList.Add(go);
//			
//			//: based on randomness, draw if 'has' person or not
//			if (Random.value < peopleProbability) {
//				//This is a person. Do something person related if needed. 
//			} else {
//				//that means, this is NOT a person
//				Destroy (go);
//			}//if-else random
//		}//for
	}//fn
}
