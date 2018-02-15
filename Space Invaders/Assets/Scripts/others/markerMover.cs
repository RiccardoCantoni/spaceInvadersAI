using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class markerMover : MonoBehaviour {

	MovementManager mvm;
	Rigidbody2D rb ;

	void Start(){
		mvm = GameObject.Find ("alienManager").GetComponent<MovementManager> ();
		rb = gameObject.GetComponent<Rigidbody2D> ();
	}

	void Update () {
		rb.MovePosition (rb.position + mvm.alienVelocityVector*Time.deltaTime);
	}

}
