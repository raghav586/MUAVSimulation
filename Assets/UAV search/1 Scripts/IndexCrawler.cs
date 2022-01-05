using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndexCrawler : MonoBehaviour {

	public PopulateGrid pg;

	public int startTile;			//tile to start on
	public int numSteps;			//total numbers of step to move
	public float stepTime = 0.2f;	//time between steps
	public bool autoStart = false;

	// Use this for initialization
	void Start () {
		if (null == pg)
			return;

		//else.. 
		if(!autoStart)
			StartCoroutine("crawl");
	}


	public void ExternalCrawlTrigger(){
		StartCoroutine("crawl");
	}

	//Start Crawling
	IEnumerator crawl(){
		//0. give some delay
		yield return new WaitForSeconds (1);

		//1. initial position
//		GetAboveTile (startTile);
		//Note: Now, this is happening in the indexing itself
		//as, you might have to.. 'activate' the starting tile

		//2. Start indexing
		for (int i = 0; i < numSteps; i++) {
			//get above the tile, and 'activate' it
			GetAboveTile (startTile + i);

			//delay
			yield return new WaitForSeconds(stepTime);
		}

	}

	//this functions get the transform in the same x/y as given tile
	void GetAboveTile(int i){
		//Get above
		var tempPos = pg.gridItems[i].transform.position;
		tempPos.z = this.transform.position.z;
		this.transform.position = tempPos;

		//Activate
		pg.gridItems[i].GetComponent<TileElement>().OnMouseUpAsButton();

		//Register 'effort'
		LogCSV.instance.EffortMade();
	}
}
