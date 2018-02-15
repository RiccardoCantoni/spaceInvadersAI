using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public bool logging;

	int alienCount;

	void Awake(){
		GameData.gameManager = this;
		if (GameObject.FindObjectsOfType (GetType ()).Length>1) {
			Destroy (gameObject);
		}
	}

	public void startGameAuto () {
		Cursor.visible = false;
		alienCount = 55;
		GameData.gameHuman = false;
		SceneManager.LoadScene ("auto");
		GameData.startTime = Time.time;
	}

	public void startGameHuman () {
		Cursor.visible = false;
		alienCount = 55;
		GameData.gameHuman = true;
		SceneManager.LoadScene ("human");
		GameData.startTime = Time.time;
	}

	public void endGame (bool won, bool human, int finalScore){
		GameData.gameDuration = Time.time - GameData.startTime;
		GameData.gameWon = won;
		GameData.gameHuman = human;
		GameData.gameScore = finalScore;
		GameData.hasData = true;
		if (logging)
			logLastGame ("log.txt");
		Cursor.visible = true;
		SceneManager.LoadScene ("menu");
	}

	public void alienDead(){
		alienCount--;
		if (alienCount == 0) {
			Score score = GameObject.Find ("score").GetComponent<Score>();
			endGame (true, GameData.gameHuman, score.totalScore);
		}
	}

	public void playerDead() {
		Score score = GameObject.Find ("score").GetComponent<Score>();
		endGame (false, GameData.gameHuman, score.totalScore);
	}
		
	private void logLastGame(string filename){
		MyUtils.appendToFile(filename, "-----");
		MyUtils.appendToFile(filename, "won: " + GameData.gameWon.ToString ());
		MyUtils.appendToFile(filename, "human: " + GameData.gameHuman.ToString ());
		MyUtils.appendToFile(filename, "score: " + GameData.gameScore.ToString ());
		MyUtils.appendToFile(filename, "duration: " + GameData.gameDuration.ToString ("F2"));
	}
		
}
