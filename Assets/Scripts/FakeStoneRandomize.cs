using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeStoneRandomize : MonoBehaviour
{
   public GameObject[] stoneLayers;
   [SerializeField] private GameObject[] requiredStoneLayers;
    bool check = false;

   [SerializeField] int[] radNum;
    int index;
    int rand;
    int counter;
    public int randomLayersNumber;
   
    
    void Start()
    {
        requiredStoneLayers = new GameObject[randomLayersNumber];
        radNum = new int[randomLayersNumber];

        
        GetUniqueRandom();
        RandomLayersPick();
        RandomStoneFake();
    }

    
    void RandomStoneFake()
    {
        GameObject pick;
        int rndom;
        for (int i = 0; i < randomLayersNumber; i++)
        {
            rndom = Random.Range(0, 3);
            pick = requiredStoneLayers[i];
            pick.transform.GetChild(rndom).GetComponent<DestinationStone>().control = true;
        }
    }

    void RandomLayersPick()
    {
        for (int i = 0; i < randomLayersNumber; i++)
        {
            requiredStoneLayers[i] = stoneLayers[radNum[i]];
        }
    }
    void GetUniqueRandom()
    {
        int rand = GetRandomNumber();
        if (counter< randomLayersNumber)
        {
            for (int i = 0; i < randomLayersNumber; i++)
            {
                if(rand == radNum[i])
                {
                    check = true;
                }
            }

            if(check == true)
            {
                check = false;
                GetUniqueRandom();
            }
            else
            {
                radNum[index] = rand;
                index++;
                counter++;
                GetUniqueRandom();
            }
        }
    }
    int GetRandomNumber()
    {
        int rand = Random.Range(0, stoneLayers.Length);
       // Debug.Log("Random = " + rand);
        return rand;
    }
}
