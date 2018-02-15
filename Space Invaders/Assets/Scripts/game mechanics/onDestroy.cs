using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onDestroy : MonoBehaviour {

	void OnDestroy(){
		Score score = GameObject.Find ("score").GetComponent<Score> ();
		score.updateScore (gameObject);
	}
}
