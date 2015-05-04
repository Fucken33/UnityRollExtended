using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TouchTest : MonoBehaviour {

	public Text touchText;
	
	// Update is called once per frame
	void Update () {
		int fingerCount = 0;
		foreach (Touch touch in Input.touches) {
			if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
				fingerCount++;

		}
		if (fingerCount > 0)
			touchText.text = "User has " + fingerCount + " finger(s) touching the screen";
		else
			touchText.text = "";
	}
}
