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
    //float[] threeInputLoc = {0.375f,0.625f,0.875f};
    //float[] fourInputLoc = {0.3f,0.5f,0.7f,0.9f};

    private enum GameStates {A,B,C}

    GameStates curState;
    float selectPos = 0;

    public float roundLength;
    float moveSpeed = 0.1f;
    bool tilesMoved;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = ((1500 / roundLength) / 60);
        a1.gameObject.SetActive(false);
        a2.gameObject.SetActive(false);
        s1.gameObject.SetActive(false);
        d1.gameObject.SetActive(false);
        d2.gameObject.SetActive(false);
        timingBarRect = timingBar.GetComponent<RectTransform>();
        timingBarWidth = timingBarRect.rect.width;
        timingBar.maxValue = timingBarWidth;
        tilesMoved = false;
        curState = GameStates.A;
        StartCoroutine(Tick());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (curState == GameStates.A && tilesMoved == false)
        {
            Debug.Log("GameState A");
            a1.gameObject.SetActive(true);
            a2.gameObject.SetActive(true);
            s1.gameObject.SetActive(false);
            d1.gameObject.SetActive(true);
            d2.gameObject.SetActive(false);
            a1.anchoredPosition = new Vector2(375f, 100f);
            a2.anchoredPosition = new Vector2(750f, 100f);
            d1.anchoredPosition = new Vector2(1125f, 100f);
            tilesMoved = true;
        }
        if (curState == GameStates.B && tilesMoved == false)
        {
            Debug.Log("GameState B");
            a1.gameObject.SetActive(true);
            a2.gameObject.SetActive(false);
            s1.gameObject.SetActive(true);
            d1.gameObject.SetActive(true);
            d2.gameObject.SetActive(false);
            s1.anchoredPosition = new Vector2(375f, 100f);
            a1.anchoredPosition = new Vector2(750f, 100f);
            d1.anchoredPosition = new Vector2(1125f, 100f);
            tilesMoved = true;
        }
        if (curState == GameStates.C && tilesMoved == false)
        {
            Debug.Log("GameState C");
            a1.gameObject.SetActive(true);
            a2.gameObject.SetActive(true);
            s1.gameObject.SetActive(false);
            d1.gameObject.SetActive(true);
            d2.gameObject.SetActive(true);
            a1.anchoredPosition = new Vector2(300f, 100f);
            d1.anchoredPosition = new Vector2(600f, 100f);
            a2.anchoredPosition = new Vector2(900f, 100f);
            d2.anchoredPosition = new Vector2(1200f, 100f);
            tilesMoved = true;
        }
        selectPos += moveSpeed;
        timingBar.value = selectPos;
    }

    IEnumerator Tick()
    {
        Debug.Log("Tick");
        yield return new WaitForSeconds(1f);
        StartCoroutine(Tick());
    }
}