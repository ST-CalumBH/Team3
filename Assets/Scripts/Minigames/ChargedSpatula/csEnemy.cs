using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csEnemy : MonoBehaviour
{
    private bool defeated = false;
    private Vector3 direction;

    void Start()
    {
        direction = new Vector3(Random.Range(0.5f, 1f), Random.Range(0.5f, 1.5f), 0);
    }

    void Update()
    {
        if (defeated == true)
        {
            Spin();
        }
    }

    public void Spin()
    {
        transform.position += direction;
        transform.Rotate(Vector3.back * 1000f * Time.deltaTime);
    }

    public void defeatedStateChange()
    {
        defeated = true;
    }
}
