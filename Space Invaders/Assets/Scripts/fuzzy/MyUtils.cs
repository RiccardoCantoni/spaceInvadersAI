using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MyUtils{

	public static float closest(List<float> list, float playerPos){
		float minDistance = Mathf.Abs (list [0] - playerPos);
		int closestIndex = 0;
		for (int i = 1; i < list.Count; i++) {
			float d = Mathf.Abs (list [i] - playerPos);
			if (d < minDistance) {
				minDistance = d;
				closestIndex = i;
			}
		}
		return list[closestIndex];
	}

	public static float calculateLead(float tgSpeed, float tgHeight, float projectileSpeed){
		float res = Mathf.Abs(tgSpeed * tgHeight / projectileSpeed);
		if (tgSpeed > 0) {
			return res;
		} else {
			return res *(-1);
		}
	}

	public static Vector3 gridToVector3(int x, int y, GameObject offsetMarker) {
		return new Vector3 (x * 1.36f + offsetMarker.transform.position.x - 7.8f, y * -1 + offsetMarker.transform.position.y + 4, -10);
	}

	public static int argmax(float[] values){
		int minIndex = -1;
		float minValue = 0;
		for (int i = 0; i < values.Length; i++) {
			if (values [i] > minValue) {
				minIndex = i;
			}
		}
		return minIndex;
	}

	public static void appendToFile(string filename, string text){
		System.IO.File.AppendAllText (filename, text+Environment.NewLine);
	}

}
