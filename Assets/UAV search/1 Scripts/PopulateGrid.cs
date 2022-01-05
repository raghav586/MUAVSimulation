using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PopulateGrid : MonoBehaviour {

	public int width,height;	//width and height of the grid
	public GameObject gridElement;
	public int depth = 0;
	public PopulatePeople pp;

	public bool reorderGrid = true;
	public List<GameObject> gridItems;

	public UnityEvent OnFinishGridPopulation;

	// Use this for initialization
	void Start () {
		//abort condition
		if((width ==0 )||(height ==0)||(null == gridElement))
			return;

		//generate list items
		GenerateListGridElements ();

		//reorder items if required
		if (reorderGrid) {
			for (int i = 1; i < width; i += 2) {
				//make alternate row be reveresed
				gridItems.Reverse (i * height, height);
			}//for
		}//if

		//spawn any action/event
		OnFinishGridPopulation.Invoke();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void GenerateListGridElements(){
		//run double for loop
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				//instantiate
				var go = GameObject.Instantiate (gridElement);
				go.transform.parent = this.transform;
				go.transform.localPosition = new Vector3 (i, j, depth);
				//setup references
				go.GetComponent<TileElement>().pp = pp;

				//add in the list
				gridItems.Add (go);
			}//j
		}//i
	}//function
}
