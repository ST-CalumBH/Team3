using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iconE : MonoBehaviour
{
    [SerializeField] private bool canChange = true;
    [SerializeField] private Animator anim;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        if (canChange) { gameObject.SetActive(true); anim.Play("eExpand");}
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
