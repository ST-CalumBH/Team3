using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PopupAd {
    public class BossDistraction : MonoBehaviour
    {
        public int numToPacify;                                     // how many times does apce need to be pressed
        [SerializeField] private int counter = 0;

        [SerializeField] private ClickDetect clickDetect;           // connected so that it can disable the mouse's detection
        public Animator anim;

        [SerializeField] private Shake shaker;
        public float shakeFrequency;

        void Start()
        {
            clickDetect = GameObject.Find("gameManager").GetComponent<ClickDetect>();
            clickDetect.distractionChange(true);
        }

        void Update()
        {
            if (counter == numToPacify)
            {
                clickDetect.distractionChange(false);
                anim.Play("beckyLeave");
            }

            if (Input.GetKeyDown(KeyCode.Space) && counter < numToPacify)
            {
                StartCoroutine(shaker.CameraBake());
            }
        }
    }
}
