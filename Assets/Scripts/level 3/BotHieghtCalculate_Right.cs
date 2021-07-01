using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotHieghtCalculate_Right : MonoBehaviour
{
    public bool rightJump;

    public AI_L3 AIBot;

    public float hieght;


    private void Update()
    {


        if (GameManager.inst.updateStoneState_Bot.right.transform.parent.localPosition.y == hieght && AIBot.currntSton.transform.localPosition.y == hieght)
        {
            rightJump = true;

            //AIBot.botHieght_Left.leftJump = false;
            //AIBot.botHieght_Middle.middleJump = false;

        }
        else
        {
            rightJump = false;

            //AIBot.botHieght_Left.leftJump = true;
            //AIBot.botHieght_Middle.middleJump = true;
        }

    }
}
