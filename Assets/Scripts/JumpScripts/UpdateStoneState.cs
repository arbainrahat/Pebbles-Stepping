using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateStoneState : MonoBehaviour
{
    public JumpProjectile jumpProjectile;
    public GameObject[] stoneStates;
    [SerializeField] private GameObject currentStoneState;
    public int stateIndex = 0;
     public GameObject frwdTargetStone;
     public GameObject rightTargetStone;
     public GameObject leftTargetStone;


    //Reference for Level 3

    public GameObject frwd;
    public GameObject right;
    public GameObject left;
    //......[Hint] : Right --> Left ...........

    void Start()
    {
        currentStoneState = stoneStates[stateIndex];
        //ForwardTargetSet();
        //RightTargetSet();
        //LeftTargetSet();
    }

    public void ForwardTargetSet()
    {
        Debug.Log("Forward_Target_Set");
        if(stateIndex == 0)
        {
            frwdTargetStone = currentStoneState.GetComponent<GetChildsGameObjects>().middleStone;
           // Debug.Log("ForwardTargetSet");
            jumpProjectile.SetProjectileTarget(frwdTargetStone.transform);
            stateIndex++;
            currentStoneState = stoneStates[stateIndex];
           // Debug.Log("curntStone State =" + currentStoneState);
        }
        else
        {
            
           // Debug.Log("ForwardTargetSet");
            jumpProjectile.SetProjectileTarget(frwdTargetStone.transform);
            if (stateIndex < stoneStates.Length -1)
            {
                stateIndex++;
             //   Debug.Log("Index Error");
                currentStoneState = stoneStates[stateIndex];
            }
            
           
        }
        
    }

    public void RightTargetSet()
    {
        if(stateIndex == 0)
        {
            rightTargetStone = currentStoneState.GetComponent<GetChildsGameObjects>().rightStone;
            jumpProjectile.SetProjectileTarget(rightTargetStone.transform);
            stateIndex++;
            currentStoneState = stoneStates[stateIndex];
        }
        else
        {
            jumpProjectile.SetProjectileTarget(rightTargetStone.transform);
            if (stateIndex < stoneStates.Length -1)
            {
                stateIndex++;
                currentStoneState = stoneStates[stateIndex];
            }
            
            
        }
        
    }

    public void LeftTargetSet()
    {
        if(stateIndex == 0)
        {
            leftTargetStone = currentStoneState.GetComponent<GetChildsGameObjects>().leftStone;
            jumpProjectile.SetProjectileTarget(leftTargetStone.transform);
            stateIndex++;
            currentStoneState = stoneStates[stateIndex];
        }
        else
        {
            jumpProjectile.SetProjectileTarget(leftTargetStone.transform);
            if (stateIndex < stoneStates.Length -1)
            {
                stateIndex++;
                currentStoneState = stoneStates[stateIndex];
            }
            
        }
        
    }


    //Set Target Stone for forward
    public void TargetSetForwardNextStone(CurrentStone curntStone)
    {
       // Debug.Log("curntStone Enum fwd =" + curntStone);
        if (curntStone == CurrentStone.Middle)   
        {
         //   Debug.Log("curntStone Enum fwd =" + curntStone);
            frwdTargetStone = currentStoneState.GetComponent<GetChildsGameObjects>().middleStone;
            frwd = frwdTargetStone;
        }
        else if (curntStone == CurrentStone.Left)
        {
            frwdTargetStone = currentStoneState.GetComponent<GetChildsGameObjects>().leftStone;
            frwd = frwdTargetStone;
        }
        else if (curntStone == CurrentStone.Right)
        {
            frwdTargetStone = currentStoneState.GetComponent<GetChildsGameObjects>().rightStone;
            frwd = frwdTargetStone;
        }
    }

    //Set Target Stone For Left
    public void TargetSetLeftNextStone(CurrentStone curntStone)
    {
        if (curntStone == CurrentStone.Middle)
        {
            leftTargetStone = currentStoneState.GetComponent<GetChildsGameObjects>().leftStone;
            left = leftTargetStone;
        }
        else if (curntStone == CurrentStone.Right)
        {
            leftTargetStone = currentStoneState.GetComponent<GetChildsGameObjects>().middleStone;
            left = leftTargetStone;
        }
    }

    //Set Target Stone For Right
    public void TargetSetRightNextStone(CurrentStone curntStone)
    {
        if (curntStone == CurrentStone.Middle)
        {
            rightTargetStone = currentStoneState.GetComponent<GetChildsGameObjects>().rightStone;
            right = rightTargetStone;
        }
        else if (curntStone == CurrentStone.Left)
        {
            rightTargetStone = currentStoneState.GetComponent<GetChildsGameObjects>().middleStone;
            right = rightTargetStone;
        }
    }

    public void SetAIAgainTarget()
    {
        currentStoneState = stoneStates[stateIndex];
    }

}
