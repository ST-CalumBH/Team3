using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CableChaosMinigame : Minigame
{
    public RectTransform a1;
    public RectTransform a2;
    public RectTransform s1;
    public RectTransform d1;
    public RectTransform d2;
    public Slider timingBar;

    RectTransform timingBarRect;
    float timingBarWidth;
    float[] threeInputLoc = {0.375f,0.625f,0.875f};
    float[] fourInputLoc = {0.3f,0.5f,0.7f,0.9f};
    enum gameStates {A,B,C}

    gameStates curState;
    // Start is called before the first frame update
    void Start()
    {
        a1.gameObject.SetActive(false);
        a2.gameObject.SetActive(false);
        s1.gameObject.SetActive(false);
        d1.gameObject.SetActive(false);
        d2.gameObject.SetActive(false);
        timingBarRect = timingBar.GetComponent<RectTransform>();
        timingBarWidth = timingBarRect.rect.width;
        timingBar.maxValue = timingBarWidth;
        curState = gameStates.A;
    }

    // Update is called once per frame
    void Update()
    {
        if(curState == gameStates.A)
        {
            Debug.Log("GameState A");
            a1.gameObject.SetActive(true);
            a2.gameObject.SetActive(true);
            s1.gameObject.SetActive(false);
            d1.gameObject.SetActive(true);
            d2.gameObject.SetActive(false);
            a1.anchoredPosition.Set(100, 100);
;        }
    }
}