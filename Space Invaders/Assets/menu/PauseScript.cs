using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour {

	bool paused;

	GameObject menu;

	void Start(){
		menu = gameObject.transform.GetChild (0).gameObject;
	}

	void Update () {
		if (Input.GetKeyDown ("escape")) {
			if (SceneManager.GetActiveScene ().buildIndex > 0) {
				if (paused) {
					unpause (1);
				} else {
					pause (1);
				}
			}
		}
	}

	public void pause(int trash){
		Cursor.visible = true;
		Time.timeScale = 0;
		paused = true;
		menu.SetActive (true);
	}
		
	public void unpause(int trash){
		Cursor.visible = false;
		menu.SetActive (false);
		paused = false;
		Time.timeScale = 1;
	}

	public void backToMenu(int trash){
		unpause (0);
		GameData.gameManager.endGame (false, GameData.gameHuman, GameObject.Find ("score").GetComponent<Score> ().totalScore);
	}
}
