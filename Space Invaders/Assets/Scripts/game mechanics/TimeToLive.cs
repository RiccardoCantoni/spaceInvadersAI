using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToLive : MonoBehaviour {

	public float time;

	void Start(){
		StartCoroutine (selfDestruct());
	}


	IEnumerator selfDestruct(){
		yield return new WaitForSeconds (time);
		Destroy (gameObject);
	}
}
