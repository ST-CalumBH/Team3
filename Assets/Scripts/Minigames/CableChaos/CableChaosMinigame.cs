using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Combat;
using UnityEngine.Analytics;
using Unity.Services.Analytics;

namespace CableChaos {
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
        public GameObject lifeObject;
        public float roundLength;
        public GameObject UI;
        public GameObject Tutorial;
        public float missCooldownLength;//Higher is shorter cooldown

        Image a1Image;
        Image a2Image;
        Image s1Image;
        Image d1Image;
        Image d2Image;   
        RectTransform timingBarRect;
        float timingBarWidth;
        float selectPos = 0;
        float margin;
        float moveSpeed = 0.1f;
        float cooldownLength;
        float cooldownTimer = 0;

        bool tilesMoved;
        bool paused = false;
        bool cooldown = false;

        Color redColour = Color.red;
        Color whiteColour = Color.white;
        float[] threeInputLoc = {375f,750f,1125f};
        float[] fourInputLoc = {300f,600f,900f,1200f};
        bool[] results = {false, false, false, false};
        bool spareLife;
        MinigameSFX mSFX;


        private enum GameStates {A,B,C}
        GameStates curState;
        
        // Start is called before the first frame update
        void Start()
        {
            AnalyticsService.Instance.CustomData("CableChaos", new Dictionary<string, object>());
            paused = true;
            mSFX = GetComponent<MinigameSFX>();
            spareLife = true;
            moveSpeed = ((1500 / roundLength) / 50);
            a1.gameObject.SetActive(false);
            a2.gameObject.SetActive(false);
            s1.gameObject.SetActive(false);
            d1.gameObject.SetActive(false);
            d2.gameObject.SetActive(false);
            a1Image = a1.GetComponent<Image>();
            a2Image = a2.GetComponent<Image>();
            s1Image = s1.GetComponent<Image>();
            d1Image = d1.GetComponent<Image>();
            d2Image = d2.GetComponent<Image>();
            timingBarRect = timingBar.GetComponent<RectTransform>();
            timingBarWidth = timingBarRect.rect.width;
            timingBar.maxValue = timingBarWidth;
            tilesMoved = false;
            curState = GameStates.A;
            margin = a1.rect.width / 2 + (a1.rect.width/20);
            animControllerPrinter.Play("Idle");
            animControllerKeith.Play("Idle");
            cooldownLength = roundLength/missCooldownLength;
            StartCoroutine(StartScreen());
        }

        // Update is called once per frame
        void Update()
        {
            switch (curState)
            {
                case GameStates.A:
                    if (cooldown == false)
                    {
                        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                            if (selectPos > threeInputLoc[0] - margin && selectPos < threeInputLoc[0] + margin)
                            {
                                results[0] = true;
                                animControllerKeith.Play("Key Pressed");
                                a1Image.color = redColour;
                                mSFX.PlaySound(0);
                            }
                            else if (selectPos > threeInputLoc[1] - margin && selectPos < threeInputLoc[1] + margin)
                            {
                                results[1] = true;
                                animControllerKeith.Play("Key Pressed");
                                a2Image.color = redColour;
                                mSFX.PlaySound(0);
                            }
                            else { cooldown = true; cooldownTimer = 0; }
                        }
                        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                        {
                            if (selectPos > threeInputLoc[2] - margin && selectPos < threeInputLoc[2] + margin)
                            {
                                results[2] = true;
                                results[3] = true;
                                d1Image.color = redColour;
                                mSFX.PlaySound(0);
                                mSFX.PlaySound(1);
                                animControllerKeith.Play("Attack");
                                animControllerPrinter.Play("Attacked");
                                StartCoroutine(StateTransition());
                            }
                            else { cooldown = true; cooldownTimer = 0; }
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
                        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                        {
                            if (selectPos > threeInputLoc[0] - margin && selectPos < threeInputLoc[0] + margin)
                            {
                                results[0] = true;
                                animControllerKeith.Play("Key Pressed");
                                s1Image.color = redColour;
                                mSFX.PlaySound(0);
                            }
                            else { cooldown = true; }
                        }
                        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                            if (selectPos > threeInputLoc[1] - margin && selectPos < threeInputLoc[1] + margin)
                            {
                                results[1] = true;
                                animControllerKeith.Play("Key Pressed");
                                a1Image.color = redColour;
                                mSFX.PlaySound(0);
                            }
                            else { cooldown = true; cooldownTimer = 0; }
                        }
                        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                        {
                            if (selectPos > threeInputLoc[2] - margin && selectPos < threeInputLoc[2] + margin)
                            {
                                results[2] = true;
                                results[3] = true;
                                animControllerKeith.Play("Attack");
                                animControllerPrinter.Play("Attacked");
                                d1Image.color = redColour;
                                mSFX.PlaySound(0);
                                mSFX.PlaySound(1);
                                StartCoroutine(StateTransition());
                            }
                            else { cooldown = true; cooldownTimer = 0; }
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
                        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                            if (selectPos > fourInputLoc[0] - margin && selectPos < fourInputLoc[0] + margin)
                            {
                                results[0] = true;
                                animControllerKeith.Play("Key Pressed");
                                a1Image.color = redColour;
                                mSFX.PlaySound(0);
                            }
                            else if (selectPos > fourInputLoc[2] - margin && selectPos < fourInputLoc[2] + margin)
                            {
                                results[2] = true;
                                animControllerKeith.Play("Key Pressed");
                                a2Image.color = redColour;
                                mSFX.PlaySound(0);
                            }
                            else { cooldown = true; cooldownTimer = 0; }
                        }
                        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                        {
                            if (selectPos > fourInputLoc[1] - margin && selectPos < fourInputLoc[1] + margin)
                            {
                                results[1] = true;
                                animControllerKeith.Play("Key Pressed");
                                d1Image.color = redColour;
                                mSFX.PlaySound(0);
                            }
                            else if (selectPos > fourInputLoc[3] - margin && selectPos < fourInputLoc[3] + margin)
                            {
                                results[3] = true;
                                animControllerKeith.Play("Attack");
                                animControllerPrinter.Play("Attacked");
                                d2Image.color = redColour;
                                mSFX.PlaySound(0);
                                mSFX.PlaySound(1);
                                StartCoroutine(StateTransition());
                            }
                            else { cooldown = true; cooldownTimer = 0; }
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
            ColourReset();
        }

        IEnumerator StartScreen()
        {
            yield return new WaitForSeconds(2f);
            Tutorial.SetActive(false);
            UI.SetActive(true);
            paused = false;
        }

        void ColourReset()
        {
            if(!Input.anyKey)
            {
                a1Image.color = whiteColour;
                a2Image.color = whiteColour;
                s1Image.color = whiteColour;
                d1Image.color = whiteColour;
                d2Image.color = whiteColour;
            }
        }

        IEnumerator MissedKey()
        {
            mSFX.PlaySound(2);
            paused = true;
            ResetResults();
            timingBar.value = selectPos;
            animControllerKeith.Play("Hit By Printer");
            if (spareLife == true)
            {
                spareLife = false;
            }
            else
            {
                StartCoroutine(EndMinigame(false));
                yield return null;
            }
            yield return new WaitForSeconds(1f);
            paused = false;
            yield return null;
        }

        private void FixedUpdate()
        {
            if (spareLife == false && lifeObject.activeInHierarchy)
            {
                lifeObject.SetActive(false);
            }
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
                if (cooldownTimer >= (cooldownLength*50f))
                { 
                    cooldown = false;
                    Debug.Log("Cooldown Over");
                }
                cooldownTimer += 1;
            }
            else {
                cooldownTimer = 0;
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
}