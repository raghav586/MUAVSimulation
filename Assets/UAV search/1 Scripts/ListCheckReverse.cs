using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// c# lsits, partially/wholy replace https://www.geeksforgeeks.org/c-sharp-reverse-the-order-of-the-elements-in-the-entire-list-or-in-the-specified-range/
// in unity, reversing a list etc: https://forum.unity.com/threads/reversing-a-list.79972/



public class ListCheckReverse : MonoBehaviour {

	public List<GameObject> original;		//the original list
	public List<GameObject> reversed;		//totally reversed list
	public int w,h;							//width and height in matrix notation	
	public List<GameObject> reorineted;		//the list that will be reorded to so that alternate columuns are reversed

	// Use this for initialization
	void Start () {
		//fill all the lists
		foreach (Transform child in transform) {
			original.Add (child.gameObject);
			reversed.Add (child.gameObject);
			reorineted.Add (child.gameObject);
		}

		//completely reorder
		reversed.Reverse ();

		//reverse alternate rows
		for (int i = 1; i < w; i+=2) {
			//Looking to index alternate items, so start at 1, and increment by 2
			reorineted.Reverse(i*h,h); //reverse from a certain index, uptil a certain count
		}
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
