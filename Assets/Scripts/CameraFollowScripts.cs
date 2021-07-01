using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScripts : MonoBehaviour
{
    public Transform Target;
   // public Transform PlayerTransformForCamera;
    public float distance;
    public float height;
    public float Side;
    public Vector3 offsets;
    public float FollowSpeed;
    public bool canFollowPlayer;
    public bool lookAt;

    void Start()
    {
        // PlayerTransformForCamera.position = Target.position;

    }


    public void StartCamera()
    {
        StartCoroutine(GetPlayer());
    }

    IEnumerator GetPlayer()
    {
        yield return new WaitForSeconds(0f);
        Target = GameManager.inst.player.transform;
        transform.localPosition = new Vector3(Target.position.x, transform.position.y, transform.position.z);
        canFollowPlayer = true;
    }

    void FixedUpdate()
    {
        // PlayerTransformForCamera.position = new Vector3(0f, Target.position.y, Target.position.z);
        //Vector3 targetPos = new Vector3(Target.position.x, Target.position.y, Target.position.z);
        if(canFollowPlayer)
        {
            Vector3 targetPos = new Vector3(transform.position.x, Target.position.y, Target.position.z);
            
            targetPos -= offsets;

        
            transform.position = Vector3.Lerp(transform.position,
             new Vector3(targetPos.x + Side, targetPos.y + height, targetPos.z + distance),
             FollowSpeed * Time.deltaTime);
            if(lookAt == true)
            {
                // transform.LookAt(Target);

                var targetRotation = Quaternion.LookRotation(Target.position - transform.position);

                // Smoothly rotate towards the target point.
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
            }
            
        }
         
    }
}
