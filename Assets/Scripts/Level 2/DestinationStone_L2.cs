using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationStone_L2 : MonoBehaviour
{
    public CurrentStone currentStone;
  

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Debug.Log("current Stone Enum =" + currentStone);
            GameManager.inst.updateStoneState_L2.TargetSetForwardNextStone(currentStone);
            GameManager.inst.updateStoneState_L2.TargetSetLeftNextStone(currentStone);
            GameManager.inst.updateStoneState_L2.TargetSetRightNextStone(currentStone);

        }

        if (collision.gameObject.CompareTag("AIBot"))
        {
            //For AI Bot
            GameManager.inst.updateStoneState_Bot_L2.TargetSetForwardNextStone(currentStone);
            GameManager.inst.updateStoneState_Bot_L2.TargetSetLeftNextStone(currentStone);
            GameManager.inst.updateStoneState_Bot_L2.TargetSetRightNextStone(currentStone);
        }
    }
}