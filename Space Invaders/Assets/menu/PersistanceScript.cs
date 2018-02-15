using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistanceScript : MonoBehaviour {
	 
	void Awake(){
		DontDestroyOnLoad (gameObject);
	}
}
