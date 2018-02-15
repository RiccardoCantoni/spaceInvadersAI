using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFuzzyController : MonoBehaviour {

	public float horizontalAxisInput;
	public bool spacebarInput;
	public int behaviourIndex;
	public GameObject behaviourDisplay, selectorDisplay;

	BehaviourSelector behaviourSelector = new BehaviourSelector ();
	FuzzyBehaviour avoidBehavior = new Avoid ();
	FuzzyBehaviour shelterBehavior = new Shelter ();
	FuzzyBehaviour atkRedShipBehaviour = new AttackRedShip ();
	FuzzyBehaviour atkClosestBehaviour = new AttackClosest ();
	FuzzyBehaviour atkLowestBehaviour = new AttackLowest ();
	FuzzyBehaviour currentBehaviour;

	GameObject marker;
	Sensor sensor;

	void Start(){
		sensor = gameObject.GetComponent<Sensor>();
		sensor.initSensor ();
		behaviourSelector.init (sensor, selectorDisplay);
		avoidBehavior.init (sensor, behaviourDisplay);
		shelterBehavior.init (sensor, behaviourDisplay);
		atkRedShipBehaviour.init (sensor, behaviourDisplay);
		atkClosestBehaviour.init (sensor, behaviourDisplay);
		atkLowestBehaviour.init (sensor, behaviourDisplay);
	}

	void Update(){
		sensor.updateSensor ();
		behaviourSelector.selectBehaviour ();
		switch (behaviourSelector.behaviourIndex) {
		case 0:
			currentBehaviour = avoidBehavior;
			break;
		case 1:
			currentBehaviour = shelterBehavior;
			break;
		case 2:
			currentBehaviour = atkRedShipBehaviour;
			break;
		case 3:
			currentBehaviour = atkClosestBehaviour;
			break;
		case 4:
			currentBehaviour = atkLowestBehaviour;
			break;
		default:
			Debug.Log ("index error");
			Time.timeScale = 0;
			break;
		}
		currentBehaviour.updateOutputs ();
		horizontalAxisInput = currentBehaviour.movementOutput;
		spacebarInput = currentBehaviour.spacebarOutput;
		if (Mathf.Abs (currentBehaviour.movementOutput) > 1) {
			Debug.Log ("input error, behaviour " + behaviourIndex);
		}
	}

}
