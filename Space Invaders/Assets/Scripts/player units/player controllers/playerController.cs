using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

	public float Velocity;
	public float timeBetweenShots;
	public GameObject bullet;
	public float bound;

	Vector3 initialPos;
	float nextShootingWindow;

	void Start(){
		initialPos = transform.position;
		nextShootingWindow = Time.time;
	}
		
	void Update () {
		float input = Input.GetAxis ("Horizontal");
		Vector3 newpos = transform.position + Vector3.right * input * Velocity * Time.deltaTime;
		if (newpos.x > initialPos.x - bound & newpos.x < initialPos.x + bound) {
			transform.position = newpos;
		}
		if (Input.GetKeyDown ("space")) {
			shoot ();
		}
	}

	void shoot() {
		if (Time.time > nextShootingWindow) {
			Instantiate (bullet, transform.position, transform.rotation);
			nextShootingWindow = Time.time + timeBetweenShots;
		}
	}

}
