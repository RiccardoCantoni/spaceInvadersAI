using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour {

	Sensor sensor;
	GameManager gm;

	void Start(){
		gm = GameData.gameManager;
	}

	void OnCollisionEnter2D (Collision2D col){
		if (gameObject.tag == "Alien") {
			Score score = GameObject.Find ("score").GetComponent<Score> ();
			score.updateScore (gameObject);
			MovementManager mm = GameObject.Find ("alienManager").GetComponent<MovementManager> ();
			mm.increaseGameSpeed ();
			if (!GameData.gameHuman) {
				Sensor s = GameObject.Find ("autoPlayer").GetComponent<Sensor> ();
				s.removeFromGrid (gameObject.GetComponent<alienController> ().gridpos);
			}
			ExplosionManager e = gameObject.GetComponent<ExplosionManager> ();
			e.explode();
		}

		if (gameObject.tag == "RedShip") {
			Score score = GameObject.Find ("score").GetComponent<Score> ();
			score.updateScore (gameObject);
			if (!GameData.gameHuman) {
				if (sensor == null) {
					sensor = GameObject.Find ("autoPlayer").GetComponent<Sensor> ();
				}
				sensor.setRedShip (null);
			}
		}

		if (gameObject.tag == "FinishLine" && col.gameObject.tag == "Alien") {
			gm.startGameAuto ();
		}

		if (gameObject.tag == "Player") {
			gm.playerDead ();
		}

		if (gameObject.tag != "FinishLine") {
			Destroy (gameObject);
		}
	}
}
