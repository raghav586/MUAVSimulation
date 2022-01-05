using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the necessary behaviour of tile element,
//most probably, on click, will just... dissapear, or show visibility

public class TileElement : MonoBehaviour {

	public PopulatePeople pp;
	public bool visited = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//This gets called on mouse click
	public void OnMouseUpAsButton(){
//		Debug.Log (this.gameObject.name + ".. got activated at "+this.transform.position.x +"," + this.transform.position.y);

		this.gameObject.SetActive (false);
		visited = true;

		//TODO: if person below, activate that person as well
		if (null != pp) {
			//if Populate people exists, get a list of its people
			var listOfPeople = pp.peopleList.FindAll(item => item!=null);
			//in the list of people, find those at same X
			var listOfPeopleAtX = listOfPeople.FindAll(item => item.transform.position.x == this.transform.position.x);
			//in that list, find all those at same Y
			var listOfPeopleAtXY = listOfPeopleAtX.FindAll(item => item.transform.position.y == this.transform.position.y);

			/*
			//find the next undiscovered person at that location (there could be multiple people on that location)
			var personFound = listOfPeopleAtXY.Find(item => item.GetComponent<PersonElement>().isDiscovered != null);
			//if a discoverable person is found, inform him about his discovery
			if(personFound != null) personFound.GetComponent<PersonElement>().Discovered();
			*/ //this block has been replaced by the block below, basic change is findall in place of find

			//For all those people.. let them know they've been discovered
			var personFound = listOfPeopleAtXY.FindAll(item => item.GetComponent<PersonElement>().isDiscovered != null);
			foreach (var person in personFound) {
				person.GetComponent<PersonElement>().Discovered();
			}

		}

	}
}
