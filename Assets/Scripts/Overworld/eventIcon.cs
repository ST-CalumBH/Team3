using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Overworld {
    public class eventIcon : MonoBehaviour
    {
        public Animator animator;

        public void changeActiveState(bool state)
        {
            gameObject.SetActive(state);
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                animator.SetBool("Dim",true);
            }
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                animator.SetBool("Dim", false);
            }
        }
    }
}