using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playfield{
	//The grid itself
	public static int w = 10;
	public static int h = 13;
	public static Element[,] elements = new Element[w, h];

	public static void uncoverMines(){
		foreach (Element elem in elements)
			if (elem.mine)
				elem.loadTexture (0);
	}

	//Find out if a mine is at the coordinates
	public static bool mineAt(int x, int y){
		if (x >= 0 && y >= 0 && x < w && y < h)
			return elements[x, y].mine;
		else
			return false;
	}

	//Count adjacent mines for an element
	public static int adjacentMines(int x, int y){
		int count = 0;

		//count mines
		for (int i = -1; i <= 1; i++) { //x  -1,0,1
			for(int j = -1;j<=1;j++){	//y, -1,0,1
				if (!(i==0 && j==0)){	//avoid 'self', i.e x and y both 0
					if(mineAt(x+i,y+j)) count++;	//increment count
				}//NOT i==j==0
			}//j
		}//i

		return count;
	}

	//FloodFill empty elements
	public static void FFuncover(int x, int y, bool[,] visited){
		//Coordinates within range
		if (x >= 0 && y >= 0 && x < w && y < h){
			//visited already ? then exit
			if (visited [x, y])
				return;

			//uncover element
			elements[x,y].loadTexture(adjacentMines(x,y));

			if (adjacentMines (x, y) > 0)
				return;

			//set visited flag
			visited[x,y] = true;

			//recursion
			FFuncover(x-1,y,visited);
			FFuncover (x + 1, y, visited);
			FFuncover (x, y - 1, visited);
			FFuncover (x, y + 1, visited);
		}
	}

	//find out if all mines have been uncovered
	public static bool isFinished(){
		foreach (Element elem in elements)
			if (elem.isCovered () && !elem.mine)
				return false;
		// there are none => all are mines => game won
		return true;
	}

}
