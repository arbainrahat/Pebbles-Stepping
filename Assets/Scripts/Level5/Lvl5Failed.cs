using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl5Failed : MonoBehaviour
{
    
   

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Lvl5Fail());
        }
    }

   

    IEnumerator Lvl5Fail()
    {
        GameManager.inst._camera.GetComponent<CameraSmooth>().enabled = false;
        GameManager.inst.player.GetComponent<CapsuleCollider>().enabled = false;
        GameManager.inst.player.GetComponent<Animator>().enabled = false;
        GameManager.inst.player.GetComponent<PlayerScript>().EnableColliders();
        GameManager.inst.player.GetComponent<PlayerScript>().DisableKinematic();
        GameManager.inst.player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        yield return new WaitForSeconds(3f);
        GameManager.inst.levelFailedPanel.SetActive(true);
    }
}
