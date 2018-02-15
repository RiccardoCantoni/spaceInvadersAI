using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuzzyBehaviour {

	public bool spacebarOutput;
	public float movementOutput;
	protected Sensor sensor;
	protected GameObject displayObject;

	public virtual void updateOutputs (){
	}
		
	public void init(Sensor s, GameObject display){
		this.sensor = s;
		this.displayObject = display;
	}

	protected void setDisplayMessage(List<string> msg){
		displayObject.GetComponent<Display> ().stringList = msg;
	}

}






