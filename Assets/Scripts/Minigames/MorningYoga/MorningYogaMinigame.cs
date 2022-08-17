using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorningYogaMinigame : Minigame
{
    // Start is called before the first frame update
    public RectTransform A;
    public RectTransform D;
    public RectTransform E;
    public RectTransform G;
    public RectTransform L;
    public RectTransform P;
    public RectTransform Q;
    public RectTransform R;
    public RectTransform W;
    public RectTransform Z;

    private enum GameStates { A, B, C, D }
    GameStates curState;

    bool tilesMoved;
    

    readonly float[] keyLocations = { -500f, -250f, 0f, 250f, 500f };
    readonly KeyCode[] stateA = { KeyCode.E, KeyCode.W };
    readonly KeyCode[] stateB = { KeyCode.A, KeyCode.D, KeyCode.G };
    readonly KeyCode[] stateC = { KeyCode.Q, KeyCode.A, KeyCode.Z, KeyCode.R };
    readonly KeyCode[] stateD = { KeyCode.P, KeyCode.A, KeyCode.L, KeyCode.Z, KeyCode.G };

    bool[] stateAResult = { false, false};
    bool[] stateBResult = { false, false, false };
    bool[] stateCResult = { false, false, false, false };
    bool[] stateDResult = { false, false, false, false, false };

    void Start()
    {
        tilesMoved = false;
        curState = GameStates.A;
        ResetLetters();
    }

    // Update is called once per frame
    void Update()
    {
        if (curState == GameStates.A)
        {
            if (Input.anyKey && !Input.GetKey(stateA[0]) && !Input.GetKey(stateA[1]))
            {
                //Debug.Log("Wrong Input");
                stateAResult[0] = false;
                stateAResult[1] = false;
            }
            else
            {
                if (Input.GetKey(stateA[0]))
                {
                    stateAResult[0] = true;
                }
                if (Input.GetKey(stateA[1]))
                {
                    stateAResult[1] = true;
                }
                else
                {
                    stateAResult[0] = false;
                    stateAResult[1] = false;
                }
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
                    
                    Debug.Log("Success");
                    i = 0;
                    curState = GameStates.B;
                    tilesMoved = false;
                }
            }
        }
        else if (curState == GameStates.B)
        {
            if (Input.anyKey && !Input.GetKey(stateB[0]) && !Input.GetKey(stateB[1]) && !Input.GetKey(stateB[2]))
            {
                stateBResult[0] = false;
                stateBResult[1] = false;
                stateBResult[2] = false;
            }
            else
            {
                if (Input.GetKey(stateB[0]))
                {
                    stateBResult[0] = true;
                }
                if (Input.GetKey(stateB[1]))
                {
                    stateBResult[1] = true;
                }
                if (Input.GetKey(stateB[2]))
                {
                    stateBResult[2] = true;
                }
                else
                {
                    stateBResult[0] = false;
                    stateBResult[1] = false;
                    stateBResult[2] = false;
                }
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

                    Debug.Log("Success");
                    i = 0;
                    curState = GameStates.C;
                    tilesMoved = false;
                }
            }
        }
        else if (curState == GameStates.C)
        {
            if (Input.anyKey && !Input.GetKey(stateC[0]) && !Input.GetKey(stateC[1]) && !Input.GetKey(stateC[2]) && !Input.GetKey(stateC[3]))
            {
                stateCResult[0] = false;
                stateCResult[1] = false;
                stateCResult[2] = false;
                stateCResult[3] = false;
            }
            else
            {
                if (Input.GetKey(stateC[0]))
                {
                    stateCResult[0] = true;
                }
                if (Input.GetKey(stateC[1]))
                {
                    stateCResult[1] = true;
                }
                if (Input.GetKey(stateC[2]))
                {
                    stateCResult[2] = true;
                }
                if (Input.GetKey(stateC[3]))
                {
                    stateCResult[3] = true;
                }
                else
                {
                    stateCResult[0] = false;
                    stateCResult[1] = false;
                    stateCResult[2] = false;
                    stateCResult[3] = false;
                }
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

                    Debug.Log("Success");
                    i = 0;
                    curState = GameStates.D;
                    tilesMoved = false;
                }
            }
        }
        else if (curState == GameStates.D)
        {
            if (Input.anyKey && !Input.GetKey(stateD[0]) && !Input.GetKey(stateD[1]) && !Input.GetKey(stateD[2]) && !Input.GetKey(stateD[3]) && !Input.GetKey(stateD[4]))
            {
                stateDResult[0] = false;
                stateDResult[1] = false;
                stateDResult[2] = false;
                stateDResult[3] = false;
                stateDResult[4] = false;
            }
            else
            {
                if (Input.GetKey(stateD[0]))
                {
                    stateDResult[0] = true;
                }
                if (Input.GetKey(stateD[1]))
                {
                    stateDResult[1] = true;
                }
                if (Input.GetKey(stateD[2]))
                {
                    stateDResult[2] = true;
                }
                if (Input.GetKey(stateD[3]))
                {
                    stateDResult[3] = true;
                }
                if (Input.GetKey(stateD[4]))
                {
                    stateDResult[4] = true;
                }
                else
                {
                    stateDResult[0] = false;
                    stateDResult[1] = false;
                    stateDResult[2] = false;
                    stateDResult[3] = false;
                    stateDResult[4] = false;
                }
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
                    StartCoroutine(EndMinigame(true));
                }
            }
        }
    }

    private void OnGUI()
    {
        
    }

    private void FixedUpdate()
    {
        if(curState == GameStates.A && tilesMoved == false)
        {
            ResetLetters();
            E.gameObject.SetActive(true);
            W.gameObject.SetActive(true);
            E.anchoredPosition = new Vector2(keyLocations[0], 375f);
            W.anchoredPosition = new Vector2(keyLocations[1], 375f);
            tilesMoved = true;
        }
        else if (curState == GameStates.B && tilesMoved == false)
        {
            ResetLetters();
            A.gameObject.SetActive(true);
            D.gameObject.SetActive(true);
            G.gameObject.SetActive(true);
            A.anchoredPosition = new Vector2(keyLocations[0], 375f);
            D.anchoredPosition = new Vector2(keyLocations[1], 375f);
            G.anchoredPosition = new Vector2(keyLocations[2], 375f);
            tilesMoved = true;
        }
        else if (curState == GameStates.C && tilesMoved == false)
        {
            ResetLetters();
            Q.gameObject.SetActive(true);
            A.gameObject.SetActive(true);
            Z.gameObject.SetActive(true);
            R.gameObject.SetActive(true);
            Q.anchoredPosition = new Vector2(keyLocations[0], 375f);
            A.anchoredPosition = new Vector2(keyLocations[1], 375f);
            Z.anchoredPosition = new Vector2(keyLocations[2], 375f);
            R.anchoredPosition = new Vector2(keyLocations[3], 375f);
            tilesMoved = true;
        }
        else if (curState == GameStates.D && tilesMoved == false)
        {
            ResetLetters();
            P.gameObject.SetActive(true);
            A.gameObject.SetActive(true);
            L.gameObject.SetActive(true);
            Z.gameObject.SetActive(true);
            G.gameObject.SetActive(true);
            P.anchoredPosition = new Vector2(keyLocations[0], 375f);
            A.anchoredPosition = new Vector2(keyLocations[1], 375f);
            L.anchoredPosition = new Vector2(keyLocations[2], 375f);
            Z.anchoredPosition = new Vector2(keyLocations[3], 375f);
            G.anchoredPosition = new Vector2(keyLocations[4], 375f);
            tilesMoved = true;
        }
    }

    void ResetLetters()
    {
        A.gameObject.SetActive(false);
        D.gameObject.SetActive(false);
        E.gameObject.SetActive(false);
        G.gameObject.SetActive(false);
        L.gameObject.SetActive(false);
        P.gameObject.SetActive(false);
        Q.gameObject.SetActive(false);
        R.gameObject.SetActive(false);
        W.gameObject.SetActive(false);
        Z.gameObject.SetActive(false);
    }
}
