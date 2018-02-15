using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldStage : MonoBehaviour {


	void OnCollisionEnter2D () {
		ShieldScript shield = transform.parent.gameObject.GetComponent<ShieldScript> ();
		shield.isHit ();
	}
}
