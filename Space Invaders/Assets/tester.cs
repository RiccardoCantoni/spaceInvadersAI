using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class tester : MonoBehaviour {


	// Use this for initialization
	void Start () {
		string str = "test test line1"+Environment.NewLine;
		string str2 = "test line2";
		MyUtils.appendToFile ("output.txt", str);
		MyUtils.appendToFile ("output.txt", str2);
		print ("done");

	}
	

}
