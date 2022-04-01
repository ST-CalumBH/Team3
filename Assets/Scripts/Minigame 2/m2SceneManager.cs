using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class m2SceneManager : MonoBehaviour
{

    public bool unscathed = true;
    public Canvas UI;
    private TextMeshPro timer;
    public float gameLength = 15f;
    // Start is called before the first frame update
    void Start()
    {
        timer = UI.GetComponent<TextMeshPro>();
        timer.text = gameLength.ToString();
        StartCoroutine(Countdown());
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(gameLength);
        if (unscathed == true)
        {
            //Win state
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
}
