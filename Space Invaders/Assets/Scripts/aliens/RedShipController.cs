using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedShipController : MonoBehaviour {

	public float velocity;
	public Vector2 direction;

	Rigidbody2D rb;

	void Start(){
		rb = gameObject.GetComponent<Rigidbody2D> ();
	}

	public void initiate(Vector3 dir){
		direction = new Vector2 (dir.x, dir.y);
	}

	void Update () {
		rb.MovePosition (rb.position + direction * velocity * Time.deltaTime);
	}

}
