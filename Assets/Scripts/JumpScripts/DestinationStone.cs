using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CurrentStone { Right, Middle, Left };

public class DestinationStone : MonoBehaviour
{
    public CurrentStone currentStone;
    public bool control=false;
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Debug.Log("current Stone Enum =" + currentStone);
            GameManager.inst.updateStoneState.TargetSetForwardNextStone(currentStone);
            GameManager.inst.updateStoneState.TargetSetLeftNextStone(currentStone);
            GameManager.inst.updateStoneState.TargetSetRightNextStone(currentStone);

            if (control == true)
            {
                GameManager.inst.canJump = true;
                gameObject.GetComponent<Rigidbody>().isKinematic = false;
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                Invoke("Fail", 0.9f);
            }


        }
        if (collision.gameObject.CompareTag("AIBot"))
        {
            //For AI Bot
            GameManager.inst.updateStoneState_Bot.TargetSetForwardNextStone(currentStone);
            GameManager.inst.updateStoneState_Bot.TargetSetLeftNextStone(currentStone);
            GameManager.inst.updateStoneState_Bot.TargetSetRightNextStone(currentStone);
        }
    }

    void Fail()
    {
        GameManager.inst.levelFailedPanel.SetActive(true);
    }
}
