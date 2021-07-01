using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurdleMovementControl : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<Animator>().enabled = true;
        }
    }
}
