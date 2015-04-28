using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float speed = 5f;
    public Text countText;
    public Text winText;
    public JoystickArea joystick;

    private int pickUpCount;
    private int totalPickUpCount;
    private Rigidbody body;
    private float horizontalAxe = 0f;
    private float verticalAxe = 0f;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        pickUpCount = 0;
        setCountText();
        winText.text = "";
        GameObject[] totalPickUps = GameObject.FindGameObjectsWithTag("Pick Up");
        totalPickUpCount = totalPickUps.Length;
    }

    void FixedUpdate()
    {
        updateAxes();
        Vector3 movement = new Vector3(horizontalAxe, 0, verticalAxe);
        body.AddForce(movement * speed);
        
        if (transform.position.y < -50)
            Application.LoadLevel(Application.loadedLevel);
    }

    void updateAxes()
    {
        Vector2 axes;
        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.WindowsEditor)
        {
            axes = joystick.joystickAxes;
        }
        else
        {
            axes = new Vector2(0, 0);
        }
        horizontalAxe = Input.GetAxis("Horizontal") + axes.x;
        verticalAxe = Input.GetAxis("Vertical") + axes.y;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            Destroy(other.gameObject);
            pickUpCount++;
            setCountText();
        }
        if (other.gameObject.CompareTag("Trigger") && pickUpCount == totalPickUpCount)
        {
            body.velocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
            body.AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);
        }
    }

    void setCountText()
    {
        countText.text = "Pick Ups: " + pickUpCount;
        if (pickUpCount == totalPickUpCount)
        {
            winText.text = "Level Clear";
        }
    }
}
