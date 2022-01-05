using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCrawlerSpawner : MonoBehaviour {

	public PopulateGrid pg;
	public GameObject randomCrawler;
	public int numRandomCrawlers = 1;
	//TODO: z position

	public bool spawnNearLastDetection = false;
	public Vector2 nearSpawnRange = new Vector2(3, 3);


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SpawnRandomCrawlers(){
		//0. abort criterion
		if ((null == pg) || (null == randomCrawler) || (numRandomCrawlers == 0))
			return;

		//1. spawn crawlers
		for (int i = 0; i < numRandomCrawlers; i++) {
			var spawnTile = new Vector2 (Random.Range (0, pg.width), Random.Range (0,pg.height));


			//instantiate random cralwer, note vector2 passsed as vector3
			GameObject go = GameObject.Instantiate (randomCrawler, spawnTile, Quaternion.identity);
			go.transform.parent = this.transform;
			var g2c = go.GetComponent<GoToCrawler> ();

			//setup parameters
			g2c.startTile = spawnTile;
			g2c.endTile = spawnTile;
			g2c.widthHeight = new Vector2 (pg.width, pg.height);
			g2c.pg = pg;
			g2c.goRandom = true;
			g2c.spawnNearLastDetection = spawnNearLastDetection;
			g2c.nearSpawnRange = nearSpawnRange;
		
		}//for

	
	}//fn
}
