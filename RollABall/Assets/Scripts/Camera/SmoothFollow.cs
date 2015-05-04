using UnityEngine;
using System.Collections;

// Place the script in the Camera-Control group in the component menu
[AddComponentMenu("Camera-Control/Smooth Follow CSharp")]
public class SmoothFollow : MonoBehaviour {

    /*
    This camera smoothes out rotation around the y-axis and height.
    Horizontal Distance to the target is always fixed.
 
    There are many different ways to smooth the rotation but doing it this way gives you a lot of control over how the camera behaves.
 
    For every of those smoothed values we calculate the wanted value and the current value.
    Then we smooth it using the Lerp function.
    Then we apply the smoothed values to the transform's position.
    */

    // the target we are following
    public Transform target; 
    // Distance to the target in the x-z plane
    public float distance = 10.0f;
    // Height we want the camera to be above the target
    public float height = 5.0f;
    // How much we smooth rotation and height
    public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;
    public float RotateSpeed = 3.0f;
    public float wantedRotationAngle = 0.0f;

    void LateUpdate()
    {
        // Return if we don't have a target
        if (!target) return;

        // Calculate rotations angles
        //float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + height;
        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        // Lerp rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, 
                                               wantedRotationAngle, 
                                               rotationDamping * Time.deltaTime);
        // Lerp the height
        currentHeight = Mathf.Lerp(currentHeight, 
                                   wantedHeight, 
                                   heightDamping * Time.deltaTime);
        // Convert angle into quaternion
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set camera position on the x-z plane (meters behind the target)
        transform.position  = target.position;
        transform.position -= currentRotation * Vector3.forward * distance;

        // Set height of the camera
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

        // Look at target
        transform.LookAt(target);
    }
}
