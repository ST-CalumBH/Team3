using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDistraction : MonoBehaviour
{
    public int numToPacify;                                     // how many times does apce need to be pressed
    [SerializeField] private int counter = 0;

    [SerializeField] private ClickDetect clickDetect;

    void Start()
    {
        clickDetect = GameObject.Find("gameManager").GetComponent<ClickDetect>();
        clickDetect.distractionChange();
    }

    void Update()
    {
        if (counter == numToPacify)
        {
            clickDetect.distractionChange();
            gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            counter++;
        }
    }
}
