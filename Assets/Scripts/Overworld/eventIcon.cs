using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventIcon : MonoBehaviour
{

    private bool active; // 0 = floating anim, 1 = hide

    private bool floatUp;
    private int floatCounter;

    private Vector3 vectorUp = (Vector3.up / 100);
    private Vector3 vectorDown = (Vector3.down / 100);

    void Start()
    {
        active = true;
        floatUp = true;
        floatCounter = 0;
    }

    
    void FixedUpdate()
    {
        if (active)
        {
            floatingAnimation();
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

    public void changeActiveState()
    {
        active = !active;
        gameObject.SetActive(active);
    }
}
