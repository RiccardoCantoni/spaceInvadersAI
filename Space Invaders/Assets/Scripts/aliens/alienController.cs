using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alienController : MonoBehaviour {

	public GameObject bomb;
	public Vector2 gridpos;

	MovementManager mvm;
	Rigidbody2D rb ;
	GameObject bombHolder;

	void Start(){
		mvm = transform.root.GetComponent<MovementManager> ();
		rb = gameObject.GetComponent<Rigidbody2D> ();
	}

	void Update () {
		rb.MovePosition (rb.position + mvm.alienVelocityVector*Time.deltaTime);
	}

	public void shoot(){
		Instantiate (bomb, transform.position+(Vector3.back), transform.rotation);
	}
		
}
