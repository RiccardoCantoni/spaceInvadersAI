using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingManager : MonoBehaviour {

	public float FireDelay;

	List<GameObject> aliens=new List<GameObject>();

	public void Initiate () {
		StartCoroutine (commandShoot());
	}

	IEnumerator commandShoot(){
		while (true) {
			yield return new WaitForSeconds (FireDelay);
			aliens.Clear ();
			foreach (Transform child in transform) {
				aliens.Add (child.gameObject);
			}
			if (aliens.Count>0){
				int i = Random.Range (0, aliens.Count);
				alienController shooter = aliens [i].GetComponent<alienController> ();
				shooter.shoot ();
			}
		}
	}

}
