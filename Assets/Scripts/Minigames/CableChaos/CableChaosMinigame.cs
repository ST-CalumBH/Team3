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
    public Animator animControllerPrinter;
    public Animator animControllerKeith;
    public float roundLength;
    public float cooldownLength = 0.1f;

    RectTransform timingBarRect;
    float timingBarWidth;
    float selectPos = 0;
    float margin;
    float moveSpeed = 0.1f;

    bool tilesMoved;
    bool paused = false;
    bool cooldown = false;

    float[] threeInputLoc = {375f,750f,1125f};
    float[] fourInputLoc = {300f,600f,900f,1200f};
    bool[] results = {false, false, false, false};

    private enum GameStates {A,B,C}
    GameStates curState;
    
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
        margin = a1.rect.width / 2;
        paused = false;
        animControllerPrinter.Play("Idle");
        animControllerKeith.Play("Idle");
    }

    // Update is called once per frame
    void Update()
    {
        switch (curState)
        {
            case GameStates.A:
                if (cooldown == false)
                {
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        if (selectPos > threeInputLoc[0] - margin && selectPos < threeInputLoc[0] + margin)
                        {
                            results[0] = true;
                            animControllerKeith.Play("Key Pressed");
                        }
                        else if (selectPos > threeInputLoc[1] - margin && selectPos < threeInputLoc[1] + margin)
                        {
                            results[1] = true;
                            animControllerKeith.Play("Key Pressed");
                        }
                        else { cooldown = true; }
                    }
                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        if (selectPos > threeInputLoc[2] - margin && selectPos < threeInputLoc[2] + margin)
                        {
                            results[2] = true;
                            results[3] = true;
                            animControllerKeith.Play("Attack");
                            animControllerPrinter.Play("Attacked");
                            StartCoroutine(StateTransition());
                        }
                        else { cooldown = true; }
                    }
                }
                for (int i = 0; i < 3; i++)
                { 
                    if(selectPos > (threeInputLoc[i] + margin) && results[i] == false)
                    {
                        StartCoroutine(MissedKey());
                    }
                }
                break;
            case GameStates.B:
                if (cooldown == false)
                {
                    if (Input.GetKeyDown(KeyCode.S))
                    {
                        if (selectPos > threeInputLoc[0] - margin && selectPos < threeInputLoc[0] + margin)
                        {
                            results[0] = true;
                            animControllerKeith.Play("Key Pressed");
                        }
                        else { cooldown = true; }
                    }
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        if (selectPos > threeInputLoc[1] - margin && selectPos < threeInputLoc[1] + margin)
                        {
                            results[1] = true;
                            animControllerKeith.Play("Key Pressed");
                        }
                        else { cooldown = true; }
                    }
                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        if (selectPos > threeInputLoc[2] - margin && selectPos < threeInputLoc[2] + margin)
                        {
                            results[2] = true;
                            results[3] = true;
                            animControllerKeith.Play("Attack");
                            animControllerPrinter.Play("Attacked");
                            StartCoroutine(StateTransition());
                        }
                        else { cooldown = true; }
                    }
                    
                }
                for (int i = 0; i < 3; i++)
                {
                    if (selectPos > (threeInputLoc[i] + margin) && results[i] == false)
                    {
                        StartCoroutine(MissedKey());
                    }
                }
                break;
            case GameStates.C:
                if (cooldown == false)
                {
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        if (selectPos > fourInputLoc[0] - margin && selectPos < fourInputLoc[0] + margin)
                        {
                            results[0] = true;
                            animControllerKeith.Play("Key Pressed");
                        }
                        else if (selectPos > fourInputLoc[2] - margin && selectPos < fourInputLoc[2] + margin)
                        {
                            results[2] = true;
                            animControllerKeith.Play("Key Pressed");
                        }
                        else { cooldown = true; }
                    }
                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        if (selectPos > fourInputLoc[1] - margin && selectPos < fourInputLoc[1] + margin)
                        {
                            results[1] = true;
                            animControllerKeith.Play("Key Pressed");
                        }
                        else if (selectPos > fourInputLoc[3] - margin && selectPos < fourInputLoc[3] + margin)
                        {
                            results[3] = true;
                            animControllerKeith.Play("Attack");
                            animControllerPrinter.Play("Attacked");
                            StartCoroutine(StateTransition());
                        }
                        else { cooldown = true; }
                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    if (selectPos > (fourInputLoc[i] + margin) && results[i] == false)
                    {
                        StartCoroutine(MissedKey());
                    }
                }
                break;
        }
        
    }

    IEnumerator MissedKey()
    {
        
        paused = true;
        ResetResults();
        timingBar.value = selectPos;
        animControllerKeith.Play("Hit By Printer");
        yield return new WaitForSeconds(1f);
        Debug.Log("Missed Key Called");
        paused = false;
        
        //Missed key badness here
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
        if (paused == false)
        {
            selectPos += moveSpeed;
            timingBar.value = selectPos;
        }
        if (cooldown == true)
        {
            float timer = 0;
            if (timer >= (cooldownLength*60f))
            { 
                cooldown = false;
                Debug.Log("Cooldown Over");
            }
            timer += 1;
        }
    }
    void ResetResults()
    {
        selectPos = 0;
        results[0] = false;
        results[1] = false;
        results[2] = false;
        results[3] = false;
    }
    IEnumerator StateTransition()
    {
        yield return new WaitForSeconds(2f);
        ResetResults();
        if (curState == GameStates.A)
        {
            curState = GameStates.B;
            tilesMoved = false;
        }
        else if(curState == GameStates.B)
        {
            curState = GameStates.C;
            tilesMoved = false;
        }
        else if (curState == GameStates.C)
        {
            Debug.Log("Won Minigame");
            paused = true;
            StartCoroutine(EndMinigame(true));
        }
    }
}