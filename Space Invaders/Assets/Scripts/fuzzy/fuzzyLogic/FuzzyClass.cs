using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuzzyClass {

	float p1, p2, p3, p4;

	public FuzzyClass(float p1, float p2, float p3, float p4){
		this.p1 = p1;
		this.p2 = p2;
		this.p3 = p3;
		this.p4 = p4;
	}

	public FuzzyClass(float p1, float p2, float p3){
		this.p1 = p1;
		this.p2 = p2;
		this.p3 = p2;
		this.p4 = p3;
	}

	public float calculateMembership(float inputValue){
		float ratio;
		if (inputValue <= p1 | inputValue >= p4) {  
			return 0;
		}

		if (inputValue > p1 & inputValue < p2 ) {
			ratio = Mathf.Abs(inputValue - p1) / Mathf.Abs(p2 - p1);
			return (Mathf.Lerp(0,1,ratio));
		}

		if (inputValue >= p2 & inputValue <= p3) {
			return 1;
		}

		if (inputValue > p3 & inputValue < p4) {
			ratio = Mathf.Abs(inputValue - p3) / Mathf.Abs(p4 - p3);
			return (Mathf.Lerp(1,0,ratio));
		}
		return 0;
	}

	public static FuzzyClass mirrorPositive(FuzzyClass x){
		return new FuzzyClass (x.p4 * (-1), x.p3 * (-1), x.p2 * (-1), x.p1 * (-1));
	}


		
}
