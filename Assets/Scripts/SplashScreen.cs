using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(WaitLoad());
        
    }

    IEnumerator WaitLoad()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Gameplay");
    }
}
