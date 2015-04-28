using UnityEngine;
using System.Collections;

public class JoystickArea : MonoBehaviour
{
    public Texture areaTexture;  // Background texture
    public Texture touchTexture; // Moving texture
    public Vector2 joystickCenter = new Vector2(120f, 120f); // Position of the center of the joystick textures (in pixels)
    public Vector2 joystickScale  = new Vector2(10, 10);     // joystick axes multiplier
    public float areaRadius       = 100f;  // radius of areaTexture (in pixels)
    public float touchRadius      = 50f;   // radius of touchTexture (in pixels)
    public float deadZoneRadius   = 20f;   // radius of the dead zone, where input is not taken (in pixels)
    
    public Vector2 joystickAxes{ get; private set; }    // joystick axis value in the interval (-1, 1) (dead zone applied)
    public Vector2 joystickScaled { get; private set; } // scale * axes

    private Vector2 joystickRaw;                        // raw joystick input in the interval (-areaRadius, areaRadius)

    [SerializeField]
    private Vector2 _smoothing = new Vector2(20f, 20f); // Smoothing of the raw joystick input (slowly decrease to 0 when there is no input)

    public Vector2 Smoothing
    {
        get { return this._smoothing; }
        set
        {
            _smoothing = value;
            if (_smoothing.x < 0.1f)
            {
                _smoothing.x = 0.1f;
            }
            if (_smoothing.y < 0.1)
            {
                _smoothing.y = 0.1f;
            }
        }
    }
    private int  joystickIndex = -1;
    private bool enableReset;
    
    void Start()
    {
        joystickCenter.x = Screen.width - joystickCenter.x;
        enableReset = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
        {
            checkTouches();
        }
        else
        {
            checkMouse();
        }
    }

    private void checkTouches()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                if (joystickIndex == touch.fingerId)
                {
                    joystickIndex = -1;
                    enableReset = true;
                }
            }

            if (joystickIndex == touch.fingerId)
            {
                OnTouchDown(touch.position);
            }
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 distToJoystickCenter = touch.position - joystickCenter;
                if (distToJoystickCenter.sqrMagnitude < Mathf.Pow(areaRadius, 2))
                {
                    joystickIndex = touch.fingerId;
                }
            }
        }
        UpdateJoystick();

        if (enableReset)
        {
            ResetJoystick();
        }
    }

    private void checkMouse()
    {
        if (Input.GetMouseButtonUp(0)) // on mouse left click released
        {
            joystickIndex = -1;
            enableReset = true;
        }
        if (joystickIndex == 1)
        {
            OnTouchDown(Input.mousePosition);
        }
        if (Input.GetMouseButtonDown(0)) // on mouse left click pressed
        {
            Vector2 distToJoystickCenter = (Vector2)Input.mousePosition - joystickCenter;
            if (distToJoystickCenter.sqrMagnitude < Mathf.Pow(areaRadius, 2))
            {
                joystickIndex = 1;
            }
        }
        if (enableReset)
        {
            ResetJoystick();
        }
        UpdateJoystick();
    }

    private void UpdateJoystick()
    {
        if (joystickRaw.sqrMagnitude > deadZoneRadius * deadZoneRadius)
        {
            float newAxisX = 0f;
            float newAxisY = 0f;

            if (Mathf.Abs(joystickRaw.x) > deadZoneRadius)
            {
                newAxisX = (joystickRaw.x - (deadZoneRadius * Mathf.Sign(joystickRaw.x))) / (areaRadius - deadZoneRadius);
            }
            else
            {
                newAxisX = joystickRaw.x / areaRadius;
            }

            if (Mathf.Abs(joystickRaw.y) > deadZoneRadius)
            {
                newAxisY = (joystickRaw.y - (deadZoneRadius * Mathf.Sign(joystickRaw.y))) / (areaRadius - deadZoneRadius);
            }
            else
            {
                newAxisY = joystickRaw.y / areaRadius;
            }
            joystickAxes = new Vector2(newAxisX, newAxisY);
        }
        else
        {
            joystickAxes = new Vector2(0, 0);
        }
        joystickScaled = new Vector2(joystickScale.x * joystickAxes.x, joystickScale.y * joystickAxes.y);
    }

    /// <summary>
    /// Apply raw position value from touch or mouse.
    /// This position must be between the circle formed by the area radius
    /// </summary>
    /// <param name="position"></param>
    void OnTouchDown(Vector2 position)
    {
        joystickRaw = new Vector2(position.x, position.y) - joystickCenter;
        if ((joystickRaw / areaRadius).sqrMagnitude > 1)
        {
            joystickRaw.Normalize();
            joystickRaw *= areaRadius;
        }
    }

    /// <summary>
    /// Apply smoothing to the joystick raw value taking it to (0, 0)
    /// </summary>
    private void ResetJoystick()
    {
        if (joystickRaw.sqrMagnitude > 0.1)
        {
            // apply smoothing if joystick raw value is above a certain value
            joystickRaw = new Vector2(joystickRaw.x - joystickRaw.x * _smoothing.x * Time.deltaTime, joystickRaw.y - joystickRaw.y * _smoothing.y * Time.deltaTime);
        }
        else
        {
            joystickRaw = Vector2.zero;
            enableReset = false;
        }
    }

    void OnGUI()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.WindowsEditor)
        {
            float areaLeft = joystickCenter.x - areaRadius;
            float areaTop = Screen.height - joystickCenter.y - areaRadius;
            Rect areaRect = new Rect(areaLeft, areaTop, areaRadius * 2, areaRadius * 2);

            float touchLeft = joystickCenter.x + (joystickRaw.x - touchRadius);
            float touchTop = Screen.height - joystickCenter.y - (joystickRaw.y + touchRadius);
            Rect touchRect = new Rect(touchLeft, touchTop, touchRadius * 2, touchRadius * 2);
            
            GUI.DrawTexture(areaRect, areaTexture, ScaleMode.ScaleToFit, true);
            GUI.DrawTexture(touchRect, touchTexture, ScaleMode.ScaleToFit, true);
        }
    }
}