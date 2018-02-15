using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMover : MonoBehaviour {

	public float velocity;
	Rigidbody2D rb;

	void Start(){
		rb = gameObject.GetComponent<Rigidbody2D> ();
	}

	void Update () {
		rb.MovePosition (rb.position + Vector2.up * velocity * Time.deltaTime);
	}
}
