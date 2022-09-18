using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CarLapCounter : MonoBehaviour
{
    int passedCPNo = 0;
    float timeAtLastPassedCP = 0;

    int numberOfPastCheckpoints = 0;

    public event Action<CarLapCounter> OnPassCheckpoint;

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.CompareTag("Checkpoint"))
        {
            Checkpoint checkpoint = collider2D.GetComponent<Checkpoint>();
            // car is going through correct checkpoints
        if (passedCPNo + 1 == checkpoint.CheckPointNo)
            {
                passedCPNo = checkpoint.CheckPointNo;

                numberOfPastCheckpoints++;

                timeAtLastPassedCP = Time.time;

                //invoke event
                OnPassCheckpoint?.Invoke(this);
            }
        }
    }

}
