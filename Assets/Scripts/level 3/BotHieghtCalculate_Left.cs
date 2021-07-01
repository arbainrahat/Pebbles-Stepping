using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotHieghtCalculate_Left : MonoBehaviour
{
    public bool leftJump;

    public AI_L3 AIBot;

    public float hieght;


    private void Update()
    {


        if (GameManager.inst.updateStoneState_Bot.left.transform.parent.localPosition.y == hieght && AIBot.currntSton.transform.localPosition.y == hieght)
        {
            leftJump = true;

            //AIBot.botHieght_Right.rightJump = false;
            //AIBot.botHieght_Middle.middleJump = false;
        }
        else
        {
            leftJump = false;

            //AIBot.botHieght_Right.rightJump = true;
            //AIBot.botHieght_Middle.middleJump = true;
        }

    }
}
