using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_L5 : MonoBehaviour
{
   public bool grounded;
    
    private Animator animator;
    public JumpProjectile jumpProjectile;
    public UpdateStoneState updateStoneState;

   
    private int rotIndex;
    public CapsuleCollider botCollider;
    public BoxCollider boxCollider_Poll;



    void Start()
    {

        Physics.IgnoreCollision(botCollider, GameManager.inst.playerCollider);
        Physics.IgnoreCollision(botCollider, boxCollider_Poll);
        animator = gameObject.GetComponent<Animator>();
        
    }


    void Update()
    {

        Physics.IgnoreCollision(botCollider, GameManager.inst.playerCollider);
        Physics.IgnoreCollision(botCollider, boxCollider_Poll);
        StartCoroutine(JumpAnimUp());
    }



    IEnumerator JumpAnimUp()
    {
        if (grounded)
        {
            grounded = false;
            JumpAnim();

            yield return new WaitForSeconds(0.3f);

            ForwardMove();
           
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

            
                transform.rotation = GameManager.inst.level5_PlayerRotation[rotIndex].rotation;
                rotIndex++;
           
            //transform.rotation = GameManager.inst.level5_PlayerRotation[rotIndex].rotation;
            //rotIndex++;
           // animator.SetBool("jump", false);
            StartCoroutine(groundBool());
        }

        //On reach End Point
        if (collision.gameObject.CompareTag("GroundEnd"))
        {
            gameObject.GetComponent<AI_L5>().enabled = false;
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
        yield return new WaitForSeconds(1.9f);
        
       // yield return new WaitForSeconds(.8f);
        animator.SetBool("jump", false);
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

    //.....Animator Control .......
    void JumpAnim()
    {
        animator.SetBool("jump", true);
    }
}
