using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HieghtCheck_Left : MonoBehaviour
{
    

    public Transform leftChild;

    public PlayerScript playerScript;
   
    public float hieght;

    private void Update()
    {
        
        if (GameManager.inst.updateStoneState.left.transform.parent.localPosition.y == hieght && playerScript.currntStone.transform.localPosition.y == hieght)
        {
           
            GameManager.inst.updateStoneState.leftTargetStone = GameManager.inst.updateStoneState.left;
          //  GameManager.inst.fallCheckLeft = false;
        }
        else
        {
            GameManager.inst.updateStoneState.leftTargetStone = leftChild.gameObject;
          //  GameManager.inst.fallCheckLeft = true;


        }
    }

    
}
