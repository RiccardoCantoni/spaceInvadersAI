using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridManager : MonoBehaviour {

	public GameObject alien;
	public Vector3 startPos;
	public float cellWidth;
	public float cellHeight;
	public float rightBound;
	public float leftBound;
	public float timeDown;

	int state = 0; //right
	int rows = 5;
	int columns = 11;
	MovementManager movementmanager;
	ShootingManager shootingmanager;


	void Start () {
		spawnGrid ();
		shootingmanager = gameObject.GetComponent<ShootingManager> ();
		shootingmanager.Initiate ();
		movementmanager = gameObject.GetComponent<MovementManager> ();
		movementmanager.initiate (Vector3.right);
	}
	
	public void spawnGrid(){
		GameObject a;
		alienController script;
		Vector3 pos = startPos;
		for (int r = 0; r < rows; r++) {
			for (int c = 0; c < columns; c++) {
				a=Instantiate (alien, pos, transform.rotation);
				a.transform.parent = transform;
				script = a.GetComponent<alienController> ();
				script.gridpos = new Vector2 (c,r);
				pos += Vector3.right * cellWidth;
			}
			pos.x = startPos.x;
			pos += Vector3.down * cellHeight;
		}
	}

	void Update(){
		foreach (Transform child in transform) {
			if (state==0 && child.transform.position.x>=rightBound){
				state = 1;
				movementmanager.DownLeft (timeDown);
			}

			if (state==1 & child.transform.position.x<=leftBound){
				state = 0;
				movementmanager.DownRight (timeDown);
			}
		}
	}
}
