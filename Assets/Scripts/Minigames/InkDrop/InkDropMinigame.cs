using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Combat;
using UnityEngine.Analytics;
using Unity.Services.Analytics;

namespace InkDrop
{
    public class InkDropMinigame : Minigame
    {

        Transform inkSpawnRef;

        public Transform inkSpawn;
        public GameObject projectile;
        public Slider timebar;
        public GameObject lifeIndicator;
        public MinigameSFX mSFX;
        public GameObject UI;
        public GameObject Tutorial;

        public float leftBound = -10f;//-10 by default unless walls moved
        public float rightBound = 10f;//10 by default unless walls moved
        float variance;//variable that holds the random number between leftBound and rightBound
        float time = 0f;//current time variable, measured in seconds
        public float maxTime = 15f;//max time measured in seconds
        public int projectileCount = 3;//number of projectiles per wave
        public float startGrace = 3f;//time before shots start
        public float shotDelay = 1f;//delay between shots in a wave
        public bool spareLife;
        public bool paused;
        public int counter;
        public bool warmedUp;

        // Start is called before the first frame update
        void Start()
        {
            warmedUp = false;
            paused = true;
            counter = 0;
            AnalyticsService.Instance.CustomData("InkDrop", new Dictionary<string, object>());
            mSFX = GetComponent<MinigameSFX>();
            spareLife = true;
            variance = Random.Range(leftBound, rightBound);
            inkSpawnRef = inkSpawn;
            timebar.maxValue = maxTime;
            
            StartCoroutine(StartScreen());
        }

        void FixedUpdate()
        {
            if (!paused)
            {
                if (!warmedUp)
                {
                    StartCoroutine(WarmUp());
                    warmedUp = true;
                }
                if (time >= maxTime)
                {
                    StartCoroutine(EndMinigame(true));
                }
                if (spareLife == false && lifeIndicator.activeInHierarchy)
                {
                    lifeIndicator.SetActive(false);
                }
                time += 0.01f * Time.deltaTime;
                timebar.value = time;
            }
        }
        IEnumerator StartScreen()
        {
            yield return new WaitForSeconds(2f);
            Tutorial.SetActive(false);
            UI.SetActive(true);
            paused = false;
        }

        public IEnumerator WarmUp()
        {
            yield return new WaitForSeconds(startGrace);
            StartCoroutine(ShootProjectiles());
        }

        private IEnumerator ShootProjectiles()
        {
            int i = 0;
            while (i < projectileCount)
            {
                inkSpawn.position = inkSpawnRef.position;
                Vector3 temp = inkSpawn.position;
                temp.x = variance;
                inkSpawn.position = temp;
                GameObject p = Instantiate(projectile, inkSpawn);
                p.transform.SetParent(transform, true);
                variance = Random.Range(-10f, 10f);
                yield return new WaitForSeconds(shotDelay);
                i++;
                Debug.Log(i.ToString());
            }
            StartCoroutine(ShotCooldown());
        }

        public void CallEndMinigame(bool res)
        {
            StartCoroutine(EndMinigame(res));
        }

        IEnumerator ShotCooldown()
        {
            float cooldown = shotDelay * projectileCount;
            yield return new WaitForSeconds(cooldown);
            StartCoroutine(ShootProjectiles());
        }

    }
}