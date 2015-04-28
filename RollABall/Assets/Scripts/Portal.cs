using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour
{
    public string sceneName;

    void OnTriggerEnter(Collider other)
    {
        Application.LoadLevel(sceneName);
    }

}
