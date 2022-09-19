using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CarLapCounter : MonoBehaviour
{

    public Text carPosText;

    int passedCPNo = 0;
    float timeAtLastPassedCP = 0;

    int numberOfPastCheckpoints = 0;

    int lapsCompleted = 0;
    const int lapsToComplete = 2;

    bool isRaceFinished = false;

    int carPos = 0;


    public event Action<CarLapCounter> OnPassCheckpoint;

    public void SetCarPostion(int position)
    {
        carPos = position;

    }

    public int GetNumberofCheckPointsPassed()
    {
        return passedCPNo;
    }

    public float GetTimeAtLastCheckPoint()
    {
        return timeAtLastPassedCP;
    }

    IEnumerator ShowPositionCO(float delayUntilHidePosition)
    {
        carPosText.text = carPos.ToString();

        carPosText.gameObject.SetActive(true);

        yield return new WaitForSeconds(delayUntilHidePosition);

        carPosText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.CompareTag("Checkpoint"))
        {
            if (isRaceFinished)
                return;

            Checkpoint checkpoint = collider2D.GetComponent<Checkpoint>();
            // car is going through correct checkpoints
        if (passedCPNo + 1 == checkpoint.CheckPointNo)
            {
                passedCPNo = checkpoint.CheckPointNo;

                numberOfPastCheckpoints++;

                timeAtLastPassedCP = Time.time;
                if (checkpoint.isFinishLine)
                {
                    passedCPNo = 0;
                    lapsCompleted++;

                    if(lapsCompleted >= lapsToComplete)
                    
                        isRaceFinished = true;
                }

                //invoke event
                OnPassCheckpoint?.Invoke(this);

                if (isRaceFinished)
                    StartCoroutine(ShowPositionCO(100));
                else StartCoroutine(ShowPositionCO(1.5f));
            }
        }
    }

}
