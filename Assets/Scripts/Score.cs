using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreTxt;
    public Image fillBar;
    public GameObject great;
    public GameObject prefect;
    public Text lvlfailScore;
    public Text lvlCompScore;

    public int score;
    float fillBarIncr_Value;
    float fillVal;

    private void Start()
    {
        great.SetActive(false);
        prefect.SetActive(false);
        scoreTxt.text = "0";
        score = 0;
    }

    private void Update()
    {
        scoreTxt.text = score.ToString(); 
    }

    public void SetFillBarIncrValue(float val)
    {
        fillBarIncr_Value = val;
        fillVal = 1 / fillBarIncr_Value;
    }

    public void FillBar()
    {
        fillBar.fillAmount += fillVal;
    }

    public void AffectsShow()
    {
        int i = Random.Range(0,2);

        if(i == 0)
        {
            StartCoroutine(Great());
        }
        else if(i == 1)
        {
            StartCoroutine(Prefect());
        }
    }

    IEnumerator Great()
    {
        great.SetActive(true);
        great.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(1.2f);
        great.SetActive(false);
        great.GetComponent<Animator>().enabled = false;
    }

    IEnumerator Prefect()
    {
        prefect.SetActive(true);
        prefect.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(1.2f);
        prefect.SetActive(false);
        prefect.GetComponent<Animator>().enabled = false;
    }

    public void SetScore()
    {
        lvlfailScore.text = score.ToString();
        lvlCompScore.text = score.ToString();
    }

    public void SetScore_FillBar()
    {
        scoreTxt.text = 0.ToString();
        fillBar.fillAmount = 0f;
    }
}
