using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minigameManager : Minigame
{
    public int lives = 3;
    public int enemyKilled = 0;

    [SerializeField] private spawnerManager spawnManager;

    void Update()
    {
        if (lives == 0)
        {
            spawnManager.changeState();
            StartCoroutine(EndMinigame(2f, false));
        }
        else if (enemyKilled == 10)
        {
            spawnManager.changeState();
            StartCoroutine(EndMinigame(2f, true));
        }
    }

    public void loseLife()
    {
        if (spawnManager.checkState()) { lives--; }         // if minigame is active, lose a life
    }

    public void gainPoint()
    {
        if (spawnManager.checkState()) { enemyKilled++; }         // if minigame is active, lose a life
    }
}