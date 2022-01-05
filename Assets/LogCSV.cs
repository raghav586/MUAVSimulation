using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

/// <summary>
/// Write CS.
/// https://sushanta1991.blogspot.com/2015/02/how-to-write-data-to-csv-file-in-unity.html
/// </summary>
public class LogCSV : MonoBehaviour {

	private List<string[]> rowData = new List<string[]>();

	public bool recentDetectionFlag = false;
	public float downRecentDetectionFlag = 10f;
	public Vector2 lastKnownDetection;

	public int seconds;
	public int effort;
	public int discover;

//	public List<GameObject> discoveredPeople;

	//Singleton pattern
	public static LogCSV instance;
	void Awake(){
		instance = this;
	}

	// Use this for initialization
	void Start () {
		WriteHeader ();
	}
	
	// Update is called once per frame
	void Update () {
		seconds = (int)Time.realtimeSinceStartup;	
	}

	//on disable/distroy.. write the file.
	void OnDisable(){
		WriteFile ();
	}

	public void EffortMade(){
		effort++;
//		WriteLine ();
	}

	public void DiscoveryMade(){
		discover++;
		WriteLine ();
		StartCoroutine ("DetectionFlagManage");
	}

	IEnumerator DetectionFlagManage(){
		recentDetectionFlag = true;
		yield return new WaitForSeconds (downRecentDetectionFlag);
		recentDetectionFlag = false;
	}

	public void WriteHeader(){
		//headers
		string[] rowDataTemp = new string[3];
		rowDataTemp[0] = "Time";
		rowDataTemp[1] = "Effort";
		rowDataTemp[2] = "Discovery";
		rowData.Add(rowDataTemp);
	}

	public void WriteLine(){
		//TODO: write some data here
		string[] rowDataTemp = new string[3];
		rowDataTemp [0] = seconds.ToString ();
		rowDataTemp [1] = effort.ToString ();
		rowDataTemp [2] = discover.ToString ();
		rowData.Add(rowDataTemp);
	}

	public void WriteFile(){
		//parse for output
		string filePath = getPath();

		string[][] output = new string[rowData.Count][];
		for(int i = 0; i < output.Length; i++){
			output[i] = rowData[i];
		}
		int length = output.GetLength(0);
		string delimiter = ",";
		StringBuilder sb = new StringBuilder();
		for (int index = 0; index < length; index++)
			sb.AppendLine(string.Join(delimiter, output[index]));

		//create file and close
		StreamWriter outStream = System.IO.File.CreateText(filePath);
		outStream.WriteLine(sb);
		outStream.Close();
	}

	// Following method is used to retrive the relative path as device platform
	private string getPath(){
		var datetime = System.DateTime.Now;
		#if UNITY_EDITOR
		return Application.dataPath+"\\CSV\\" + datetime.ToString("dddd, dd-MMMM-yyyy HH-mm-ss") +".csv";
		#elif UNITY_ANDROID
		return Application.persistentDataPath+"Saved_data.csv";
		#elif UNITY_IPHONE
		return Application.persistentDataPath+"/"+"Saved_data.csv";
		#else
		return Application.dataPath +"/"+"Saved_data.csv";
		#endif
	}

}
