using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iconE : MonoBehaviour
{

    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
