using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LastGameText : MonoBehaviour {

	Text text;
	string firstLine = "last game results: \n";
	string playerType, gameResult, score, time;

	void Start(){
		if (GameData.hasData) {
			text = gameObject.GetComponent<Text> ();
			playerType = GameData.gameHuman ? "human" : "auto";
			gameResult = GameData.gameWon ? "victory" : "defeat";
			score = GameData.gameScore.ToString ();
			time = GameData.gameDuration.ToString ("F2");
			text.text = 
			firstLine +
			"player: " + playerType + "\n" +
			"result: " + gameResult + "\n" +
			"score: " + score + "\n" +
			"game duration: " + time;
		}
	}
			
}
