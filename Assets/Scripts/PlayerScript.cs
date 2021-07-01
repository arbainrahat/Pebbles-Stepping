using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Animator animator;
    public UpdateStoneState updateStoneState;
   // public bool groundCheck = false;
    public bool rightSideCheck = false;
    public bool leftSideCheck = false;
    private int rotIndex;

    public Collider[] Colliders;
    public Rigidbody[] rigidbodies;

    //For Level 3
    public bool level3;
    public GameObject currntStone;

    public HieghtCheck_Right HieghtCheck_R;
    public HieghtCheck_Middle HieghtCheck_M;
    public HieghtCheck_Left HieghtCheck_L;

    public SkinnedMeshRenderer render;
    public AudioSource audioSource;
    public AudioClip jump;

    

    private void OnEnable()
    {
        Debug.Log("Pref For Char" + PlayerPrefs.GetInt("PlayerCloth"));
        int i = PlayerPrefs.GetInt("PlayerCloth");
        Material[] mat = render.materials;
        mat[0] = GameManager.inst.charSelect.pantMat[i];
        mat[1] = GameManager.inst.charSelect.hairMat[i];
        mat[3] = GameManager.inst.charSelect.upperMat[i];
        mat[4] = GameManager.inst.charSelect.shirtMat[i];
        mat[5] = GameManager.inst.charSelect.shoeMat[i];
        render.materials = mat;
    }

    public void SetBoolLeve3()
    {
        level3 = true;
    }


    public void SetPlayerScript_UpdateStoneState(UpdateStoneState state)
   {
        updateStoneState = state;
   }

    private void Start()
    {
        Debug.Log(this);
        animator = gameObject.GetComponent<Animator>();
        rotIndex = 0;
    }

   

    public void EnableColliders()
    {
        for (int i = 0; i < Colliders.Length; i++)
        {
            Colliders[i].enabled = true;
        }
    }

    public void DisableKinematic()
    {
        for (int i = 0; i < rigidbodies.Length; i++)
        {
            rigidbodies[i].isKinematic = false;
        }
    }

    public void ForwardMove()
    {
        audioSource.clip = jump;
        audioSource.Play();

        Debug.Log("ForwardMove_PlayerScript");
        //if(GameManager.inst.isGrounded == true)
        //{
          //  animator.SetBool("jump", true);
            updateStoneState.ForwardTargetSet();

        if(GameManager.inst.updateStoneState.frwdTargetStone == HieghtCheck_M.middleChild.gameObject && level3 && GameManager.inst.updateStoneState.stateIndex != 9)
        {
            Invoke("FallDown", 0.3f);
            StartCoroutine(LevelFail());
        }

        //}
        GameManager.inst.scoreScript.score += 10;
        GameManager.inst.scoreScript.FillBar();
        GameManager.inst.scoreScript.AffectsShow();

    }

    public void RightMove()
    {
        audioSource.clip = jump;
        audioSource.Play();
        //if(GameManager.inst.isGrounded == true)
        //{
        //  animator.SetBool("jump", true);
        transform.Rotate(0f,35,0f);
            updateStoneState.RightTargetSet();

        if (GameManager.inst.updateStoneState.rightTargetStone == HieghtCheck_R.rightChild.gameObject && level3 && GameManager.inst.updateStoneState.stateIndex != 9)
        {

            Invoke("FallDown", 0.3f);
            StartCoroutine(LevelFail());
        }
        //}
        GameManager.inst.scoreScript.score += 10;
        GameManager.inst.scoreScript.FillBar();
        GameManager.inst.scoreScript.AffectsShow();
    }

    public void LeftMove()
    {
        audioSource.clip = jump;
        audioSource.Play();
        //if(GameManager.inst.isGrounded == true)
        //{
        //  animator.SetBool("jump",true);
        transform.Rotate(0f, -35, 0f);
            updateStoneState.LeftTargetSet();
        //}

        if (GameManager.inst.updateStoneState.leftTargetStone == HieghtCheck_L.leftChild.gameObject && level3 && GameManager.inst.updateStoneState.stateIndex != 9)
        {

            Invoke("FallDown", 0.3f);
            StartCoroutine(LevelFail());

        }
        GameManager.inst.scoreScript.score += 10;
        GameManager.inst.scoreScript.FillBar();
        GameManager.inst.scoreScript.AffectsShow();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(level3 == true)
        {
            if (collision.gameObject.CompareTag("MiddleStone") || collision.gameObject.CompareTag("RightStone") || collision.gameObject.CompareTag("LeftStone"))
            {
                currntStone = collision.gameObject;
            }
        }
        

        if (collision.gameObject.tag == "Ground" || collision.gameObject.CompareTag("MiddleStone") || collision.gameObject.CompareTag("RightStone") || collision.gameObject.CompareTag("LeftStone"))
        {
            Debug.Log("Collision");
            StartCoroutine(groundBool());
             

            if (GameManager.inst.lvl5Rot){
                transform.rotation = GameManager.inst.level5_PlayerRotation[rotIndex].rotation;
                rotIndex++;
            }
            else if (GameManager.inst.lvl4Rot)
            {
                transform.rotation = GameManager.inst.level4_PlayerRotation[rotIndex].rotation;
                rotIndex++;
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

    public void JumpAnim()
    {
        animator.SetBool("jump", true);
    }

    //public void JumpAnimFalse()
    //{
    //    animator.SetBool("jump", false);
    //}

    IEnumerator groundBool()
    {
       // Debug.Log("Courantine");
        yield return new WaitForSeconds(.3f);
        GameManager.inst.isGrounded = true;
       // Debug.Log("Isgrounded = " + GameManager.inst.isGrounded);
       
    }

    void FallDown()
    {
        GameManager.inst.player.GetComponent<PlayerScript>().EnableColliders();
        GameManager.inst.player.GetComponent<PlayerScript>().DisableKinematic();
        GameManager.inst.player.GetComponent<Animator>().enabled = false;
       // GameManager.inst.player.GetComponent<CapsuleCollider>().enabled = false;
    }

    IEnumerator LevelFail()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.inst.levelFailedPanel.SetActive(true);
    }
}
