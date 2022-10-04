using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using UnityEngine.Analytics;
using Unity.Services.Analytics;
using UnityEngine.SceneManagement;

namespace VirusAttack {
    public class minigameManager : Minigame
    {
        [SerializeField] private string nextScene;
        [SerializeField] private int newSpawnPoint = 0;
        [SerializeField] GameObject Tutorial;
        
        public int lives = 3;
        public int enemyKilled = 0;
        public int enemyGoal = 20;
        public int counter = 0;

        bool paused = true;

        [SerializeField] private spawnerManager spawnManager;

        private void Start()
        {
            AnalyticsService.Instance.CustomData("VirusAttack", new Dictionary<string, object>());
            StartCoroutine(StartScreen());
        }

        IEnumerator StartScreen()
        {
            yield return new WaitForSeconds(2f);
            Tutorial.SetActive(false);
            paused = false;
            BasicMovement BM = FindObjectOfType<BasicMovement>();
            spawnerManager SM = FindObjectOfType<spawnerManager>();
            SideShoot SS = FindObjectOfType<SideShoot>();
            BM.paused = false;
            SM.paused = false;
            SS.paused = false;
        }

        void Update()
        {
            if (!paused)
            {
                if (lives == 0)
                {
                    PlayerPrefs.SetInt("SpawnPoint", newSpawnPoint);
                    SceneManager.LoadScene(nextScene);

                    //spawnManager.spawnerOff();
                    //StartCoroutine(EndMinigame(true)); // meant to be false, but when an EndMinigame(false) is run, it needs the life persistant manager
                }
                else if (enemyGoal == enemyKilled)
                {
                    PlayerPrefs.SetInt("SpawnPoint", newSpawnPoint);
                    SceneManager.LoadScene(nextScene);

                    //spawnManager.spawnerOff();
                    //StartCoroutine(EndMinigame(true));
                }
            }
        }


        public void loseLife()
        {
            if (spawnManager.checkState()) { lives--; }         // if minigame is active, lose a life
        }

        public void gainPoint()
        {
            if (spawnManager.checkState()) { enemyKilled++; }         // if minigame is active, lose a life
        }
    }
}
