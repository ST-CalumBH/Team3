using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chargedSpatula : Minigame
{
    public float speedSpatula = 10f;
    public float speedPlayer = 100f;

    private string minigameStatus; //{ inProgress, win, lose }

    private float eulerZ;
    [SerializeField] private float winSpeed = 3000f;

    [SerializeField] private GameObject enemy;
    [SerializeField] private csEnemy controller;

    void Awake()
    {
        minigameStatus = "inProgress";
        enemy = GameObject.Find("enemy");
        controller = enemy.GetComponent<csEnemy>();
    }

    void Update()
    {
        switch (minigameStatus)
        {
            case "inProgress":
                SpatulaMovement(); 
                break;
            case "win": // win and lose movement is done in fixedUpdate() could delete them
                break;
            case "lose":
                break;
            default:
                break;
        }
    }

    void FixedUpdate()
    {
        switch (minigameStatus)
        {
            case "inProgress":
                inProgress();
                break;
            case "win":
                win();
                break;
            case "lose":
                break;
            default:
                break;
        }
    }

    void inProgress()
    {
        eulerZ = transform.rotation.z;

        if (eulerZ >= 0)
        {
            transform.Rotate(Vector3.back * speedSpatula * Time.fixedDeltaTime);
        }

        if (eulerZ >= 0.9f)
        {
            minigameStatus = "win";
        }
    }

    void SpatulaMovement()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.Rotate(Vector3.forward * speedPlayer * Time.deltaTime);
        }
    }

    void win()
    {
        controller.defeatedStateChange();

        if (winSpeed > 0)
        {
            transform.Rotate(Vector3.back * winSpeed * Time.fixedDeltaTime);
            winSpeed -= 50f;
        }
        else
        {
            StartCoroutine(EndMinigame());
        }
    }

    void lose()
    {

    }
}
