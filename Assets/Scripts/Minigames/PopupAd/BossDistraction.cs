using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PopupAd {
    public class BossDistraction : MonoBehaviour
    {
        public int numToPacify;                                     // how many times does apce need to be pressed
        private int counter = 0;

        [SerializeField] private ClickDetect clickDetect;           // connected so that it can disable the mouse's detection
        public Animator anim;

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

            if (Input.GetKeyDown(KeyCode.Space))
            {
                counter++;

                CameraBake();
            }
        }

        private void CameraBake()
        {
            if (shakeFrequency > 0)
            {
                Vector3 originalPos = transform.position;

                float offsetX = Random.value * shakeFrequency * 2 - shakeFrequency;
                float offsetY = Random.value * shakeFrequency * 2 - shakeFrequency;
                originalPos.x += offsetX;
                originalPos.y += offsetY;

                transform.position = originalPos;
            }
        }
    }
}
