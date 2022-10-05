using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VirusAttack;
using Combat;
using UnityEngine.Analytics;
using Unity.Services.Analytics;

namespace PopupAd {
    public class popupadManager : Minigame
    {
        [SerializeField] private int totalAdsClosed;

        [SerializeField] private spawnerManager spawnManager;
        [SerializeField] GameObject Tutorial;

        private bool updateOn = false;
        public bool paused = true;

        private void Start()
        {
            totalAdsClosed = 0;

            AnalyticsService.Instance.CustomData("PopupAd", new Dictionary<string, object>());
            StartCoroutine(StartScreen());
        }

        void Update()
        {
            if (!paused)
            {
                if (totalAdsClosed == spawnManager.getTarget() && updateOn == true)
                {
                    spawnManager.spawnerOff();
                    StartCoroutine(EndMinigame(2f, true));
                }
            }
        }

        public void AdClosed()
        {
            totalAdsClosed++;
            Debug.Log(spawnManager.getTarget() + ", " + totalAdsClosed);
        }

        IEnumerator StartScreen()
        {
            yield return new WaitForSeconds(2f);
            Tutorial.SetActive(false);
            paused = false;
            spawnerManager SM = FindObjectOfType<spawnerManager>();
            SM.paused = false;
            StartCoroutine(UpdatePause());
        }

        IEnumerator UpdatePause()
        {
            yield return new WaitForSeconds(1f);
            updateOn = true;
        }
    }
}