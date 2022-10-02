using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOnKeyPress : MonoBehaviour
{
    public bool space;
    public bool WASDArrow;
    public bool ADLeftRight;
    public bool mouseClick;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && space)
        {
            Destroy(gameObject);
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) 
            || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) 
            || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow)) && WASDArrow)
        {
            Destroy(gameObject);
        }

        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow)
            || Input.GetKeyDown(KeyCode.RightArrow)) && ADLeftRight)
        {
            Destroy(gameObject);
        }

        if (Input.GetMouseButtonDown(0) && mouseClick)
        {
            Destroy(gameObject);
        }
    }
}
