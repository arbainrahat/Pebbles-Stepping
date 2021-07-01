using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HieghtCheck_Right : MonoBehaviour
{
  
  
    public Transform rightChild;

    public PlayerScript playerScript;

    public float hieght;

   
    private void Update()
    {


        if (GameManager.inst.updateStoneState.right.transform.parent.localPosition.y == hieght && playerScript.currntStone.transform.localPosition.y == hieght)
        {
            GameManager.inst.updateStoneState.rightTargetStone = GameManager.inst.updateStoneState.right;

          //  GameManager.inst.fallCheckRight = false;
        }
        else
        {
            GameManager.inst.updateStoneState.rightTargetStone = rightChild.gameObject;
            //GameManager.inst.fallCheckRight = true;
        }

    }

   

}
