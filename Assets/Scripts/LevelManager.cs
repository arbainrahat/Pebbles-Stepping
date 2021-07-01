using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public UnityEvent OnStart;
    public UnityEvent OnDisabale;


    private void OnEnable()
    {
        OnStart.Invoke();
    }

   
    

    private void OnDisable()
    {
        OnDisabale.Invoke();
    }
}
