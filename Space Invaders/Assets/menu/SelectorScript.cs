using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorScript : MonoBehaviour {

	public GameObject selectorPrefab;
	public List<GameObject> buttons;
	public GameObject placeholder;

	GameObject selector;

	public void onHover(int parameter){
		if (selector == null) {
			selector = Instantiate (selectorPrefab);
			selector.transform.parent = transform;
		}
		switch (parameter) {
		case 0:
			selector.transform.position = new Vector3 (placeholder.transform.position.x, buttons[0].transform.position.y, 100);
			break;
		case 1:
			selector.transform.position = new Vector3 (placeholder.transform.position.x, buttons[1].transform.position.y, 100);
			break;
		case 2:
			selector.transform.position = new Vector3 (placeholder.transform.position.x, buttons[2].transform.position.y, 100);
			break;
		default:
			break;
		}
	}
}
