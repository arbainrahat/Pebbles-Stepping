using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe_L2 : MonoBehaviour
{
    public float minSwipeDistance;
    public float maxSwipeTime;

    private float swipeStartTime;
    private float swipeEndTime;
    private float swipeTime;

    private Vector2 startSwipePosition;
    private Vector2 endSwipePosition;
    private float swipeLength;


    public Vector2 Distance;
    public float xDistance;
    public float yDistance;

    bool firstSwipeUp = false;

    private void Update()
    {
        SwipeTest();
    }
    void SwipeTest()
    {
        if (Input.touchCount > 0)
        {

            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                swipeStartTime = Time.time;
                startSwipePosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                swipeEndTime = Time.time;
                endSwipePosition = touch.position;
                swipeTime = swipeEndTime - swipeStartTime;
                swipeLength = (endSwipePosition - startSwipePosition).magnitude;

                if (swipeTime < maxSwipeTime && swipeLength > minSwipeDistance)
                {
                    SwipeControl();
                    if(GameManager.inst.player.GetComponent<PlayerScript_L2>().jumpContrl == false)
                    {
                        StartCoroutine(JumpAnimRight());
                        StartCoroutine(JumpAnimLeft());
                    }
                   
                    //if(firstSwipeUp == true)
                    //{
                    //  //  StartCoroutine(JumpAnimUp());
                    //}
                    
                }
            }
        }
    }

    void SwipeControl()
    {
        Distance = endSwipePosition - startSwipePosition;
        xDistance = Mathf.Abs(Distance.x);
        yDistance = Mathf.Abs(Distance.y);
    }



    void SwipeUp()
    {

        GameManager.inst.player.GetComponent<PlayerScript_L2>().ForwardMove();
        GameManager.inst.jumpProjectile.LunchNow();
    }

    void SwipeRight()
    {

       // Debug.Log("SwipeRight");
        if (GameManager.inst.player.GetComponent<PlayerScript_L2>().rightSideCheck == false)
        {
            
            GameManager.inst.player.GetComponent<PlayerScript_L2>().RightMove();
            GameManager.inst.jumpProjectile.LunchNow();
        }

    }

    void SwipeLeft()
    {
       // Debug.Log("SwipeLeft");
        if (GameManager.inst.player.GetComponent<PlayerScript_L2>().leftSideCheck == false)
        {
            
            GameManager.inst.player.GetComponent<PlayerScript_L2>().LeftMove();
            
            GameManager.inst.jumpProjectile.LunchNow();

            
        }

    }

    IEnumerator JumpAnimRight()
    {
        if (xDistance > yDistance)
        {
            if (Distance.x > 0)
            {
                GameManager.inst.player.GetComponent<PlayerScript_L2>().StopCoroutne();
                if (GameManager.inst.player.GetComponent<PlayerScript_L2>().rightSideCheck == false && GameManager.inst.isGrounded)
                {
                    firstSwipeUp = true;
                  //  GameManager.inst.player.GetComponent<PlayerScript_L2>().disableCollision = false;


                    GameManager.inst.isGrounded = false;
                    GameManager.inst.player.GetComponent<PlayerScript_L2>().JumpAnim();
                    GameManager.inst.player.transform.parent = null;

                    //yield return new WaitForSeconds(0.25f);
                    yield return new WaitForSeconds(0.45f);
                    // Debug.Log("Animation_Check_Right");
                    SwipeRight();

                }
            }
        }

    }

    IEnumerator JumpAnimLeft()
    {
        if (xDistance > yDistance)
        {
            if (Distance.x < 0)
            {
                GameManager.inst.player.GetComponent<PlayerScript_L2>().StopCoroutne();
                if (GameManager.inst.player.GetComponent<PlayerScript_L2>().leftSideCheck == false && GameManager.inst.isGrounded)
                {
                    firstSwipeUp = true;
                  //  GameManager.inst.player.GetComponent<PlayerScript_L2>().disableCollision = false;

                    GameManager.inst.isGrounded = false;
                    GameManager.inst.player.GetComponent<PlayerScript_L2>().JumpAnim();
                    GameManager.inst.player.transform.parent = null;

                    yield return new WaitForSeconds(0.45f);

                  //  Debug.Log("Animation_Check_Left");
                    SwipeLeft();

                }
            }
        }

    }

    IEnumerator JumpAnimUp()
    {
        if (yDistance > xDistance && GameManager.inst.isGrounded)
        {
            if (Distance.y > 0)
            {
                
                GameManager.inst.isGrounded = false;

                GameManager.inst.player.GetComponent<PlayerScript_L2>().JumpAnim();
                GameManager.inst.player.transform.parent = null;

                yield return new WaitForSeconds(0.3f);

               // Debug.Log("Animation_Check_Up");
                SwipeUp();
            }
        }

    }

}
