using UnityEngine;
using System.Collections;

public class DisbaleCollider : MonoBehaviour
{

    public GameObject target;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine("disableCollider");
        }
    }

    IEnumerator disableCollider()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        int pickUpsGathered = player.GetComponent<PlayerController>().getPickUpCount();
        int pickUpNumber = player.GetComponent<PlayerController>().getPickUpNum();
        if (target.GetComponent<MeshCollider>() && pickUpNumber == pickUpsGathered)
        {
            yield return new WaitForSeconds(2f);
            target.GetComponent<MeshCollider>().enabled = false;
        }
    }
}
