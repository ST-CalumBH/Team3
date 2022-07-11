using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animationtrigger : MonoBehaviour
{
    [SerializeField] private Animator myAnimationController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myAnimationController.SetBool("PopOut1", true);
        }
        else if (other.CompareTag("Player"))
        {
            myAnimationController.SetBool("slide", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myAnimationController.SetBool("PopOut1", false);
        }
        else if (other.CompareTag("Player"))
        {
            myAnimationController.SetBool("slide", false);
        }
    }
}
