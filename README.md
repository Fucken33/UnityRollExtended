# UnityRollExtended
This is the extended Roll-A-Ball template from Unity's tutorials.

It's using Unity 5.0.1. Includes portals, multiple levels, lots of prefabs and cross platform input.

The Cross Platform Input does not use the Standard Assets, it uses my own script: The "Joystick Area"

Is clean and simple. Just instantiate an empty game object, add the JoystickArea script and customize the parameters. Here is a brief guide of what every parameter means:

+ <b>areaTexture</b>: This is the bigger texture, the background texture. It will not respond to touches
+ <b>areaRadius</b>: Radius in pixels of the areaTexture (it will be scaled to fit this value)
+ <b>touchTexture</b>: This is the smaller texture, the one that is moved and responds to touches
+ <b>touchRadius</b>: Radius in pixels of the touchTexture (it will be scaled to fit this value)
+ <b>deadZoneRadius</b>: Radius of the dead zone in pixels
+ <b>Smoothing</b>: How slowly the joystick values will decrease when there is no input
+ <b>joystickCenter</b>: This is the position of the center of the textures in pixels, being (0,0) the bottom left corner of the screen. 

When you want to use the joystick values to move your character you should look at this properties of the script:

+ <b>joystickAxes</b>:  joystick axis values both in the interval (-1, 1). Dead zone applied. This is the value you use to get a input like "Input.GetAxis()". for example "Input.GetAxis("Horizontal")" is equal to joystickAxxes.x and Input.GetAxis("Vertical") is equal to joystickAxes.y
+ <b>joystickScale</b>: Axis multiplier
+ <b>joystickScaled</b>: This is vector2 like joystickAxes. You can use it to get a something like:

<pre>
joystick.joystickScale = new Vector2(speed, speed);

// Here "joystickScaled = joystickAxes * joystickScale"
float horizontal = joystick.joystickScaled.x;
float vertical = joystick.joystickScaled.y;

Vector3 movement = new Vector3(horizontal, 0f, vertical);
player.GetComponent<RigidBody>().addForce(movement);
</pre>
