using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLowest : FuzzyBehaviour {
	
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

	#region alienSpeed

	FuzzyClass speedLowPositive = new FuzzyClass (0, 0.75f, 1.15f, 1.45f);
	FuzzyClass speedMediumPositive = new FuzzyClass (1.15f, 1.45f, 1.95f, 2.35f);
	FuzzyClass speedHighPositive = new FuzzyClass (1.95f, 2.35f, 2.75f, 3);
	FuzzyClass speedLowNegative = new FuzzyClass (-1.45f, -1.15f, -0.75f, 0);
	FuzzyClass speedMediumNegative = new FuzzyClass (-2.35f, -1.95f, -1.45f, 1.15f);
	FuzzyClass speedHighNegative = new FuzzyClass (-3, -2.75f, -2.35f, -1.95f);

	#endregion

	public override void updateOutputs(){

		//variabile composta
		float targetPosition = sensor.lowestAlienDistance + sensor.lowestAlienLead;

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

		float sp, spp, sppp, sn, snn, snnn;
		sp = speedLowPositive.calculateMembership (sensor.alienVelocity);
		spp = speedMediumPositive.calculateMembership (sensor.alienVelocity);
		sppp = speedHighPositive.calculateMembership (sensor.alienVelocity);
		sn = speedLowNegative.calculateMembership (sensor.alienVelocity);
		snn = speedMediumNegative.calculateMembership (sensor.alienVelocity);
		snnn = speedHighNegative.calculateMembership (sensor.alienVelocity);

		//FAM
		float hardRight, right, hardLeft, left;
		float tgMovingRight, tgMovingLeft;
		float tgIsRight, tgIsLeft;

		// tgppp 
		//OR 
		// tgpp & (mvright | sn) 
		//OR
		// shield & isright
		tgMovingRight = FuzzyLib.fuzzyOr(sp, spp, sppp);
		tgIsRight = FuzzyLib.fuzzyOr (tgp, tgpp, tgppp);
		hardRight = tgppp;
		hardRight = FuzzyLib.fuzzyOr(hardRight, FuzzyLib.fuzzyAnd(tgpp, FuzzyLib.fuzzyOr(tgMovingRight, sn)));
		hardRight = FuzzyLib.fuzzyOr(hardRight, FuzzyLib.fuzzyAnd (shield, tgIsRight));

		// tgp 
		//OR
		// (tgpp & (tgnn | tgnnn))
		tgMovingLeft = FuzzyLib.fuzzyOr(tgnn, tgnnn);
		right = FuzzyLib.fuzzyOr (tgp, FuzzyLib.fuzzyAnd (tgpp, tgMovingLeft));
		right = FuzzyLib.fuzzyAnd (right, FuzzyLib.fuzzyNot (shield));

		// tgn
		//OR
		// (tgnn & (tgpp | tgppp))
		tgMovingRight = FuzzyLib.fuzzyOr(tgpp, tgppp);
		left = FuzzyLib.fuzzyOr (tgn, FuzzyLib.fuzzyAnd (tgnn, tgMovingRight));
		left = FuzzyLib.fuzzyAnd (left, FuzzyLib.fuzzyNot (shield));

		// tgnnn 
		//OR
		// (tgnn & (mvleft | sp)
		//OR
		// shield & isleft
		tgMovingLeft = FuzzyLib.fuzzyOr(sn, snn, snnn);
		tgIsLeft = FuzzyLib.fuzzyOr (tgn, tgnn, tgnnn);
		hardLeft = tgnnn;
		hardLeft = FuzzyLib.fuzzyOr(hardLeft, FuzzyLib.fuzzyAnd(tgnn, FuzzyLib.fuzzyOr(tgMovingLeft, sp)));
		hardLeft = FuzzyLib.fuzzyOr(hardLeft, FuzzyLib.fuzzyAnd(shield, tgIsLeft));

		//defuzzyficazione
		movementOutput = (-0.8f)*hardLeft +(-0.4f)*left + 0.4f*right + 0.8f*hardRight ;

		if ((left + hardLeft  + right + hardRight)>1)
			Debug.Log (left.ToString("F2") +  hardLeft.ToString("F2")  + right .ToString("F2")+ hardRight.ToString("F2"));


		spacebarOutput = FuzzyLib.toBool (FuzzyLib.fuzzyNot(shield));

		//display
		List<string> message = new List<string>();
		message.Add ("==ATK LOWEST==");
		message.Add ("target distance:");
		message.Add (tgnnn.ToString("F2") + "|" + tgnn.ToString ("F2") + "|" + tgn.ToString ("F2") + (" l"));
		message.Add (tgp.ToString("F2") + "|" + tgpp.ToString("F2") + "|" + tgppp.ToString("F2") + (" r"));
		message.Add ("target velocity:");
		message.Add (snnn.ToString ("F2") + "|" + snn.ToString ("F2") + "|" + sn.ToString ("F2")+ (" l"));
		message.Add (sp.ToString ("F2") + "|" + spp.ToString ("F2") + "|" + sppp.ToString ("F2")+ (" r"));
		message.Add ("shield:");
		message.Add (shield.ToString("F2"));
		message.Add ("movement:");
		message.Add (hardLeft.ToString("F2") + "|" + left.ToString("F2") + "|" + right.ToString("F2") + "|" + hardRight.ToString("F2"));
		message.Add ("shooting");
		message.Add (spacebarOutput.ToString());

		setDisplayMessage (message);

	}
		
}
