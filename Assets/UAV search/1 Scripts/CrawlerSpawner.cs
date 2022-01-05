using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerSpawner : MonoBehaviour {

	public PopulateGrid pg;		//the referece to the grid parent
	public GameObject crawler;
	public int numCrawlers = 1; //number of crawlers to be spawned
	public float crawlZpos = -1;

	public List<IndexCrawler> indexCrawlers;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Actually spawn the crawlers here
	public void CrawlSpawners(){
		//0. Abort criterion
		if ((null == pg) || (null == crawler) || (numCrawlers == 0))
			return;

		//1. Get remainder and quotient
		//https://stackoverflow.com/questions/14848275/how-to-get-remainder-and-mod-by-dividing-using-c-sharp/14848335
		int totalTiles = pg.width * pg.height;
		int quotient = totalTiles / numCrawlers;
		int remainder = totalTiles % numCrawlers;
	
		//2. spawn the crawlers 
		for (int i = 0; i < numCrawlers; i++) {

			//get the spawn position, and make the z at right 'layering' order
			var spawnPos = pg.gridItems[i*quotient].transform.position;
			spawnPos.z = crawlZpos; 

			//spawn, set hierchy and get component reference
			GameObject go = GameObject.Instantiate (crawler,spawnPos, Quaternion.identity);
			go.transform.parent = this.transform;
			var ic = go.GetComponent<IndexCrawler> ();

			//set up parameters
			ic.pg = pg;
			ic.startTile = i * quotient;
			ic.numSteps = quotient;

			//store in the list
			indexCrawlers.Add (ic);
		}

		//3. add the remainder to the last crawler's steps
		indexCrawlers[indexCrawlers.Count-1].numSteps += remainder;

		//4. Trigger crawling
		foreach (var ic in indexCrawlers)
			ic.ExternalCrawlTrigger ();
	}//fn.
}
