using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourSelector : FuzzyBehaviour {

	int avoid = 0;
	int shelter = 1;
	int atkRedship = 2;
	int atkClosest = 3;
	int atkLowest = 4;

	public int behaviourIndex;

	float[] behaviours = new float[5];

	#region danger

	FuzzyClass dangerLow = new FuzzyClass(-0.1f, 0, 0.2f, 0.5f);
	FuzzyClass dangerMedium = new FuzzyClass (0.2f, 0.5f, 0.8f);
	FuzzyClass dangerHigh = new FuzzyClass (0.5f, 0.8f, 5, 10);

	#endregion

	#region shield

	FuzzyClass shieldClose = new FuzzyClass (-1, 0, 0.9f, 1.35f);
	FuzzyClass shieldFar = new FuzzyClass (0.9f, 1.35f, 100, 101);

	#endregion

	#region redship

	CrispClass redShipPresent = new CrispClass (0.75f, 1.25f);

	FuzzyClass screenWeak = new FuzzyClass (-1, 0, 4, 9);
	FuzzyClass screenMedium = new FuzzyClass (4, 9, 16, 21);
	FuzzyClass screenStrong = new FuzzyClass (16, 21, 35, 36);

	#endregion

	#region grid

	FuzzyClass distributionHorizontal = new FuzzyClass(0,1,1.4f,1.8f);
	FuzzyClass distributionUndefined = new FuzzyClass(1.4f, 1.8f, 2.3f, 2.7f);
	FuzzyClass distributionVertical = new FuzzyClass(2.3f, 2.7f, 10f, 11f);

	FuzzyClass lowestAlienLow = new FuzzyClass(-1, 0, 2.5f, 4.5f);
	FuzzyClass lowestAlienHigh = new FuzzyClass(2.5f, 4.5f, 10, 11);


	#endregion

	public override void updateOutputs(){
		Debug.Log ("ERROR: updateOutputs called on selector");
	}

	public void selectBehaviour(){

		//fuzzyficazione
		float dngl, dngm, dngh;
		dngl = dangerLow.calculateMembership (sensor.danger);
		dngm = dangerMedium.calculateMembership (sensor.danger);
		dngh = dangerHigh.calculateMembership (sensor.danger);

		float shc, shf;
		shc = shieldClose.calculateMembership (Mathf.Abs(sensor.shieldDistance));
		shf = shieldFar.calculateMembership (Mathf.Abs(sensor.shieldDistance));

		float rs;
		rs = redShipPresent.calculateMembership (sensor.redShipPresent);

		float sw, sm, ss; 
		sw = screenWeak.calculateMembership (sensor.redShipScreen);
		sm = screenMedium.calculateMembership (sensor.redShipScreen);
		ss = screenStrong.calculateMembership (sensor.redShipScreen);

		float dh, du, dv;
		dh = distributionHorizontal.calculateMembership (sensor.alienDistributionIndex);
		du = distributionUndefined.calculateMembership (sensor.alienDistributionIndex);
		dv = distributionVertical.calculateMembership (sensor.alienDistributionIndex);

		float lwh, lwl;
		lwh = lowestAlienHigh.calculateMembership (sensor.lowestAlienHeight);
		lwl = lowestAlienLow.calculateMembership (sensor.lowestAlienHeight);

		//FAM
		float defend = FuzzyLib.fuzzyOr (dngm, dngh); 
		float attack = dngl; 

		//avoid = defend & !shc
		behaviours[avoid] = FuzzyLib.fuzzyAnd(defend, shf);

		//shelter = defend & shc
		behaviours [shelter] = FuzzyLib.fuzzyAnd (defend, shc);

		//atkred = attack & pres & (sw | dv | (sm & du & lwh))
		behaviours[atkRedship] = FuzzyLib.fuzzyAnd(
			attack, 
			rs, 
			FuzzyLib.fuzzyOr(sw, dv, FuzzyLib.fuzzyAnd(sm, du, lwh)));
		
		//atkClosest = attack & lwh & (dh | (!rs & (du | dv))) 
		behaviours[atkClosest] = FuzzyLib.fuzzyAnd(
			attack, 
			lwh, 
			FuzzyLib.fuzzyOr(
				dh,
				FuzzyLib.fuzzyAnd(
					FuzzyLib.fuzzyNot(rs), 
					FuzzyLib.fuzzyOr(du, dv))));
		
		//atkLowest = attack & lwl
		behaviours[atkLowest] = FuzzyLib.fuzzyAnd(attack, lwl);

		//defuzzyficazione
		behaviourIndex = MyUtils.argmax (behaviours);

		//display
		List<string> message = new List<string>();
		message.Add ("==SELECTOR==");
		message.Add ("danger level:");
		message.Add (dngl.ToString("F2") + "|" + dngm.ToString ("F2") + "|" + dngh.ToString ("F2"));
		message.Add ("shield:");
		message.Add (shc.ToString("F2") + "|" + shf.ToString ("F2"));
		message.Add ("red ship present:");
		message.Add (rs.ToString("F2"));
		if (sensor.redShipPresent > 0) {
			message.Add ("red ship screen:");
			message.Add (sw.ToString ("F2") + "|" + sm.ToString ("F2") + "|" + ss.ToString ("F2"));
		}
		message.Add ("alien distribution:");
		message.Add (dh.ToString("F2") + "|" + du.ToString ("F2") + "|" + dv.ToString ("F2"));
		message.Add ("lowest alien height:");
		message.Add (lwh.ToString("F2") + "|" + lwl.ToString ("F2"));
		message.Add ("behavior index:");
		message.Add (behaviourIndex.ToString());

		setDisplayMessage (message);
	}
}
