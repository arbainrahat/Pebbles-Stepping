using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllar : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;

    public float moveSpeed;
    private bool groundCheck = false;
    private bool stoneCheck = false;
    public float jumpHieght;
    public float jumpDistance;
    
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

   
    void Update()
    {
        if (groundCheck)
        {
            rb.velocity = new Vector3(0f, 0f, moveSpeed);
            animator.SetBool("run", true);
        }
        else if(!groundCheck)
        {
           animator.SetBool("run", false);
            print("stone"+groundCheck);
        }

        //...........Jump.................

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("run", false);
            animator.SetBool("jump", true);
            rb.velocity = new Vector3(0f,jumpHieght,jumpDistance);
        }
        else
        {
            animator.SetBool("jump" ,false);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            rb.velocity = new Vector3(jumpDistance, jumpHieght, 0f);
        }
        
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            groundCheck = true;
        }

        if (collision.gameObject.CompareTag("stone"))
        {
            stoneCheck = true;
            
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            groundCheck = false;
        }

        if (collision.gameObject.CompareTag("stone"))
        {
            stoneCheck = false;
        }
    }

}
