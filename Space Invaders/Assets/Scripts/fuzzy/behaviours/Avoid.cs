using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avoid : FuzzyBehaviour{

	static float maxDistance = 6;

	#region bombs

	FuzzyClass bombsFarLeft = new FuzzyClass (
		(-1) * maxDistance - 1, 
		(-1) * maxDistance, 
		maxDistance / -2f - 0.5f, 
		maxDistance / -2f + 0.5f);
	FuzzyClass bombsLeft = new FuzzyClass (
		maxDistance / -2f - 0.5f,
		maxDistance / -2f + 0.5f,
		-0.1f,
		0.5f);
	FuzzyClass bombsRight = new FuzzyClass (
		0,
		0.1f,
		maxDistance / 2f - 0.5f,
		maxDistance / 2f + 0.5f);
	FuzzyClass bombsFarRight = new FuzzyClass (
		maxDistance / 2f - 0.5f,
		maxDistance / 2f + 0.5f,
		maxDistance,
		maxDistance + 1);
	
	#endregion

	#region bounds

	float bfl, bl, br, bfr;

	FuzzyClass boundCloseLeft = new FuzzyClass (
        -3,
        -0.5f,
        -0.1f,
        0);
	FuzzyClass boundCloseRight = new FuzzyClass (
         0,
         0.1f,
         0.5f,
         3);

	float bdl, bdr;

	#endregion

	float hardLeft, left, right, hardRight;

	public override void updateOutputs(){
		//fuzzyficazione
		bfl = bombsFarLeft.calculateMembership (sensor.bombFunction);
		bl = bombsLeft.calculateMembership (sensor.bombFunction);
		br = bombsRight.calculateMembership (sensor.bombFunction);
		bfr = bombsFarRight.calculateMembership (sensor.bombFunction);

		bdl = boundCloseLeft.calculateMembership (sensor.boundDistance);
		bdr = boundCloseRight.calculateMembership (sensor.boundDistance);

		//FAM
		hardLeft = FuzzyLib.fuzzyOr(br, bdr);
		left = bfr;
		right = bfl;
		hardRight = FuzzyLib.fuzzyOr(bl, bdl);

		//defuzzyficazione
		movementOutput = hardLeft * (-0.8f) + left * (-0.4f) + right * (0.4f) + hardRight * (0.8f);

		//display
		List<string> message = new List<string>();
		message.Add ("==AVOID==");
		message.Add ("bombs distance:");
		message.Add (bfl.ToString("F2") + "|" + bl.ToString("F2") + "|" + br.ToString("F2") + "|" + bfr.ToString("F2"));
		message.Add ("bound distance:");
		message.Add (bdl.ToString ("F2") + "|" + bdr.ToString ("F2"));
		message.Add ("movement:");
		message.Add (hardLeft.ToString("F2") + "|" + left.ToString("F2") + "|" + right.ToString("F2") + "|" + hardRight.ToString("F2"));
		message.Add ("shooting");
		message.Add ("false");

		setDisplayMessage (message);

	}

}
