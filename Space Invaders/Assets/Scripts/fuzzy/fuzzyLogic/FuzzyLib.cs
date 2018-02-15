using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FuzzyLib{

	public static float fuzzyAnd(float a, float b){
		return Mathf.Min (a, b);
	}

	public static float fuzzyAnd (float a, float b, float c){
		return fuzzyAnd (a, fuzzyAnd (b, c));
	}

	public static float fuzzyOr(float a, float b){
		return Mathf.Max (a, b);
	}

	public static float fuzzyOr(float a, float b, float c){
		return fuzzyOr (a, fuzzyOr (b, c));
	}

	public static float fuzzyNot(float a){
		return 1f - a;
	}

	public static float fuzzyOrMultiple(List<float> list){
		return Mathf.Max (list.ToArray ());
	}

	public static bool toBool (float f){
		if (f == 0) {
			return false;
		}
		return true;
	}

}
