using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour {

	public float initialVerticalSpeed;
	public float finalVerticalSpeed;
	public float initialHorizontalSpeed;
	public float finalHorizontalSpeed;


	float horizontalSpeedIncrease;
	float verticalSpeedIncrease;

	public Vector2 alienVelocityVector;

	void Start(){
		horizontalSpeedIncrease = (finalHorizontalSpeed - initialHorizontalSpeed) / 55f;
		verticalSpeedIncrease = (finalVerticalSpeed - initialVerticalSpeed) / 55f;
	}

	public void DownLeft(float timeDown){
		StartCoroutine (DownLeftCoroutine (timeDown));
	}

	public void DownRight(float timeDown){
		StartCoroutine (DownRightCoroutine (timeDown));
	}

	IEnumerator DownRightCoroutine(float timeDown){
		alienVelocityVector = (Vector3.down * initialVerticalSpeed);
		yield return new WaitForSeconds (timeDown);
		alienVelocityVector = (Vector3.right * initialHorizontalSpeed);
	}

	IEnumerator DownLeftCoroutine(float timeDown){
		alienVelocityVector = (Vector3.down * initialVerticalSpeed);
		yield return new WaitForSeconds (timeDown);
		alienVelocityVector = (Vector3.left * initialHorizontalSpeed);
	}

	public void initiate (Vector3 vector){
		alienVelocityVector = (vector*initialHorizontalSpeed);
	}

	public void increaseGameSpeed(){
		initialVerticalSpeed += verticalSpeedIncrease;
		initialHorizontalSpeed += horizontalSpeedIncrease;
		updateVelocityVector ();
		}

	private void updateVelocityVector(){
		Vector3 dir = alienVelocityVector.normalized;
		if (dir.x == 0) { // up down
			alienVelocityVector = dir * initialVerticalSpeed;
		} else {	//left right
			alienVelocityVector = dir * initialHorizontalSpeed;
		}
	}
}
