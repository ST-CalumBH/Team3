using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iconE : MonoBehaviour
{
    private bool canChange = true;

    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

    public void Show()
    {
        if (canChange) { gameObject.SetActive(true); }
    }

    public void Hide()
    {
        if (canChange) { gameObject.SetActive(false); }
    }

    public void Disable()
    {
        canChange = false;
    }

    public void Enable()
    {
        canChange = true;
    }

}
