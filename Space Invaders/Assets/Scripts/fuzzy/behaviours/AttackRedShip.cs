using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRedShip : FuzzyBehaviour {

	#region targetPosition

	FuzzyClass targetCloseRight = new FuzzyClass (0, 0.5f, 1, 3);
	FuzzyClass targetRight = new FuzzyClass (1, 3, 5, 7);
	FuzzyClass targetFarRight = new FuzzyClass (5, 7, 16, 17);
	FuzzyClass targetCloseLeft = new FuzzyClass (-3, -1, -0.5f, 0);
	FuzzyClass targetLeft = new FuzzyClass (-7, -5, -3, -1);
	FuzzyClass targetFarLeft = new FuzzyClass (-17, -16, -7, -5);

	#endregion

	#region shield

	CrispClass underShield = new CrispClass (-0.8f, 0.8f);

	#endregion

	public override void updateOutputs(){

		//variabile composta
		float targetPosition = sensor.redShipDistance + sensor.redShipLead;

		//fuzzyficazione
		float tgp, tgpp, tgppp, tgn, tgnn, tgnnn;
		tgp = targetCloseRight.calculateMembership (targetPosition);
		tgpp = targetRight.calculateMembership (targetPosition);
		tgppp = targetFarRight.calculateMembership (targetPosition);
		tgn = targetCloseLeft.calculateMembership (targetPosition);
		tgnn = targetLeft.calculateMembership (targetPosition);
		tgnnn = targetFarLeft.calculateMembership (targetPosition);

		float shield;
		shield = underShield.calculateMembership (sensor.shieldDistance);

		//FAM

		float hardLeft, left, right, hardRight;

		hardLeft = FuzzyLib.fuzzyOr (tgn, FuzzyLib.fuzzyOr (tgnn, tgnnn));
		hardRight = FuzzyLib.fuzzyOr (tgp, FuzzyLib.fuzzyOr (tgpp, tgppp));
		left = 0;
		right = 0;

		//defuzzyficazione
		movementOutput = -0.8f*hardLeft -0.4f*left + 0.4f*right + 0.8f*hardRight ;
		spacebarOutput = FuzzyLib.toBool (FuzzyLib.fuzzyNot(shield));

		//display
		List<string> message = new List<string>();
		message.Add ("==ATK RED SHIP==");
		message.Add ("target distance:");
		message.Add (tgnnn.ToString("F2") + "|" + tgnn.ToString ("F2") + "|" + tgn.ToString ("F2") + (" l"));
		message.Add (tgp.ToString("F2") + "|" + tgpp.ToString("F2") + "|" + tgppp.ToString("F2") + (" r"));
		message.Add ("shooting");
		message.Add (spacebarOutput.ToString());

		setDisplayMessage (message);

	}

}