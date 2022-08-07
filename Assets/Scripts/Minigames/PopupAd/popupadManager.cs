using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popUpAdManager : Minigame
{
    [SerializeField] private int CurrentAds = 0;

    [SerializeField] private spawnerManager spawnManager;

    void Update()
    {
        if (CurrentAds == 0)
        {
            spawnManager.changeState();
            StartCoroutine(EndMinigame(2f, true));
        }
    }

    public void AdAdded()
    {
        CurrentAds++;
    }

    public void AdDeleted()
    {
        CurrentAds--;
    }
}
