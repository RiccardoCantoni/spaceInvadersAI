using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelter : FuzzyBehaviour {

	FuzzyClass shieldFarLeft = new FuzzyClass (-16f, -15f, -2f, -1.5f);
	FuzzyClass shieldLeft = new FuzzyClass (-2.5f, -2f, -0.2f, 0.1f);
	FuzzyClass shieldRight = new FuzzyClass (-0.1f, 0.2f, 2f, 2.5f);
	FuzzyClass shieldFarRight = new FuzzyClass (1.5f, 2f, 15f, 16f);

	float sfl, sl, sr, sfr;

	float hardLeft, left, right, hardRight;

	public override void updateOutputs(){
		
		//fuzzyficazione
		sfl = shieldFarLeft.calculateMembership(sensor.shieldDistance);
		sl = shieldLeft.calculateMembership (sensor.shieldDistance);
		sr = shieldRight.calculateMembership (sensor.shieldDistance);
		sfr = shieldFarRight.calculateMembership (sensor.shieldDistance);

		//FAM
		hardLeft = FuzzyLib.fuzzyOr (sl,sfl);
		left = 0;
		right = 0;
		hardRight = FuzzyLib.fuzzyOr (sr,sfr);

		//defuzzyficazione
		spacebarOutput = false;
		movementOutput = hardLeft * (-0.8f) + left * (-0.4f) + right * (0.4f) + hardRight * (0.8f);

		//display
		List<string> message = new List<string>();
		message.Add ("==SHELTER==");
		message.Add ("shield distance:");
		message.Add (sfl.ToString("F2") + "|" + sl.ToString ("F2") + "|" + sr.ToString ("F2") + "|" + sfr.ToString ("F2") );
		message.Add ("movement:");
		message.Add (hardLeft.ToString("F2") + "|" + left.ToString("F2") + "|" + right.ToString("F2") + "|" + hardRight.ToString("F2"));
		message.Add ("shooting");
		message.Add (spacebarOutput.ToString());

		setDisplayMessage (message);

	}
}
