using UnityEngine;
using System.Collections;

public class SonicCollision : MonoBehaviour
{
    private Rigidbody body;
    private Vector3 rotatedNormal;

    void Start()
    {
        rotatedNormal = Vector3.zero;
        body = GetComponent<Rigidbody>();
    }

    void zeroVelocity()
    {
        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
    }

    void OnCollisionStay(Collision collision)
    {
        //zeroVelocity();
        foreach(ContactPoint contact in collision.contacts)
        {
            Vector3 normal = contact.normal;
            float rampAngle = Vector3.Angle(Vector3.up, normal);
            print("ramp angle: " + rampAngle);
            Debug.DrawLine(contact.point, contact.point + (normal * 5), Color.red, 1, false);

            rotatedNormal = rotate90(normal);
            Debug.DrawLine(contact.point, contact.point + (rotatedNormal * 5), Color.red, 1, false);
        }
    }

    Vector3 rotate90(Vector3 vec)
    {
        return Vector3.Cross(-1*transform.forward, vec);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            body.AddForce(100*rotatedNormal);
        }
    }
}