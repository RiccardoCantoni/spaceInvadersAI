using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour {

	GameManager gm;

	public void buttonClicked(int parameter){
		gm = GameObject.Find ("GameMaster").GetComponent<GameManager> ();
		switch (parameter) {
		case 0:
			Application.Quit ();
			break;
		case 1:
			gm.startGameHuman ();
			break;
		case 2:
			gm.startGameAuto ();
			break;
		default:
			MyUtils.appendToFile ("log.txt", "=====");
			Application.Quit ();
			break;
		}

	}

}
