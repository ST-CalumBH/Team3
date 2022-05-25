using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundAnimController : MonoBehaviour
{
    public GameObject BackgroundObject;
    public string AnimName;
    Animator Animator;

    // Start is called before the first frame update
    void Start()
    {
        Animator = BackgroundObject.GetComponent<Animator>();
        
        Animator.StopPlayback();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Animator.Play("Base Layer."+AnimName);
        }
    }
}
