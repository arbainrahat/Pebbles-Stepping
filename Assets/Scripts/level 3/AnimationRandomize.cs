using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRandomize : MonoBehaviour
{
    public Animator[] animators;

    private void OnEnable()
    {
        for (int i = 0; i < animators.Length; i++)
        {
            float rand = Random.Range(0f, 4f);
            StartCoroutine(Anim(rand, i));
        }
    }

    IEnumerator Anim(float t,int index)
    {
        yield return new WaitForSeconds(t);
        animators[index].enabled = true;

    }
}
