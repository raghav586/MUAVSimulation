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
public class WriteCSV : MonoBehaviour {

	private List<string[]> rowData = new List<string[]>();

	// Use this for initialization
	void Start () {
		Save();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Save(){
		//headers
		string[] rowDataTemp = new string[3];
		rowDataTemp[0] = "Name";
		rowDataTemp[1] = "ID";
		rowDataTemp[2] = "Income";
		rowData.Add(rowDataTemp);

		//TODO: write some data here

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
		return Application.dataPath+"/CSV/" + datetime.ToString("dddd, dd-MMMM-yyyy HH:mm:ss") +".csv";
		#elif UNITY_ANDROID
		return Application.persistentDataPath+"Saved_data.csv";
		#elif UNITY_IPHONE
		return Application.persistentDataPath+"/"+"Saved_data.csv";
		#else
		return Application.dataPath +"/"+"Saved_data.csv";
		#endif
	}

}
