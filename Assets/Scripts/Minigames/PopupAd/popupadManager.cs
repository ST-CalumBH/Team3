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
        [SerializeField] private int CurrentAds = 0;

        [SerializeField] private spawnerManager spawnManager;
        [SerializeField] GameObject Tutorial;

        private bool updateOn = false;
        public bool paused = true;

        private void Start()
        {
            AnalyticsService.Instance.CustomData("PopupAd", new Dictionary<string, object>());
            StartCoroutine(StartScreen());
        }

        void Update()
        {
            if (!paused)
            {
                if (CurrentAds == 0 && updateOn == true)
                {
                    spawnManager.spawnerOff();
                    StartCoroutine(EndMinigame(2f, true));
                }
            }
        }

        public void AdAdded()
        {
            CurrentAds++;
        }

        public void AdDeleted()
        {
            CurrentAds--;
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