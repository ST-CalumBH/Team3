using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popupadManager : Minigame
{
    [SerializeField] private int CurrentAds = 0;

    [SerializeField] private spawnerManager spawnManager;


    void Start()
    {

    }

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
