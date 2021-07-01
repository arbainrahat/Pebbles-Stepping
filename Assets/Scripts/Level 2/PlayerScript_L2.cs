using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript_L2 : MonoBehaviour
{
    private Animator animator;
    public UpdateStoneState_L2 updateStoneState_L2;
    
    public bool rightSideCheck = false;
    public bool leftSideCheck = false;

    //public Vector3 rotationAngles;
    private GameObject targetHurdle;
    

    public Collider[] Colliders;
    public Rigidbody[] rigidbodies;
   // public bool disableCollision = false;
    public Rigidbody rb;
    public float failHieght;
    public bool jumpContrl;

    public SkinnedMeshRenderer render;
    public AudioSource audioSource;
    public AudioClip jump;

    private void OnEnable()
    {

        int i = PlayerPrefs.GetInt("PlayerCloth");
        Material[] mat = render.materials;
        mat[0] = GameManager.inst.charSelect.pantMat[i];
        mat[1] = GameManager.inst.charSelect.hairMat[i];
        mat[3] = GameManager.inst.charSelect.upperMat[i];
        mat[4] = GameManager.inst.charSelect.shirtMat[i];
        mat[5] = GameManager.inst.charSelect.shoeMat[i];
        render.materials = mat;
    }

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();

    }

    private void Update()
    {
        if (transform.position.y < failHieght)
        {
            Debug.Log("level Fail");
            GameManager.inst.levelFailedPanel.SetActive(true);
        }
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
        //Debug.Log("ForwardMove_PlayerScript");
        updateStoneState_L2.ForwardTargetSet();
        GameManager.inst.scoreScript.score += 10;
        GameManager.inst.scoreScript.FillBar();
        GameManager.inst.scoreScript.AffectsShow();
    }

    public void RightMove()
    {
        audioSource.clip = jump;
        audioSource.Play();

        transform.Rotate(0f, 35, 0f);
        updateStoneState_L2.RightTargetSet();
        GameManager.inst.scoreScript.score += 10;
        GameManager.inst.scoreScript.FillBar();
        GameManager.inst.scoreScript.AffectsShow();
    }

    public void LeftMove()
    {
        audioSource.clip = jump;
        audioSource.Play();

        transform.Rotate(0f, -35, 0f);
        updateStoneState_L2.LeftTargetSet();
        GameManager.inst.scoreScript.score += 10;
        GameManager.inst.scoreScript.FillBar();
        GameManager.inst.scoreScript.AffectsShow();

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.CompareTag("MiddleStone") || collision.gameObject.CompareTag("RightStone") || collision.gameObject.CompareTag("LeftStone"))
        {
           // Debug.Log("Collision");
            StartCoroutine(groundBool());
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

        //Make Player child of Hurdle

        if (collision.gameObject.tag == "RightStone" || collision.gameObject.tag == "LeftStone")
        {
            targetHurdle = collision.gameObject;
            //transform.parent = targetHurdle.transform;
            // Invoke("MakeParent", 0.4f);
            StartCoroutine(Unparent());

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

        if (collision.gameObject.tag == "RightStone" || collision.gameObject.tag == "LeftStone")
        {
            // disableCollision = false;
             StopAllCoroutines();
           
        }
    }

   

    public void JumpAnim()
    {
        animator.SetBool("jump", true);
    }


    IEnumerator groundBool()
    {
        // Debug.Log("Courantine");
        yield return new WaitForSeconds(.3f);
        GameManager.inst.isGrounded = true;
        // Debug.Log("Isgrounded = " + GameManager.inst.isGrounded);

    }

    IEnumerator Unparent()
    {
        yield return new WaitForSeconds(.2f);
        transform.parent = targetHurdle.transform;
        yield return new WaitForSeconds(1.5f);

        transform.parent = null;
        jumpContrl = true;
        //  targetHurdle.GetComponent<Animator>().SetBool("Fast", true);

        // yield return new WaitForSeconds(2f);
        // UpForce();
       // targetHurdle.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        rb.constraints = RigidbodyConstraints.None;
        animator.enabled = false;
        EnableColliders();
        DisableKinematic();
        GameManager.inst.cameraFollow.lookAt = true;
        // StopAllCoroutines();
        StopCoroutine("Unparent");

    }

    public  void StopCoroutne()
    {
        StopCoroutine("Unparent");
    }

    void UpForce()
    {
        Debug.Log("UP Force");
        rb.AddForce(Vector3.up * 200, ForceMode.Impulse);
    }
}
