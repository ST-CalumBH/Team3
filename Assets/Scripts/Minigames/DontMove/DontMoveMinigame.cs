using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DontMove {
    public class DontMoveMinigame : Minigame
    {
        private string minigameState; // { inProgress, win, lose };

        [SerializeField] private Slider timingBar;
        public float maxTime = 10f;
        private float time = 0f;

        public KeithHead headController;
        public BossHead boss;

        void Start()
        {
            minigameState = "inProgress";
            timingBar.maxValue = maxTime;
        }

        void Update()
        {
            if (Time.timeScale == 0) return;

            if (minigameState == "inProgress") { detectAction(); }

            if (time >= maxTime && minigameState == "inProgress")
            {
                winSequence();
            }

            time += Time.deltaTime;
            timingBar.value = time;
        }

        private void detectAction()
        {
            if (Input.anyKey)
            {
                if (Input.GetKey(KeyCode.Escape)) { }
                else 
                {
                    Debug.Log("button pressed");
                    loseSequence();
                }
            }
        }

        private void winSequence()
        {
            minigameState = "win";
            headController.win();
            StartCoroutine(EndMinigame(3f, true));
        }

        private void loseSequence()
        {
            minigameState = "lose";
            headController.lose();
            StartCoroutine(boss.fadeOut());
            StartCoroutine(EndMinigame(3f, false));
        }
    }
}
