using UnityEngine;
using System.Collections;

public class Dash : MonoBehaviour
{

    public Vector3 dashForce;

    void Start()
    {
        if (dashForce == Vector3.zero)
        {
            dashForce = new Vector3(0f,0f,-20f);
        }
    }

    void OnCollisionEnter(Collision collisionData)
    {
        Debug.Log("red cube colliding with " + collisionData.gameObject.name);
        collisionData.rigidbody.AddForce(dashForce, ForceMode.VelocityChange);
    }

}