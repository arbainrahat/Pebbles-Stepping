using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_L3 : MonoBehaviour
{
    public  bool grounded;
    bool rightSideCheck = false;
    bool leftSideCheck = false;
    private Animator animator;
    public JumpProjectile jumpProjectile;
    public UpdateStoneState updateStoneState;

    public CapsuleCollider botCollider;

    public BotHieghtCalculate_Right botHieght_Right;
    public BotHieghtCalculate_Middle botHieght_Middle;
    public BotHieghtCalculate_Left botHieght_Left;

    public GameObject currntSton;

    public bool firstRun;

    public Transform botSpwanPos;

    void Start()
    {

        Physics.IgnoreCollision(botCollider, GameManager.inst.player.GetComponent<CapsuleCollider>());
        animator = gameObject.GetComponent<Animator>();

    }


    void Update()
    {
        if(firstRun == false)
        {
           
            if (GameManager.inst.updateStoneState_Bot.frwd.transform.parent.localPosition.y == botHieght_Middle.hieght)
            {
                StartCoroutine(JumpAnimUp());
                Debug.Log("Set All Bot AI 3");
            }
           
            
        }
         

         if(updateStoneState.stateIndex == 8 && currntSton.transform.position.y == -0.01570963f)
        {
            StartCoroutine(JumpAnimUp());
        }

        else
        {
            if (firstRun)
            {
                BotContinueJump();
            }
        }

        Physics.IgnoreCollision(botCollider, GameManager.inst.playerCollider);

        if(updateStoneState.stateIndex == 8)
        {
            Debug.Log("State Index is : " + updateStoneState.stateIndex);
            StartCoroutine(JumpAnimUp());
        }
    }

    

    void BotContinueJump()
    {

        if (botHieght_Right.rightJump == true)
        {
            StartCoroutine(JumpAnimRight());
        }


        if (botHieght_Left.leftJump == true)
        {
            StartCoroutine(JumpAnimLeft());
        }

        if (botHieght_Middle.middleJump == true)
        {
           StartCoroutine(JumpAnimUp());
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
                    
            }
        }

    }

    IEnumerator JumpAnimUp()
    {
        if (grounded)
        {
            grounded = false;
            JumpAnim();

            yield return new WaitForSeconds(0.3f);

            ForwardMove();
            firstRun = true;
        }
    }

    //.............Collision Check............

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Player"))
        //{
        //    gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
        //}

       

        if (collision.gameObject.CompareTag("MiddleStone") || collision.gameObject.CompareTag("RightStone") || collision.gameObject.CompareTag("LeftStone"))
        {
            currntSton = collision.gameObject;
        }

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
            gameObject.GetComponent<AI_L3>().enabled = false;
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

    private void OnTriggerExit(Collider other)
    {
        //if (other.CompareTag("Player"))
        //{
        //    gameObject.GetComponent<CapsuleCollider>().isTrigger = false;
        //}
    }

    IEnumerator groundBool()
    {
        yield return new WaitForSeconds(.8f);
        grounded = true;
    }

    //.....Movement methods.......
    void ForwardMove()
    {
        updateStoneState.ForwardTargetSet();

        if (GameManager.inst.canJump == false)
        {
            jumpProjectile.LunchNow();
            
        }

    }

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Death"))
        {
            Debug.Log("Bot Death");
            transform.SetPositionAndRotation(botSpwanPos.position, botSpwanPos.rotation);
            updateStoneState.stateIndex = 0;
            updateStoneState.SetAIAgainTarget();
            firstRun = false;
            StopAllCoroutines();
        }

    }
}
