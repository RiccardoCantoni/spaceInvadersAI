using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour {

	public float bombFunction;
	public float boundDistance;
	public float shieldDistance;
	public float alienVelocity;
	public float lowestAlienDistance;
	public float lowestAlienLead;
	public float lowestAlienHeight;
	public float closestAlienDistance;
	public float closestAlienHeight;
	public float closestAlienLead;
	public float redShipPresent;
	public float redShipDistance;
	public float redShipLead;
	public float redShipScreen;
	public float danger;
	public float alienDistributionIndex;

	private List <GameObject> bombList= new List<GameObject>();
	private int alienNumber = 55;

	GameObject offsetMarker;
	MovementManager movementManager;
	GameObject currentRedShip;
	GameObject debug;

	public void initSensor(){
		offsetMarker = GameObject.Find("offsetMarker");
		debug = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		movementManager = GameObject.Find ("alienManager").GetComponent<MovementManager> ();
	}

	public void updateSensor(){
		updateBombFunction (transform.position, 6);
		updateBoundDistance ();
		updateShieldDistance ();
		updateAlienVelocity ();
		updateLowestAlienData ();
		updateLowestAlienLead ();
		updateClosestAlienData ();
		updateClosestAlienLead ();
		updateRedShipPresent ();
		if (currentRedShip != null) {
			updateRedShipDistance ();
			updateRedShipLead ();
			updateRedShipScreen ();
		}
		updateDanger (4,10);
		updateAlienDistributionIndex ();
	}
		
	#region bombs

	private void updateBombFunction(Vector3 playerPos, float maxDistance){
		List<Vector3> nearbyBombs = new List<Vector3> ();
		Vector3 nearest = new Vector3(100,100,100);
		float nearestDistance = 100;
		float currentDistance = 100;
		updateBombList ();
		foreach (GameObject b in bombList){
			currentDistance = Vector3.Distance (playerPos, b.transform.position);
			if (currentDistance <= maxDistance) {
				if (currentDistance < nearestDistance) {
					nearest = b.transform.position;
					nearestDistance = currentDistance;
				}
				nearbyBombs.Add (b.transform.position);
			}
		}
		float res = nearest.x;
		nearbyBombs.Remove (nearest);
		bombFunction = res-playerPos.x;
	}

	private void updateBombList(){
		bombList.Clear ();
		bombList.AddRange(GameObject.FindGameObjectsWithTag ("AlienBullet"));
	}

	#endregion

	#region bounds

	private void updateBoundDistance (){
		if (transform.position.x <= 0) {
			boundDistance = (-8.7f) - transform.position.x;
		} else {
			boundDistance =  8.7f - transform.position.x;
		}
	}

	#endregion

	#region shields

	private void updateShieldDistance(){
		shieldDistance = calculateShieldDistance();
	}

	private float calculateShieldDistance(){
		GameObject[] shields = GameObject.FindGameObjectsWithTag ("Shield");
		float closestDistance = 100;
		float shieldDistance = 100;
		foreach (GameObject s in shields) {
			float currentDistance = Mathf.Abs (s.transform.position.x - transform.position.x);
			if (currentDistance < closestDistance) {
				closestDistance = currentDistance;
				shieldDistance = s.transform.position.x - transform.position.x;
			}
		}
		return shieldDistance;
	}

	#endregion

	#region grid
	bool[,] alienGrid = new bool[11,5];  //F -> alive , T-> dead

	public void removeFromGrid (Vector2 pos){
		alienGrid [(int)pos.x,(int)pos.y] = true;
		alienNumber--;
	}

	private bool isAlive(int x, int y){
		if (alienGrid [x, y]==false) return true;
		return false;
	}

	private int getNumberOfRows(){
		bool foundHighest = false;
		int highest = 0; 
		int lowest = 4;
		for (int r = 0; r < 5; r++) {
			if (!foundHighest) {
				if (isAliveRow (r)) {  //highest
					highest = r;
					foundHighest = true;
				}
			} else {
				if (!isAliveRow (r)) { //lowest+1
					lowest = r-1;
					break;
				}
			}
		}
		return lowest - highest + 1;
	}

	private bool isAliveRow(int rowDown){
		for (int i = 0; i < 11; i++) {
			if (isAlive (i, rowDown)) {
				return true;
			}
		}
		return false;
	}

	#endregion

	#region lowest

	void updateLowestAlienData(){
		List<float> lowestLevel = new List<float> ();
		lowestLevel.Clear ();
		int lowestY = 4;
		for (int y = 4; y >= 0; y--) {
			for (int x = 0; x < 11; x++) {
				if (isAlive (x, y)) {
					lowestLevel.Add (MyUtils.gridToVector3(x, y, offsetMarker).x);
				}
			}
			if (lowestLevel.Count > 0) {
				lowestY = y;
				break;
			}
		}
		if (lowestLevel.Count > 0) {  //game ends
			lowestAlienDistance = MyUtils.closest (lowestLevel, transform.position.x) - transform.position.x;
			lowestAlienHeight = MyUtils.gridToVector3 (0, lowestY, offsetMarker).y + 5.3f;
		}
	}

	void updateLowestAlienLead(){
		lowestAlienLead = MyUtils.calculateLead (alienVelocity, lowestAlienHeight, 10f);
	}

	#endregion

	#region closest

	void updateClosestAlienData(){
		Vector3 closest = Vector3.zero;
		float minDistance = 100;
		for (int y = 0 ; y < 5; y++) {
			for (int x = 0; x < 11; x++) {
				if (isAlive (x, y)) {
					Vector3 curPos = MyUtils.gridToVector3 (x, y, offsetMarker);
					float curDistance = Vector3.Distance (transform.position, curPos);
					if (curDistance < minDistance) {
						closest = curPos;
						minDistance = curDistance; 
					}
				}
			}
		}
		closestAlienDistance = closest.x - transform.position.x;
		closestAlienHeight = closest.y - transform.position.y;
	}

	void updateClosestAlienLead(){
		closestAlienLead = MyUtils.calculateLead (alienVelocity, closestAlienHeight, 10f);
	}

	#endregion

	#region alienVelocity

	void updateAlienVelocity(){
		alienVelocity = movementManager.alienVelocityVector.x;
	}

	#endregion

	#region redship

	public void setRedShip(GameObject rs){
		currentRedShip = rs;
	}

	void updateRedShipDistance(){
		redShipDistance = currentRedShip.transform.position.x - transform.position.x;
	}

	void updateRedShipLead(){
		float absoluteLead = MyUtils.calculateLead (currentRedShip.GetComponent<RedShipController> ().velocity, 10.37f, 10);
		if (currentRedShip.GetComponent<RedShipController> ().direction.x > 0) {
			redShipLead = absoluteLead;
		} else {
			redShipLead = (-1)*absoluteLead;
		}
	}

	void updateRedShipScreen(){
		float lead = Mathf.Abs (redShipLead);
		Vector3 boxCenter = new Vector3 (currentRedShip.transform.position.x + redShipLead, 0, -10);
		redShipScreen = Physics2D.OverlapBoxAll (boxCenter, new Vector2 (lead, 8), 0, 1<<8).Length; 
		//Debug.DrawLine (boxCenter + new Vector3 (-lead/2f, 4, 0), boxCenter + new Vector3 (lead/2f, -4, 0));
		//Debug.Log (redShipScreen);

		//Debug.DrawLine (boxCenter + new Vector3 (boxWidth/2f, boxHeight/2f, 0), boxCenter + new Vector3 (-boxWidth/2f, -boxHeight/2f, 0));
	}

	void updateRedShipPresent(){
		if (currentRedShip != null) {
			redShipPresent = 1;
		} else {
			redShipPresent = -1;
		}
	}

	#endregion

	#region danger

	void updateDanger(float boxWidth, float boxHeight){
		Vector3 boxCenter = transform.position+Vector3.up*(boxHeight/2f - 0.5f);
		Collider2D[] bombColliders = Physics2D.OverlapBoxAll (boxCenter, new Vector2 (boxWidth, boxHeight+1), 0, 1 << 10);
		float res = 0;
		foreach (Collider2D col in bombColliders) {
			res += 1 / Vector3.Distance (col.transform.position, transform.position);
		}
		danger = res;
	}


	#endregion

	#region distribution

	void updateAlienDistributionIndex(){
		float rows = getNumberOfRows ();
		float res = (rows*11f / (float)alienNumber);
		if (res > 10) {
			alienDistributionIndex = 10;
		} else {
			alienDistributionIndex = res;
		}
	}

	#endregion

}
