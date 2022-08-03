using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdDisappear : MonoBehaviour
{

    public void DestroySelf()
    {
        Destroy(gameObject);
        Debug.Log("Destroyed");
    }
}
