using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject full;
    public GameObject fR;
    public GameObject fM;
    public GameObject RightLeft;
    public GameObject middle;

    public GameObject _Tutorial;

    public GameObject[] aIBot;
    public GameObject _swipe;

    public void Tutorl_L1(string s)
    {
       
        if (PlayerPrefs.GetInt("leve1") == 0)
        {
            _swipe.SetActive(false);
            aIBot[0].GetComponent<AI_Bot>().enabled = false;
            //  Time.timeScale = 0;
            gameObject.SetActive(true);
            if (s == "F")
            {
                Debug.Log("Tutorial 1  ");
                StartCoroutine(FullSide());
                // full.SetActive(true);
            }
            else if (s == "RL")
            {
                StartCoroutine(_RightLeft());
                // RightLeft.SetActive(true);
            }
            else if (s == "M")
            {
                middle.SetActive(true);
            }

            PlayerPrefs.SetInt("leve1", 1);
            
        }
        

    }

    public void Tutorl_L2(string s)
    {
       
        if (PlayerPrefs.GetInt("leve2") == 0)
        {
            _swipe.SetActive(false);
            aIBot[1].GetComponent<AI_L2>().enabled = false;
            //  Time.timeScale = 0;
            gameObject.SetActive(true);
            if (s == "F")
            {
               // StartCoroutine(FullSide());
                // full.SetActive(true);
            }
            else if (s == "RL")
            {
                StartCoroutine(_RightLeft());
                //RightLeft.SetActive(true);
            }
            else if (s == "M")
            {
                middle.SetActive(true);
            }

            PlayerPrefs.SetInt("leve2", 1);
        }
        
    }

    public void Tutorl_L3(string s)
    {
      //  Time.timeScale = 0;
        

        if (PlayerPrefs.GetInt("leve3") == 0)
        {
            _swipe.SetActive(false);
            gameObject.SetActive(true);
            aIBot[2].GetComponent<AI_L3>().enabled = false;
            if (s == "F")
            {
                StartCoroutine(FullSide());
                // full.SetActive(true);
            }
            else if (s == "RL")
            {
               // StartCoroutine(_RightLeft());
                // RightLeft.SetActive(true);
            }
            else if (s == "M")
            {
               // middle.SetActive(true);
            }

            PlayerPrefs.SetInt("leve3", 1);
        }
        
    }

    public void Tutorl_L4(string s)
    {
       // Time.timeScale = 0;
        

        if (PlayerPrefs.GetInt("leve4") == 0)
        {
            _swipe.SetActive(false);
            gameObject.SetActive(true);
            aIBot[3].GetComponent<AI_L4>().enabled = false;
            if (s == "F")
            {
               // StartCoroutine(FullSide());
                //full.SetActive(true);
            }
            else if (s == "RL")
            {
               // StartCoroutine(_RightLeft());
                // RightLeft.SetActive(true);
            }
            else if (s == "M")
            {
                StartCoroutine(Middle());
            }

            PlayerPrefs.SetInt("leve4", 1);
        }
        
    }

    public void Tutorl_L5(string s)
    {
       // Time.timeScale = 0;
       

        if (PlayerPrefs.GetInt("leve5") == 0)
        {
            _swipe.SetActive(false);
            gameObject.SetActive(true);
            aIBot[4].GetComponent<AI_L5>().enabled = false;
            if (s == "F")
            {
               // StartCoroutine(FullSide());
               // full.SetActive(true);
            }
            else if (s == "RL")
            {
               // StartCoroutine(_RightLeft());
                //RightLeft.SetActive(true);
            }
            else if (s == "M")
            {
                StartCoroutine(Middle());
            }

            PlayerPrefs.SetInt("leve5", 1);
        }
        
    }

    public void Exit()
    {
      //  Time.timeScale = 1;
        full.SetActive(false);
        RightLeft.SetActive(false);
        middle.SetActive(false);

        gameObject.SetActive(false);

    }


    IEnumerator FullSide()
    {
       
        full.SetActive(true);
        yield return new WaitForSeconds(2.1f);
        Debug.Log("Tutorial 2 ");
        //full.transform.GetChild(0).gameObject.SetActive(true);
        fR.SetActive(true);
        yield return new WaitForSeconds(3.6f);
        // full.transform.GetChild(1).gameObject.SetActive(true);
        fM.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        _Tutorial.SetActive(false);
        aIBot[0].GetComponent<AI_Bot>().enabled = true;
        aIBot[2].GetComponent<AI_L3>().enabled = true;
        _swipe.SetActive(true);
    }

    IEnumerator _RightLeft()
    {
        RightLeft.SetActive(true);
        yield return new WaitForSeconds(2.1f);
        RightLeft.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        _Tutorial.SetActive(false);
        aIBot[1].GetComponent<AI_L2>().enabled = true;
        aIBot[1].GetComponent<AI_L2>().grounded = true;
        _swipe.SetActive(true);
        GameManager.inst._camera.GetComponent<CameraFollowScripts>().StartCamera();
    }

    IEnumerator Middle()
    {
        middle.SetActive(true);
        yield return new WaitForSeconds(2f);
        _Tutorial.SetActive(false);
        aIBot[3].GetComponent<AI_L4>().enabled = true;
        aIBot[3].GetComponent<AI_L4>().grounded = true;
        aIBot[4].GetComponent<AI_L5>().enabled = true;
        aIBot[4].GetComponent<AI_L5>().grounded = true;
        _swipe.SetActive(true);
    }
}
