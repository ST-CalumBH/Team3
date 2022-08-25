using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VirusAttack;
using Combat;

namespace PopupAd {
    public class popupadManager : Minigame
    {
        [SerializeField] private int CurrentAds = 0;

        [SerializeField] private spawnerManager spawnManager;

        private bool updateOn = false;

        private void Start()
        {
            StartCoroutine(UpdatePause());
        }

        void Update()
        {
            if (CurrentAds == 0 && updateOn == true)
            {
                spawnManager.spawnerOff();
                StartCoroutine(EndMinigame(2f, true));
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

        IEnumerator UpdatePause()
        {
            yield return new WaitForSeconds(1f);
            updateOn = true;
        }
    }
}