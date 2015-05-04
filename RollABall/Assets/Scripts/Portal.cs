using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour
{
    public string sceneName;

    void OnTriggerEnter(Collider other)
    {
        StartCoroutine("OnPortal");
    }

    IEnumerator OnPortal()
    {
        float fadeTime = GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        Application.LoadLevel(sceneName);
    }
}
