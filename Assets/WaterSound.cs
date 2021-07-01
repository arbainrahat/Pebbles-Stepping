using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSound : MonoBehaviour
{
    public AudioSource source;
    public AudioClip JumpWater;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            source.clip = JumpWater;
            source.Play();
            Vibration.Vibrate(2000);
            Debug.Log("Water fallllllllllpdddplcdlcl");
        }
        
    }
}
