using UnityEngine;
using System.Collections;

public class Rearrange : MonoBehaviour {

    public Transform target;

	// Update is called once per frame
	void Update () {
        if (!target) return;
        if (Input.GetKeyDown(KeyCode.R))
        {
            target.GetChild(target.childCount - 1).SetSiblingIndex(0);
        }
	}
}
