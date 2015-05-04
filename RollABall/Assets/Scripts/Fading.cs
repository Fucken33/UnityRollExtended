using UnityEngine;
using System.Collections;

public class Fading : MonoBehaviour
{

    public Texture2D fadeOutTexture; // texture that will overlay the screen. Can be a black image or a loading graphic
    public float fadeSpeed = 0.8f;   // fading speed

    private int drawDepth = -1000;   // texture's order in the draw hierarchy: a low number means it renders on top
    private float alpha = 1.0f;
    private int fadeDir = -1;        // scene fade in = -1, scene fade out = 1

    void OnGUI()
    {
        // Fade in/out the alpha value using direction, speed and deltaTime
        alpha += fadeDir * fadeSpeed * Time.deltaTime;

        // Force (clamp) the value to be between 0 and 1
        alpha = Mathf.Clamp01(alpha);

        // Set the aplha of the GUI color
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;
        Rect textureRect = new Rect(0, 0, Screen.width, Screen.height);
        GUI.DrawTexture(textureRect, fadeOutTexture);
    }
    // Sets fadedir to the direction parameter, then fades
    public float BeginFade(int direction)
    {
        fadeDir = direction;
        return fadeSpeed; // return the fade speed so it is easy to time the App.LoadLevel()
    }

    // OnLevelWasLoaded is called when a level is loaded.
    // It is passed a level index so you can limit the fade in to certain scenes
    void OnLevelWasLoaded()
    {
        // alpha = 1;   // set alpha to be 1 if it is not set by default
        BeginFade(-1);  // call the fade in function
    }
}
