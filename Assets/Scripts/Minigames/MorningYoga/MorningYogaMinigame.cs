using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Overworld;
using UnityEngine.Analytics;
using Unity.Services.Analytics;
using System;

public class MorningYogaMinigame : Minigame
{
    private playerController controller;

    public RectTransform A;
    public RectTransform D;
    public RectTransform E;
    public RectTransform G;
    public RectTransform H;
    public RectTransform L;
    public RectTransform M;
    public RectTransform P;
    public RectTransform Q;
    public RectTransform R;
    public RectTransform T;
    public RectTransform W;
    public RectTransform Z;

    public Sprite aPressed;
    public Sprite dPressed;
    public Sprite ePressed;
    public Sprite gPressed;
    public Sprite hPressed;
    public Sprite lPressed;
    public Sprite mPressed;
    public Sprite pPressed;
    public Sprite qPressed;
    public Sprite rPressed;
    public Sprite tPressed;
    public Sprite wPressed;
    public Sprite zPressed;

    public Animator backgroundAnim;
    public Animator keithAnim;
    public Slider progressBar;
    public GameObject Tutorial;
    public GameObject UI;

    public float letterHeight = 375f;

    private enum GameStates { A, B, C, D }
    GameStates curState;

    bool tilesMoved;
    bool recInput;

    readonly float[] keyLocations = { -500f, -250f, 0f, 250f, 500f };
    readonly KeyCode[] stateA = { KeyCode.E, KeyCode.W };
    readonly KeyCode[] stateB = { KeyCode.A, KeyCode.D, KeyCode.G };
    readonly KeyCode[] stateC = { KeyCode.Q, KeyCode.A, KeyCode.Z, KeyCode.T };
    readonly KeyCode[] stateD = { KeyCode.P, KeyCode.A, KeyCode.M, KeyCode.H };

    bool[] stateAResult = { false, false };
    bool[] stateBResult = { false, false, false };
    bool[] stateCResult = { false, false, false, false };
    bool[] stateDResult = { false, false, false, false };

    bool paused;

    MinigameSFX mSFX;
    Image aImage;
    Image dImage;
    Image eImage;
    Image gImage;
    Image hImage;
    Image lImage;
    Image mImage;
    Image pImage;
    Image qImage;
    Image rImage;
    Image tImage;
    Image wImage;
    Image zImage;

