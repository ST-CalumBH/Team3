using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundAnimController : MonoBehaviour
{
    public GameObject BackgroundObject;
    Animator Animator;

    // Start is called before the first frame update
    void Start()
    {
        BackgroundObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Animator.Play("Disco");
        }
    }
}
