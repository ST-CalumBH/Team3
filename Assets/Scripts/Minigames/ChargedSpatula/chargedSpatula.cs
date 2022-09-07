using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Combat;

namespace ChargedSpatula {
    public class chargedSpatula : Minigame
{
    public float speedSpatula;
    public float speedPlayer;

    private string minigameStatus; //{ inProgress, win, lose }

    private float eulerZ;
    [SerializeField] private float winSpeed;

    [SerializeField] private GameObject enemy;
    [SerializeField] private csEnemy controller;

    [SerializeField] private float chargePoint;

    [SerializeField] private AudioClip whack;

    AudioSource AS;

    public Animator animator;

    public Slider powerBar;

    void Awake()
    {
        minigameStatus = "inProgress";
        enemy = GameObject.Find("enemy");
        controller = enemy.GetComponent<csEnemy>();
        AS = GetComponent<AudioSource>();
    }

    void Update()
    {
        switch (minigameStatus)
        {
            case "inProgress":
                SpatulaMovement(); 
                break;
            default:
                break;
        }

        powerBar.value = (eulerZ / chargePoint); // (0 - 1) scaling
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

        if (eulerZ >= chargePoint)
        {
            AS.PlayOneShot(whack);
            minigameStatus = "win";
        }

        animator.SetFloat("ChargeLevel", (eulerZ / chargePoint)); // scales our eulerZ from (0 - chargePoint) into (0 - 1) so our animator can read it.
    }

    void SpatulaMovement()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.Rotate(Vector3.forward * speedPlayer * Time.fixedDeltaTime);
        }
    }

    void win() // once we have our swatting animation, we can delete just the if statement and leave just the top two lines
    {
        controller.defeatedStateChange();
        animator.SetBool("FullyCharged", true);

        if (winSpeed > 0)
        {
            transform.Rotate(Vector3.back * winSpeed * Time.fixedDeltaTime);
            winSpeed -= 50f;
        }
        else
        {
            StartCoroutine(EndMinigame(true));
        }
    }

    void lose()
    {

    }
}
}