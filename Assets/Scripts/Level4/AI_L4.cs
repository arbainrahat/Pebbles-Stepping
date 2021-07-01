using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_L4 : MonoBehaviour
{

    public bool grounded;

    private Animator animator;
    public JumpProjectile jumpProjectile;
    public UpdateStoneState updateStoneState;


    private int rotIndex;
    public CapsuleCollider botCollider;
    public BoxCollider boxCollider_Poll_1;
    public BoxCollider boxCollider_Poll_2;
    public BoxCollider boxCollider_Poll_3;
    public BoxCollider boxCollider_Poll_4;

    private CapsuleCollider playerColider;

    private void Awake()
    {
        
    }

    void Start()
    {
        playerColider = GameManager.inst.player.GetComponent<CapsuleCollider>();
        Physics.IgnoreCollision(botCollider, playerColider);
        Physics.IgnoreCollision(botCollider, boxCollider_Poll_1);
        Physics.IgnoreCollision(botCollider, boxCollider_Poll_2);
        Physics.IgnoreCollision(botCollider, boxCollider_Poll_3);
        Physics.IgnoreCollision(botCollider, boxCollider_Poll_4);
        animator = gameObject.GetComponent<Animator>();

    }


    void Update()
    {

        Physics.IgnoreCollision(botCollider, playerColider);
        Physics.IgnoreCollision(botCollider, boxCollider_Poll_1);
        Physics.IgnoreCollision(botCollider, boxCollider_Poll_2);
        Physics.IgnoreCollision(botCollider, boxCollider_Poll_3);
        Physics.IgnoreCollision(botCollider, boxCollider_Poll_4);
        StartCoroutine(JumpAnimUp());
    }



    IEnumerator JumpAnimUp()
    {
        if (grounded)
        {
            grounded = false;
            JumpAnim();

            
            yield return new WaitForSeconds(0.5f);

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


            transform.rotation = GameManager.inst.level4_PlayerRotation[rotIndex].rotation;
            rotIndex++;

            
            // animator.SetBool("jump", false);
            StartCoroutine(groundBool());
        }

        //On reach End Point
        if (collision.gameObject.CompareTag("GroundEnd"))
        {
            gameObject.GetComponent<AI_L4>().enabled = false;
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
        Debug.Log("AI Leve4");
        yield return new WaitForSeconds(1f);

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
