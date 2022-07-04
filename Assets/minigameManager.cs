using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minigameManager : Minigame
{
    public int lives = 3;

    void Start()
    {
        
    }


    void Update()
    {
        if (lives == 0)
        {
            StartCoroutine(EndMinigame(2f, false));
        }
    }

    public void LoseLife()
    {
        lives--;
    }
}
