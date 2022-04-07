using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class m2SceneManager : MonoBehaviour
{

    public bool unscathed = true;
    [SerializeField] private float gameLength = 15f;
    [SerializeField] private Text timer;
    [SerializeField] private GameObject win;
    [SerializeField] private GameObject loss;


    // Start is called before the first frame update
    void Start()
    {
        timer.text = gameLength.ToString();
        StartCoroutine(Countdown());
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        if (unscathed == false)
        {
            loss.SetActive(true);
            StartCoroutine(EndMinigame());
        }
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(gameLength);
        if (unscathed == true)
        {
            win.SetActive(true);
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

    IEnumerator EndMinigame()
    {
        yield return new WaitForSeconds(3f);

    }
}