    void Start()
    {
        AnalyticsService.Instance.CustomData("MorningYoga", new Dictionary<string, object>());
        try
        {
            controller = GameObject.Find("Player").GetComponent<playerController>();
            controller.freezePlayer();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

        aImage = A.GetComponent<Image>();
        dImage = D.GetComponent<Image>();
        eImage = E.GetComponent<Image>();
        gImage = G.GetComponent<Image>();
        hImage = H.GetComponent<Image>();
        lImage = L.GetComponent<Image>();
        mImage = M.GetComponent<Image>();
        pImage = P.GetComponent<Image>();
        qImage = Q.GetComponent<Image>();
        rImage = R.GetComponent<Image>();
        tImage = T.GetComponent<Image>();
        wImage = W.GetComponent<Image>();
        zImage = Z.GetComponent<Image>();
        mSFX = GetComponent<MinigameSFX>();
        recInput = true;
        tilesMoved = false;
        paused = true;
        curState = GameStates.A;
        keithAnim.Play("Idle");
        backgroundAnim.Play("Idle");
        ResetLetters();
        StartCoroutine(StartScreen());
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            if (Input.anyKeyDown)
            {
                int num = UnityEngine.Random.Range(2, 7);
                mSFX.PlaySound(num);
            }
            if (curState == GameStates.A && recInput)
            {
                if (Input.anyKey && !Input.GetKey(stateA[0]) && !Input.GetKey(stateA[1]))
                {
                    //Debug.Log("Wrong Input");
                    keithAnim.Play("Idle");
                    stateAResult[0] = false;
                    stateAResult[1] = false;
                    ResetImage();
                }
                else if (Input.anyKey && (Input.GetKey(stateA[0]) | Input.GetKey(stateA[1])))
                {
                    if (Input.GetKey(stateA[0]))
                    {
                        stateAResult[0] = true;
                        if (Input.GetKey(stateA[1]))
                        {
                            keithAnim.Play("Two");
                            wImage.overrideSprite = wPressed;
                            stateAResult[1] = true;
                        }
                        else
                        {
                            keithAnim.Play("One");
                            eImage.overrideSprite = ePressed;
                        }
                    }
                }
                else
                {
                    keithAnim.Play("Idle");
                    stateAResult[0] = false;
                    stateAResult[1] = false;
                    ResetImage();
                }
                int i = 0;
                foreach (bool b in stateAResult)
                {
                    if (b == false)
                    {
                        break;
                    }
                    else
                    {
                        i++;
                    }
                    if (i >= stateAResult.Length)
                    {
                        recInput = false;
                        Debug.Log("Success");
                        i = 0;
                        BarUpdate(25f);
                        StartCoroutine(StateTransition());
                    }
                }
            }
            else if (curState == GameStates.B && recInput)
            {
                if (Input.anyKey && !Input.GetKey(stateB[0]) && !Input.GetKey(stateB[1]) && !Input.GetKey(stateB[2]))
                {
                    keithAnim.Play("Idle");
                    stateBResult[0] = false;
                    stateBResult[1] = false;
                    stateBResult[2] = false;
                    ResetImage();
                }
                else if (Input.anyKey && (Input.GetKey(stateB[0]) | Input.GetKey(stateB[1]) | Input.GetKey(stateB[2])))
                {
                    if (Input.GetKey(stateB[0]))
                    {
                        stateBResult[0] = true;
                        if (Input.GetKey(stateB[1]))
                        {
                            stateBResult[1] = true;

                            if (Input.GetKey(stateB[2]))
                            {
                                keithAnim.Play("Three");
                                gImage.overrideSprite = gPressed;
                                stateBResult[2] = true;
                            }
                            else
                            {
                                keithAnim.Play("Two");
                                dImage.overrideSprite = dPressed;
                            }
                        }
                        else
                        {
                            keithAnim.Play("One");
                            aImage.overrideSprite = aPressed;
                        }
                    }
                }
                else
                {
                    keithAnim.Play("Idle");
                    stateBResult[0] = false;
                    stateBResult[1] = false;
                    stateBResult[2] = false;
                    ResetImage();
                }
                int i = 0;
                foreach (bool b in stateBResult)
                {
                    if (b == false)
                    {
                        break;
                    }
                    else
                    {
                        i++;
                    }
                    if (i >= stateBResult.Length)
                    {

                        recInput = false;
                        Debug.Log("Success");
                        i = 0;
                        BarUpdate(50f);
                        StartCoroutine(StateTransition());
                    }
                }
            }
            else if (curState == GameStates.C && recInput)
            {
                if (Input.anyKey && !Input.GetKey(stateC[0]) && !Input.GetKey(stateC[1]) && !Input.GetKey(stateC[2]) && !Input.GetKey(stateC[3]))
                {
                    keithAnim.Play("Idle");
                    stateCResult[0] = false;
                    stateCResult[1] = false;
                    stateCResult[2] = false;
                    stateCResult[3] = false;
                    ResetImage();
                }
                else if (Input.anyKey && (Input.GetKey(stateC[0]) | Input.GetKey(stateC[1]) | Input.GetKey(stateC[2]) | Input.GetKey(stateC[3])))
                {
                    if (Input.GetKey(stateC[0]))
                    {
                        stateCResult[0] = true;
                        if (Input.GetKey(stateC[1]))
                        {
                            stateCResult[1] = true;
                            if (Input.GetKey(stateC[2]))
                            {
                                stateCResult[2] = true;
                                if (Input.GetKey(stateC[3]))
                                {
                                    keithAnim.Play("Four");
                                    tImage.overrideSprite = tPressed;
                                    stateCResult[3] = true;
                                }
                                else
                                {
                                    keithAnim.Play("Three");
                                    zImage.overrideSprite = zPressed;
                                }
                            }
                            else
                            {
                                keithAnim.Play("Two");
                                aImage.overrideSprite = aPressed;
                            }
                        }
                        else
                        {
                            keithAnim.Play("One");
                            qImage.overrideSprite = qPressed;
                        }
                    }
                }
                else
                {
                    keithAnim.Play("Idle");
                    stateCResult[0] = false;
                    stateCResult[1] = false;
                    stateCResult[2] = false;
                    stateCResult[3] = false;
                    ResetImage();
                }
                int i = 0;
                foreach (bool b in stateCResult)
                {
                    if (b == false)
                    {
                        break;
                    }
                    else
                    {
                        i++;
                    }
                    if (i >= stateCResult.Length)
                    {

                        recInput = false;
                        Debug.Log("Success");
                        i = 0;
                        BarUpdate(75f);
                        StartCoroutine(StateTransition());
                    }
                }
            }
            else if (curState == GameStates.D && recInput)
            {
                if (Input.anyKey && !Input.GetKey(stateD[0]) && !Input.GetKey(stateD[1]) && !Input.GetKey(stateD[2]) && !Input.GetKey(stateD[3]))
                {
                    keithAnim.Play("Idle");
                    stateDResult[0] = false;
                    stateDResult[1] = false;
                    stateDResult[2] = false;
                    stateDResult[3] = false;
                    ResetImage();
                }
                else if (Input.anyKey && (Input.GetKey(stateD[0]) | Input.GetKey(stateD[1]) | Input.GetKey(stateD[2]) | Input.GetKey(stateD[3])))
                {
                    if (Input.GetKey(stateD[0]))
                    {
                        stateDResult[0] = true;
                        if (Input.GetKey(stateD[1]))
                        {
                            stateDResult[1] = true;
                            if (Input.GetKey(stateD[2]))
                            {
                                stateDResult[2] = true;
                                if (Input.GetKey(stateD[3]))
                                {
                                    stateDResult[3] = true;
                                    hImage.overrideSprite = hPressed;
                                    keithAnim.Play("Four");
                                }
                                else
                                {
                                    keithAnim.Play("Three");
                                    mImage.overrideSprite = mPressed;
                                }
                            }
                            else
                            {
                                keithAnim.Play("Two");
                                aImage.overrideSprite = aPressed;
                            }
                        }
                        else
                        {
                            keithAnim.Play("One");
                            pImage.overrideSprite = pPressed;
                        }
                    }
                }
                else
                {
                    keithAnim.Play("Idle");
                    stateDResult[0] = false;
                    stateDResult[1] = false;
                    stateDResult[2] = false;
                    stateDResult[3] = false;
                    ResetImage();
                }
                int i = 0;
                foreach (bool b in stateDResult)
                {
                    if (b == false)
                    {
                        break;
                    }
                    else
                    {
                        i++;
                    }
                    if (i >= stateDResult.Length)
                    {
                        recInput = false;
                        Debug.Log("Success");
                        i = 0;
                        BarUpdate(125f);
                        StartCoroutine(StateTransition());
                    }
                }
            }
            ImageReset();
        }
    }

