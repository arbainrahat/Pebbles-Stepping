using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class JumpProjectile : MonoBehaviour
{
    [Header("Scripts")]

    public float gravity = -18;
   
    public Rigidbody ballBody;
    [SerializeField]
    private Transform target;
    [SerializeField]
    public float Height_h = 0;
    public bool drawProjectile = false;
    public Vector3 Velocity;
    public float JumpValue;
    
    
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    LunchNow();
        //}
        //if (drawProjectile)
        //{
        //    DrawNow();
        //}

    }
    public void LunchNow()
    {
        //_bowlingScript.ballTrail.enabled = true;
        SetHeight();
        Physics.gravity = Vector3.up * gravity;
        ballBody.useGravity = true;
        ballBody.isKinematic = false;
        ballBody.velocity = CalculateLunchData().initialVelocity;

        //wrongInput = ;
        //print(CalculateLunchData());
    }
    void DrawNow()
    {
        LunchData lunchData = CalculateLunchData();
        int resolution = 30;
        Vector3 previousPoint = ballBody.position;
        for (int i = 0; i <= resolution; i++)
        {
            float simulationTime = i / (float)resolution * lunchData.time;
            Vector3 displacement = lunchData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2;
            Vector3 drawPoint = ballBody.position + displacement;
            Debug.DrawLine(previousPoint, drawPoint, Color.red);
            previousPoint = drawPoint;
        }
        //Debug.DrawLine();
    }
    LunchData CalculateLunchData()
    {
        
        float VerticalDisp_py = target.position.y - ballBody.position.y;
        Vector3 HorizontalDisp_px = new Vector3(target.position.x - ballBody.position.x, 0, target.position.z - ballBody.position.z);
        float time = (Mathf.Sqrt(-2 * Height_h / gravity) + Mathf.Sqrt(2 * (VerticalDisp_py - Height_h) / gravity));
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * Height_h);
        Vector3 velocityXZ = HorizontalDisp_px / time;
        return new LunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }
    struct LunchData
    {
        public readonly Vector3 initialVelocity;
        public readonly float time;
        public LunchData(Vector3 initialVelocity, float time)
        {
            this.initialVelocity = initialVelocity;
            this.time = time;
        }
    }
    public void SetProjectileTarget(Transform newTarget)
    {
        target = newTarget;
        
    }
    void SetHeight()
    {
        Height_h = JumpValue;   
    }
}
