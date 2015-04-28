using UnityEngine;
using System.Collections;

public class ChangeColor : MonoBehaviour {

	public float changeHeight = 2f;

	private Renderer render;
	private bool changed;

	void Start(){
		render = GetComponent<Renderer> ();
	}

	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Ray landingRay = new Ray (transform.position, Vector3.down);

		if (!changed) {
			if (Physics.Raycast (landingRay, out hit, changeHeight)) {
				//GetComponent<Material> ().SetColor ("Albedo", Color.red);
				render.material.color = new Color(Random.value,Random.value,Random.value);

				changed = true;
			}
		}
	}
}
