using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
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

    [HideInInspector]
    public bool sidesControl = false;


    //For Level 3

    public bool rightCheck;
    public bool middleCheck;
    public bool leftCheck;
   

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
                    if(sidesControl == false)
                    {
                        StartCoroutine(JumpAnimRight());
                        StartCoroutine(JumpAnimLeft());
                    }
                   
                    StartCoroutine(JumpAnimUp());
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
        
        //if (yDistance > xDistance)
        //{
            Debug.Log("SwipeUp");
            //if (Distance.y > 0)
            //{
            // GameManager.inst.player.GetComponent<PlayerScript>().JumpAnim();

            GameManager.inst.player.GetComponent<PlayerScript>().ForwardMove();

            if (GameManager.inst.canJump == false)
            {
            print("canJump : " + GameManager.inst.canJump);
                GameManager.inst.jumpProjectile.LunchNow();

            }



            //}
       // }
    }

    void SwipeRight()
    {
        
        //if (xDistance > yDistance)
        //{
        //    if (Distance.x > 0)
        //    {
                Debug.Log("SwipeRight");
                if (GameManager.inst.player.GetComponent<PlayerScript>().rightSideCheck == false)
                {
                    // GameManager.inst.player.GetComponent<PlayerScript>().JumpAnim();
                    GameManager.inst.player.GetComponent<PlayerScript>().RightMove();
                    
                    //if (GameManager.inst.player.GetComponent<PlayerScript>().groundCheck == true)
                    //{

                    if (GameManager.inst.canJump == false)
                    {
                        GameManager.inst.jumpProjectile.LunchNow();

                    }

                    //}
                }

        //    }
        //}
    }

    void SwipeLeft()
    {
        
      // if (xDistance > yDistance)
      //{
      //  if (Distance.x < 0)
      //  {
                Debug.Log("SwipeLeft");
                if (GameManager.inst.player.GetComponent<PlayerScript>().leftSideCheck == false)
            {
                // GameManager.inst.player.GetComponent<PlayerScript>().JumpAnim();
                GameManager.inst.player.GetComponent<PlayerScript>().LeftMove();
                     
                    //if (GameManager.inst.player.GetComponent<PlayerScript>().groundCheck == true)
                    //{

                    if (GameManager.inst.canJump == false)
                    {
                        GameManager.inst.jumpProjectile.LunchNow();

                    }

                    //}
                }

      //  }
      //}
        
    }

    IEnumerator JumpAnimRight()
    {
        if (xDistance > yDistance )
        {
            if (Distance.x > 0)
            {
                if (GameManager.inst.player.GetComponent<PlayerScript>().rightSideCheck == false && GameManager.inst.isGrounded)
                {
                    GameManager.inst.isGrounded = false;
                    GameManager.inst.player.GetComponent<PlayerScript>().JumpAnim();        
                    
                    yield return new WaitForSeconds(0.3f);
                    
                    Debug.Log("Animation_Check_Right");
                    SwipeRight();

                }
            }
        }
                

        //yield return new WaitForSeconds(0.3f);
        //SwipeRight();
      //  SwipeControl();
    }

    IEnumerator JumpAnimLeft()
    {
        if (xDistance > yDistance)
        {
            if (Distance.x < 0)
            {
                if (GameManager.inst.player.GetComponent<PlayerScript>().leftSideCheck == false && GameManager.inst.isGrounded)
                {
                    GameManager.inst.isGrounded = false;
                    GameManager.inst.player.GetComponent<PlayerScript>().JumpAnim();
                    
                    yield return new WaitForSeconds(0.3f);
                    
                    Debug.Log("Animation_Check_Left");
                    SwipeLeft();

                }
            }
        }    

        //yield return new WaitForSeconds(0.3f);
        //SwipeLeft();
      //  SwipeControl();
    }

    IEnumerator JumpAnimUp()
    {
        if (yDistance > xDistance && GameManager.inst.isGrounded)
        {
            if (Distance.y > 0)
            {
                GameManager.inst.isGrounded = false;

                GameManager.inst.player.GetComponent<PlayerScript>().JumpAnim();
                
                yield return new WaitForSeconds(0.3f);
               
                Debug.Log("Animation_Check_Up");
                SwipeUp();
            }
        }
                
        //yield return new WaitForSeconds(0.3f);
        //SwipeUp();
      // SwipeControl();
    }

    

}
