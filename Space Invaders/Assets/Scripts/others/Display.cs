using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display : MonoBehaviour {

	public string title;
	public List<string> stringList;

	Text text;

	void Start () {
		stringList = new List<string> ();
		text = gameObject.transform.GetChild (0).transform.GetChild (0).GetComponent<Text> ();
	}

	void Update () {
		string newText = title + "\n";
		foreach (string s in stringList) {
			newText = newText + (s + "\n");
		}
		text.text = newText;
	}
}
