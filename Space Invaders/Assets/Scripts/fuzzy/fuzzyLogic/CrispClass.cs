using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrispClass{

	float p1, p2;

	public CrispClass(float p1, float p2){
		this.p1 = p1;
		this.p2 = p2;
	}

	public float calculateMembership(float inputValue){
		if (inputValue <= p1)
			return 0;
		if (inputValue < p2)
			return 1;
		return 0;
	}


}
