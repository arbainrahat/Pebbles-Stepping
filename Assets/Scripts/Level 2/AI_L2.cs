using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_L2 : MonoBehaviour
{
  public  bool grounded;
    bool rightSideCheck = false;
    bool leftSideCheck = false;
    private Animator animator;
    public JumpProjectile jumpProjectile;
    public UpdateStoneState_L2 updateStoneState;

    [SerializeField] int randomNumber;
    //[SerializeField] int randomValue;
    [SerializeField] bool startCheck = false;

    public CapsuleCollider botCollider;


    void Start()
    {
        Physics.IgnoreCollision(botCollider, GameManager.inst.playerCollider);
        animator = gameObject.GetComponent<Animator>();
        randomNumber = 2;
    }


    void Update()
    {
        
        BotContinueJump();

        Physics.IgnoreCollision(botCollider, GameManager.inst.playerCollider);
    }

    void BotContinueJump()
    {    
          if (randomNumber == 2)
          {
            StartCoroutine(JumpAnimLeft());
           
          }
          else if(randomNumber == 0)
          {
            StartCoroutine(JumpAnimRight()); 
          }
    }


    IEnumerator JumpAnimRight()
    {
        if (rightSideCheck == false && grounded)
        {
            grounded = false;
            JumpAnim();

            yield return new WaitForSeconds(0.3f);
            
            if (rightSideCheck == false)
            {
                RightMove();      
                jumpProjectile.LunchNow();
                randomNumber = 2;
            }
        }
    }

    IEnumerator JumpAnimLeft()
    {

        if (leftSideCheck == false && grounded)
        {
            grounded = false;
            JumpAnim();

            yield return new WaitForSeconds(0.3f);

            if (leftSideCheck == false)
            {
              LeftMove();
              jumpProjectile.LunchNow();
              randomNumber = 0;
            }
        }

    }

    

    //.............Collision Check............

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Player"))
        //{
        //    gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
        //}

        if (collision.gameObject.tag == "Ground" || collision.gameObject.CompareTag("MiddleStone") || collision.gameObject.CompareTag("RightStone") || collision.gameObject.CompareTag("LeftStone"))
        {
            transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            animator.SetBool("jump", false);
            StartCoroutine(groundBool());
        }

        if (collision.gameObject.tag == "RightStone")
        {
            rightSideCheck = true;
        }
        else if (collision.gameObject.tag == "LeftStone")
        {
            leftSideCheck = true;
        }

        //On reach End Point
        if (collision.gameObject.CompareTag("GroundEnd"))
        {
            gameObject.GetComponent<AI_L2>().enabled = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //Swipe control for sides
        if (collision.gameObject.tag == "RightStone")
        {
            rightSideCheck = false;
        }

        if (collision.gameObject.tag == "LeftStone")
        {
            leftSideCheck = false;
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        gameObject.GetComponent<CapsuleCollider>().isTrigger = false;
    //    }
    //}

    IEnumerator groundBool()
    {
        yield return new WaitForSeconds(.8f);
        grounded = true;
    }

    //.....Movement methods.......
    

    void RightMove()
    {
        transform.Rotate(0f, 35, 0f);
        updateStoneState.RightTargetSet();
    }

    void LeftMove()
    {
        transform.Rotate(0f, -35, 0f);
        updateStoneState.LeftTargetSet();
    }

    //.....Animator Control .......
    void JumpAnim()
    {
        animator.SetBool("jump", true);
    }
}
