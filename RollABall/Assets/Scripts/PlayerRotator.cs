using UnityEngine;
using System.Collections;

public class PlayerRotator : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Camera.main.GetComponent<SmoothFollow>().wantedRotationAngle -= 90;
        Camera.main.GetComponent<SmoothFollow>().height = 10;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
     }

}
