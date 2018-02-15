using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour {

	public GameObject explosion;

	public void explode(){
		Instantiate (explosion, transform.position, transform.rotation);
	}

}
