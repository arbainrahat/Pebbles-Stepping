using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotHieghtCalculate_Middle : MonoBehaviour
{
    public bool middleJump;

    public AI_L3 AIBot;

    public float hieght;


    private void Update()
    {


        if (GameManager.inst.updateStoneState_Bot.frwd.transform.parent.localPosition.y == hieght && AIBot.currntSton.transform.localPosition.y == hieght)
        {
            middleJump = true;

            //AIBot.botHieght_Left.leftJump = false;
            //AIBot.botHieght_Right.rightJump = false;
        }
        else
        {
            middleJump = false;

            //AIBot.botHieght_Left.leftJump = true;
            //AIBot.botHieght_Right.rightJump = true;
        }

    }
}
