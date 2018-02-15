using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRestarter : MonoBehaviour {

	void Start () {
		StartCoroutine (restartCoroutine (1));
	}
	
	IEnumerator restartCoroutine(float time){
		yield return new WaitForSeconds (time);
		GameObject.Find ("MainMenu").GetComponent<ButtonScript> ().buttonClicked (2);
	}
}
