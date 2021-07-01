using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Bot : MonoBehaviour
{
    bool grounded;
    bool rightSideCheck = false;
    bool leftSideCheck = false;
    public Animator animator;
    public JumpProjectile jumpProjectile;
    public UpdateStoneState updateStoneState;

    [SerializeField] int randomNumber;
    [SerializeField] int randomValue;
    [SerializeField] bool startCheck = false;

    public CapsuleCollider botCollider;


    public bool mainMenuBot;
    

    void Start()
    {
        
            Physics.IgnoreCollision(botCollider, GameManager.inst.playerCollider);
            animator = gameObject.GetComponent<Animator>();
            Bot_RandomJumpStart();
       
    }


    void Update()
    {
        if(startCheck == false)
        {
            if (randomValue == 0)
            {
                StartCoroutine(JumpAnimRight());
            }
            else if (randomValue == 1)
            {
                StartCoroutine(JumpAnimUp());
            }
            else if (randomValue == 2)
            {
                StartCoroutine(JumpAnimLeft());
            }

            startCheck = true;
        }
        else
        {
            BotContinueJump();
        }

        Physics.IgnoreCollision(botCollider,GameManager.inst.playerCollider);
    }

    void GetRandomNumber()
    {
        randomNumber = Random.Range(0, 3);
    }
    int GenerateRandom(int min,int max)
    {
        int i = Random.Range(min,max);
        return i;
    }

    void Bot_RandomJumpStart()
    {
        GetRandomNumber();
        if(randomNumber == PlayerPrefs.GetInt("random"))
        {  
            Bot_RandomJumpStart();
        }
        else
        {
            randomValue = randomNumber;
            PlayerPrefs.SetInt("random", randomValue);
        }

    }

    void BotContinueJump()
    {
        randomNumber = PlayerPrefs.GetInt("random");
        if(randomNumber == 2)
        {
            randomValue = GenerateRandom(1, 3);
           // Debug.Log("Case 1 = " + randomValue);
            if(randomValue == 1)
            {
                StartCoroutine(JumpAnimRight());
            }
            else
            {
                StartCoroutine(JumpAnimUp());
            }
        }
        else if(randomNumber == 0)
        {
            randomValue = GenerateRandom(0, 2);
           // Debug.Log("Case 2 = " + randomValue);
            if (randomValue == 1)
            {
                StartCoroutine(JumpAnimLeft());
            }
            else
            {
                StartCoroutine(JumpAnimUp());
            }
        }
        else
        {
            Bot_RandomJumpStart();
            startCheck = false;
           // Debug.Log("Case 3 = " + randomNumber);
        }
        PlayerPrefs.SetInt("random", randomValue);
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
                if (GameManager.inst.canJump == false)
                {
                    jumpProjectile.LunchNow();
                }
                
               
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

                if (GameManager.inst.canJump == false)
                {
                    jumpProjectile.LunchNow();
                }


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
            
        }
    }

    //.............Collision Check............

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
        }

        if (collision.gameObject.tag == "Ground" || collision.gameObject.CompareTag("MiddleStone") || collision.gameObject.CompareTag("RightStone") || collision.gameObject.CompareTag("LeftStone"))
        {
            //transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            if(mainMenuBot == true)
            {
                transform.eulerAngles.Set(0f, 90, 0f);
            }
            else
            {
                transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            }
            
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
        if(collision.gameObject.CompareTag("GroundEnd"))
        {
           gameObject.GetComponent<AI_Bot>().enabled = false;

            
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
        if (other.CompareTag("Player"))
        {
            gameObject.GetComponent<CapsuleCollider>().isTrigger = false;
        }
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
}
