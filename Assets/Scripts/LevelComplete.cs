using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    public GameObject levelCompletePanel;


    public bool lvl5;
    public Animator poll;
   // public GameObject levelFailedPanel;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            levelCompletePanel.SetActive(true);
            if (lvl5)
            {
                poll.enabled = false;
            }
        }
        //if (collision.gameObject.CompareTag("AIBot"))
        //{
        //    levelFailedPanel.SetActive(true);
        //}

    }
}
