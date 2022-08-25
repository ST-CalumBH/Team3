using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Becky {
    public class Animationtrigger : MonoBehaviour
    {
        [SerializeField] private Animator myAnimationController;

        private BoxCollider boxCol;

        private void Start()
        {
            boxCol = gameObject.GetComponent<BoxCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            myAnimationController.Play("Popout 1");
            myAnimationController.Play("Popout 2");
            myAnimationController.Play("Popout 3");
            myAnimationController.Play("Popout 4");
            myAnimationController.Play("Popout 5");
            myAnimationController.Play("Popout 6");

            boxCol.enabled = false;


            //if (other.CompareTag("Player"))
            //{
            //    myAnimationController.SetBool("PopOut1", true);
            //}
            //else if (other.CompareTag("Player"))
            //{
            //    myAnimationController.SetBool("slide", true);
            //}
        }

        //private void OnTriggerExit(Collider other)
        //{
        //    if (other.CompareTag("Player"))
        //    {
        //        myAnimationController.SetBool("PopOut1", false);
        //    }
        //    else if (other.CompareTag("Player"))
        //    {
        //        myAnimationController.SetBool("slide", false);
        //    }
        //}
    }
} 
