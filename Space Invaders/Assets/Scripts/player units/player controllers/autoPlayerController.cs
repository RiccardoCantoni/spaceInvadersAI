using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoPlayerController: MonoBehaviour {

	public float Velocity;
	public float timeBetweenShots;
	public GameObject bullet;
	public float bound;

	public float input;

	float nextShootingWindow;

	MainFuzzyController MFC;

	void Start(){
		nextShootingWindow = Time.time;
		MFC = gameObject.GetComponent<MainFuzzyController> ();
	}

	void Update () {
		input = MFC.horizontalAxisInput;
		Vector3 newpos = transform.position + Vector3.right * input * Velocity * Time.deltaTime;
		if (newpos.x > (-1f*bound) & newpos.x < bound) {
			transform.position = newpos;
		}
		if (MFC.spacebarInput) {
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
