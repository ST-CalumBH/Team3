using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeithHead : MonoBehaviour
{
    public Animator anim;
    private string headState;

    public string[] animationClips;

    private float headElaspedTime = 0f;
    public float changeFrequency;
    public float minTime;
    public float maxTime;


    void Start()
    {
        headState = "neutral";
        StartCoroutine(FaceChange());
    }

    void Update()
    {
        
    }

    IEnumerator FaceChange()
    {
        while (headState == "neutral")
        {
            headElaspedTime += Time.deltaTime;

            if (headElaspedTime >= changeFrequency)
            {
                headElaspedTime = 0f;
                changeFrequency = Random.Range(minTime, maxTime);

                anim.Play(animationClips[Random.Range(0, animationClips.Length)]);
            }

            yield return null;
        }
    }

    public void lose()
    {
        headState = "lose";
        anim.Play("loseKeith");
    }

    public void win()
    {
        headState = "win";
        anim.Play("reliefKeith");
    }
}
