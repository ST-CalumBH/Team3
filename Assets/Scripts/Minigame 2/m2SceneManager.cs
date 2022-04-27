using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class m2SceneManager : Minigame
{

    [SerializeField] private float gameLength = 15f;
    [SerializeField] private Text timer;
    [SerializeField] private GameObject win;
    [SerializeField] private GameObject loss;


    // Start is called before the first frame update
    void Start()
    {
        timer.text = gameLength.ToString();
        failed = false;
        StartCoroutine(Countdown());
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        if (failed == true)
        {
            loss.SetActive(true);
            failed = true;
            StartCoroutine(EndMinigame());
        }
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(gameLength);
        if (failed == false)
        {
            win.SetActive(true);
            StartCoroutine(EndMinigame());
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1f);
        gameLength -= 1;
        timer.text = gameLength.ToString();
        if (gameLength > 0)
        {
            StartCoroutine(Timer());
        }
    }

    //IEnumerator EndMinigame()
    //{
    //    yield return new WaitForSeconds(3f);
    //    Destroy(transform.parent.gameObject);
    //}
}
