using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonTest : MonoBehaviour {

	Image testimg;

	void Start(){
		testimg = GetComponent<Image> ();
	}

	public void Green(){
		testimg.color = Color.green;
	}

	public void Azul(){
		testimg.color = new Color (255, 255, 0);
	}

	public void Rojo(){
		testimg.color = new Color (0,255,255);
	}
}
