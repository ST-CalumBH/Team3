using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

namespace VirusAttack {
    public class minigameManager : Minigame
    {
        public int lives = 3;
        public int enemyKilled = 0;
        public int enemyGoal = 20;

        [SerializeField] private spawnerManager spawnManager;

        void Update()
        {
            if (lives == 0)
            {
                spawnManager.spawnerOff();
                StartCoroutine(EndMinigame(2f, false));
            }
            else if (enemyGoal == enemyKilled)
            {
                spawnManager.spawnerOff();
                StartCoroutine(EndMinigame(2f, true));
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
