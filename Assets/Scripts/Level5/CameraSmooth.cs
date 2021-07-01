using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmooth : MonoBehaviour
{
    public Transform target;
    public float distance;
    public float hieght;
    public float smoothTime = 0.3f;
    public Vector3 Offsets;
    private Vector3 velocity = Vector3.zero;


    void Update()
    {
        if(target != null)
        {
            Vector3 targetPosition = target.TransformPoint(new Vector3(0 + Offsets.x, hieght + Offsets.y, distance + Offsets.z));
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            transform.LookAt(target);
        }
    }
}
