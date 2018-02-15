using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedShipSpawner : MonoBehaviour {

	public float spawnTimer;
	private float timerOffset;
	public GameObject redShip;

	GameObject newShip;
	Sensor sensor;

	void Start () {
		StartCoroutine (spawnCoroutine ());
		if (!GameData.gameHuman) {
			sensor = GameObject.Find ("autoPlayer").GetComponent<Sensor> ();
		}
	}
		
	IEnumerator spawnCoroutine(){
		while (true) {
			timerOffset = Random.value * 3f;
			yield return new WaitForSeconds (spawnTimer + timerOffset);
			spawnRandomised ();
		}
	}

	void spawnRandomised(){
		int x = Random.Range(0,2);
		if (x == 0) {
			newShip = Instantiate (redShip, transform.position + new Vector3 (-8.3f, 0, 0), transform.rotation);
			RedShipController rsc = newShip.GetComponent<RedShipController> ();
			rsc.initiate (Vector3.right);
		} else {
			newShip = Instantiate (redShip, transform.position + new Vector3 (8.3f, 0, 0), transform.rotation);
			RedShipController rsc = newShip.GetComponent<RedShipController> ();
			rsc.initiate (Vector3.left);
		}
		if (!GameData.gameHuman) {
			sensor.setRedShip (newShip);
		}
	}

}