    void BarUpdate(float progress)
    {
        progressBar.value = progress;
    }
    void ImageReset()
    {
        if (!Input.anyKey)
        {
            aImage.overrideSprite = null;
            dImage.overrideSprite = null;
            eImage.overrideSprite = null;
            gImage.overrideSprite = null;
            hImage.overrideSprite = null;
            lImage.overrideSprite = null;
            mImage.overrideSprite = null;
            pImage.overrideSprite = null;
            qImage.overrideSprite = null;
            rImage.overrideSprite = null;
            tImage.overrideSprite = null;
            wImage.overrideSprite = null;
            zImage.overrideSprite = null;
        }
    }
    IEnumerator StartScreen()
    {
        yield return new WaitForSeconds(2f);
        Tutorial.SetActive(false);
        UI.SetActive(true);
        paused = false;
    }
    IEnumerator StateTransition()
    {
        mSFX.PlaySound(1);
        ResetLetters();
        if (curState == GameStates.A)
        {
            keithAnim.Play("Two");
            yield return new WaitForSeconds(2f);
            curState = GameStates.B;
            tilesMoved = false;
            recInput = true;
        }
        else if (curState == GameStates.B)
        {
            keithAnim.Play("Three");
            yield return new WaitForSeconds(2f);
            curState = GameStates.C;
            tilesMoved = false;
            recInput = true;
        }
        else if (curState == GameStates.C)
        {
            keithAnim.Play("Four");
            yield return new WaitForSeconds(2f);
            curState = GameStates.D;
            tilesMoved = false;
            recInput = true;
        }
        else if (curState == GameStates.D)
        {
            keithAnim.Play("Four");
            yield return new WaitForSeconds(2f);
            Debug.Log("Won Minigame");
            try
            {
                StartCoroutine(DelayUnfreeze());
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        StartCoroutine(EndMinigame(true));
        }
    }

    void ResetImage()
    {
        aImage.overrideSprite = null;
        dImage.overrideSprite = null;
        eImage.overrideSprite = null;
        gImage.overrideSprite = null;
        hImage.overrideSprite = null;
        lImage.overrideSprite = null;
        mImage.overrideSprite = null;
        pImage.overrideSprite = null;
        qImage.overrideSprite = null;
        rImage.overrideSprite = null;
        tImage.overrideSprite = null;
        wImage.overrideSprite = null;
        zImage.overrideSprite = null;
    }

    private void FixedUpdate()
    {
        if (curState == GameStates.A && tilesMoved == false)
        {
            ResetLetters();
            E.gameObject.SetActive(true);
            W.gameObject.SetActive(true);
            E.anchoredPosition = new Vector2(keyLocations[0], letterHeight);
            W.anchoredPosition = new Vector2(keyLocations[1], letterHeight);
            tilesMoved = true;
        }
        else if (curState == GameStates.B && tilesMoved == false)
        {
            ResetLetters();
            A.gameObject.SetActive(true);
            D.gameObject.SetActive(true);
            G.gameObject.SetActive(true);
            A.anchoredPosition = new Vector2(keyLocations[0], letterHeight);
            D.anchoredPosition = new Vector2(keyLocations[1], letterHeight);
            G.anchoredPosition = new Vector2(keyLocations[2], letterHeight);
            tilesMoved = true;
        }
        else if (curState == GameStates.C && tilesMoved == false)
        {
            ResetLetters();
            Q.gameObject.SetActive(true);
            A.gameObject.SetActive(true);
            Z.gameObject.SetActive(true);
            T.gameObject.SetActive(true);
            Q.anchoredPosition = new Vector2(keyLocations[0], letterHeight);
            A.anchoredPosition = new Vector2(keyLocations[1], letterHeight);
            Z.anchoredPosition = new Vector2(keyLocations[2], letterHeight);
            T.anchoredPosition = new Vector2(keyLocations[3], letterHeight);
            tilesMoved = true;
        }
        else if (curState == GameStates.D && tilesMoved == false)
        {
            ResetLetters();
            P.gameObject.SetActive(true);
            A.gameObject.SetActive(true);
            M.gameObject.SetActive(true);
            H.gameObject.SetActive(true);
            P.anchoredPosition = new Vector2(keyLocations[0], letterHeight);
            A.anchoredPosition = new Vector2(keyLocations[1], letterHeight);
            M.anchoredPosition = new Vector2(keyLocations[2], letterHeight);
            H.anchoredPosition = new Vector2(keyLocations[3], letterHeight);
            tilesMoved = true;
        }
    }

    void ResetLetters()
    {
        
        A.gameObject.SetActive(false);
        D.gameObject.SetActive(false);
        E.gameObject.SetActive(false);
        G.gameObject.SetActive(false);
        H.gameObject.SetActive(false);
        L.gameObject.SetActive(false);
        P.gameObject.SetActive(false);
        M.gameObject.SetActive(false);
        Q.gameObject.SetActive(false);
        R.gameObject.SetActive(false);
        T.gameObject.SetActive(false);
        W.gameObject.SetActive(false);
        Z.gameObject.SetActive(false);
    }
    public IEnumerator DelayUnfreeze()
    {
        yield return new WaitForSeconds(1f); // since endMinigame(bool state) defaults to 1f
        controller.unfreezePlayer();
    }
}
