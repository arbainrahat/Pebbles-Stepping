using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFailed : MonoBehaviour
{
    public bool enableTrigger;
   

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.inst.levelFailedPanel.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && enableTrigger == true)
        {
            GameManager.inst.levelFailedPanel.SetActive(true);
        }
    }

}
