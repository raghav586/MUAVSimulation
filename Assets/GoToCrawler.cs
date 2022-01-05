using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This crawler goes from point A to point B
public class GoToCrawler : MonoBehaviour {
	
	public float zDepth = -2f;
	public Vector2 startTile;
	public Vector2 endTile;
	public Vector2 currentTile;
	public float distThresh = 0.2f;
	public float stepTime = 0.2f;

	public bool canStep = true;
	public Vector2 vecDif;

	public bool goRandom = true;
	public Vector2 widthHeight = new Vector2(10,10);
	public Vector2 priorityGrid = new Vector2(5, 5);

	public PopulateGrid pg;		//the grid populator, if present, this could be... 'activated'
	public bool spawnNearLastDetection = false;
	public Vector2 nearSpawnRange = new Vector2(3, 3);


//	public Vector3 vec3to2;
//	public Vector2 from3to2;
//
//	public Vector2 vec2to3;
//	public Vector3 from2to3;


	// Use this for initialization
	void Start () {
		//start 
		this.transform.position = new Vector3 (startTile.x, startTile.y, zDepth);
//		//go to end tile

	}
	
	// Update is called once per frame
	void Update () {
		//get current tile, and vector difference till end tile
		currentTile = (Vector2)this.transform.position;
		vecDif = endTile - currentTile;

		//take a step if needed and can
		if(canStep && (vecDif.magnitude > distThresh)){
			StartCoroutine("Go2EndTile");
		}

		//reached point, and random movement is on, try another random tile
		if(goRandom && (vecDif.magnitude <= distThresh)){
			Vector2 newEndTile = new Vector2();
			if (spawnNearLastDetection && LogCSV.instance.recentDetectionFlag) {
				//if asked to spawn near a last known detection then
				for(int i = 0; i < 20; i++) {
					var newX = LogCSV.instance.lastKnownDetection.x;
					var newY = LogCSV.instance.lastKnownDetection.y;
					newX += (int)Random.Range (-nearSpawnRange.x, nearSpawnRange.x);
					newY += (int)Random.Range (-nearSpawnRange.y, nearSpawnRange.y);
					newX = Mathf.Clamp (newX, 0, pg.width-1);
					newY = Mathf.Clamp (newY, 0, pg.height-1);
					newEndTile = new Vector2 (newX, newY);
					var shortList = pg.gridItems.FindAll (item => item.transform.position.x == newX);
					//shortlist by Y
					var tile = shortList.Find (item => item.transform.position.y == newY);
					Debug.Log("In the loop");
					//activate it
					if(!tile.GetComponent<TileElement>().visited) {
						Debug.Log("Found Unvisited tile");	
						break;
					}
				}
			} else {
				//just new random location
				for(int i = 0; i < 20; i++) {
					newEndTile = new Vector2 (Random.Range(0,(int)widthHeight.x),Random.Range(0,(int)widthHeight.y));
					var shortList = pg.gridItems.FindAll (item => item.transform.position.x == newEndTile.x);
					//shortlist by Y
					var tile = shortList.Find (item => item.transform.position.y == newEndTile.y);
					if(!tile.GetComponent<TileElement>().visited) {
						Debug.Log("Found Unvisited tile");	
						break;
					}
				}
			}

//			endTile = new Vector2 (Random.Range(0,(int)widthHeight.x),Random.Range(0,(int)widthHeight.y));
			endTile = newEndTile;
		}
	}

	IEnumerator Go2EndTile(){
		//flag down
		canStep = false;

		//step Y or X
		if(Mathf.Abs(vecDif.y) >= Mathf.Abs(vecDif.x)){
			//Y value seems larger, minimize that first
			TakeStep(new Vector2(0,Mathf.Sign(vecDif.y)));
		}else{
			//X value seems larger.. minimize that
			TakeStep(new Vector2(Mathf.Sign(vecDif.x),0));
		}

		yield return new WaitForSeconds (stepTime);

		//flag up
		canStep = true;

	}//iEnum

	//take the step
	void TakeStep(Vector2 step){
		var cP = this.transform.position;
		this.transform.position = new Vector3 (cP.x + step.x, cP.y + step.y, cP.z);

		//Activate a grid, if present
		if (null != pg) {
			//shortlist all by X
			var shortList = pg.gridItems.FindAll (item => item.transform.position.x == this.transform.position.x);
			//shortlist by Y
			var tile = shortList.Find (item => item.transform.position.y == this.transform.position.y);
		
			//activate it
			tile.GetComponent<TileElement>().OnMouseUpAsButton();
		}

		//Register 'effort'
		LogCSV.instance.EffortMade();

	}//fn.
}
