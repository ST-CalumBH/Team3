using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emptyMinigame : Minigame
{
    bool end;
    void Start()
    {
        Debug.Log("empty Start");
        end = true;
    }

    IEnumerator endGame()
    {
        StartCoroutine(EndMinigame(0.1f, true));
        yield return null;
    }
    // Update is called once per frame
    void Update()
    {
        if(end)
        {
            end = false;
            StartCoroutine(endGame());
        }
    }
}
