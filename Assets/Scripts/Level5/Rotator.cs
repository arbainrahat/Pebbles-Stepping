using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float Speed = 1f;
    public bool antiClockWise;
    
    private void Update()
    {
        // transform.Rotate(Vector3.up * Time.deltaTime * Speed, Space.World);

        if (antiClockWise)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * Speed * -1, Space.World);
        }
        else
        {
            transform.Rotate(Vector3.up * Time.deltaTime * Speed, Space.World);
        }

    }

}
