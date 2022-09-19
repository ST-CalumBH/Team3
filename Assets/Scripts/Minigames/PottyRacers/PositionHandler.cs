using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class PositionHandler : MonoBehaviour
{
    public List<CarLapCounter> carLapCounters = new List<CarLapCounter>();

    void Start()
    {
        CarLapCounter[] carLapCounterArray = FindObjectsOfType<CarLapCounter>();

        carLapCounters = carLapCounterArray.ToList<CarLapCounter>();

        foreach (CarLapCounter lapCounter in carLapCounters)
            lapCounter.OnPassCheckpoint += OnPassCheckpoint;

    }

    void OnPassCheckpoint(CarLapCounter carLapCounter)
    {
        //display car position number
        carLapCounters = carLapCounters.OrderByDescending(s => s.GetNumberofCheckPointsPassed()).ThenBy(s => s.GetTimeAtLastCheckPoint()).ToList();

        int carPos = carLapCounters.IndexOf(carLapCounter) + 1;

        carLapCounter.SetCarPostion(carPos);
    }
}
