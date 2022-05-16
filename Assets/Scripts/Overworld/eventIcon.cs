using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventIcon : MonoBehaviour
{

    private string state; // floating, eventActive (i.e single-use, stays invis)

    private bool floatUp;
    private int floatCounter;

    private Vector3 vectorUp = (Vector3.up / 100);
    private Vector3 vectorDown = (Vector3.down / 100);

    void Start()
    {
        state = "floating";
        floatUp = true;
        floatCounter = 0;
    }

    
    void FixedUpdate()
    {
        switch (state)
        {
            case "floating":
                floatingAnimation();
                break;
            case "eventActive":
                break;
        }
    }

    public void floatingAnimation()
    {
        if (floatUp)
        {
            transform.position += vectorUp;
            floatCounter++;

            if (floatCounter == 100)
            {
                floatUp = false;
                floatCounter = 0;
            }
        }
        else
        {
            transform.position += vectorDown;
            floatCounter++;

            if (floatCounter == 100)
            {
                floatUp = true;
                floatCounter = 0;
            }
        }
    }

    public void changeToActiveState()
    {
        state = "eventActive";
        gameObject.SetActive(false);
    }
}
