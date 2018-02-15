using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public int alienScore;
	public int redShipScore;

	public int totalScore;
	Text scoreText;
	GameManager gm;

	void Start () {
		totalScore = 0;
		scoreText = GameObject.Find ("ScoreText").GetComponent<Text>();
		gm = GameObject.Find ("GameMaster").GetComponent<GameManager> ();
	}

	public void updateScore(GameObject obj){
		if (obj.tag == "Alien") {
			totalScore += alienScore;
			gm.alienDead ();
		} else {
			totalScore += redShipScore;
		}
		scoreText.text = "SCORE: " + totalScore;
	}

}
