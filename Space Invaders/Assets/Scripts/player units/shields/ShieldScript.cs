using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour {

	public int hp;
	public GameObject st1;
	public GameObject st2;
	public GameObject st3;

	GameObject curStage;

	void Start () {
		hp = 3;
		curStage=Instantiate (st1, transform.position, transform.rotation);
		curStage.transform.parent = transform;
	}
	
	public void isHit(){
		hp--;
		Destroy (curStage);
		switch (hp) {
		case 2:
			curStage = Instantiate (st2, transform.position + Vector3.down*0, transform.rotation);
			curStage.transform.parent = transform;
			break;
		case 1:
			curStage = Instantiate (st3, transform.position + Vector3.down*0, transform.rotation);
			curStage.transform.parent = transform;
			break;
		case 0:
			Destroy (gameObject);
			break;
		}
	}
		
}
