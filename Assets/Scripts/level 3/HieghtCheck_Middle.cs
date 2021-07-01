using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HieghtCheck_Middle : MonoBehaviour
{
    
    
    public Transform middleChild;
    
    public PlayerScript playerScript;
   
    public float hieght;

    private void Update()
    {

        if (GameManager.inst.updateStoneState.frwd.transform.parent.localPosition.y == hieght && playerScript.currntStone.transform.localPosition.y == hieght)
        {
            
            GameManager.inst.updateStoneState.frwdTargetStone = GameManager.inst.updateStoneState.frwd;
           // GameManager.inst.fallCheckMiddle = false;
        }
        else
        {
            GameManager.inst.updateStoneState.frwdTargetStone = middleChild.gameObject;

          //  GameManager.inst.fallCheckMiddle = true;


        }
       
    }

    
}
