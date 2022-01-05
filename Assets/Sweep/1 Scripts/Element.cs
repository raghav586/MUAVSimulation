using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour {

	//weather this element is a mine or not
	public bool mine;

	//Different textures
	public Sprite[] emptyTextures;
	public Sprite mineTexture;

	// Use this for initialization
	void Start () {
		//randomly decide if this is a mine or not (looking for 15% probability)
		mine = Random.value < 0.15;

		//register itself in the grid
		int x = (int)transform.position.x;
		int y = (int)transform.position.y;
		Playfield.elements [x, y] = this;
	}
	
	//Load another texture
	public void loadTexture(int adjacentCount){
		if (mine)
			GetComponent<SpriteRenderer> ().sprite = mineTexture;
		else
			GetComponent<SpriteRenderer> ().sprite = emptyTextures[adjacentCount];
	}

	//Is it still covered ?
	public bool isCovered(){
		return GetComponent<SpriteRenderer> ().sprite.texture.name == "default";	
	}

	//This gets called on mouse click
	public void OnMouseUpAsButton(){
		if (mine) {
			//Uncover all mines
			Playfield.uncoverMines();
			print("you lose");
		} else {
			//--- Show adjacent mine number ---
			int x = (int)transform.position.x;
			int y = (int)transform.position.y;
			loadTexture (Playfield.adjacentMines (x, y));

			//--- Uncover area without mines
			Playfield.FFuncover(x,y,new bool[Playfield.w, Playfield.h]);

			//--- find out of the game was won ---
			if (Playfield.isFinished ())
				print ("You won");
		}
	}

}
