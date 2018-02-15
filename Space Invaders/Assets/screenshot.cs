using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenshot : MonoBehaviour {

	void Update(){
		if (Input.GetMouseButtonDown(0)){
			ScreenCapture.CaptureScreenshot("Screenshot.png");
			print ("click");
		}
	}
}
