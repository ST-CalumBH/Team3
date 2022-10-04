using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

namespace CoffeeDodge
{
    public class CoffeeDodgeMinigame : Minigame
    {
        [SerializeField] private AudioClip splash;

        AudioSource audioSource;

        Animator animator;

        public GameObject Boss;
        public GameObject CoffeePrefab;
        public CoffeeDodgePlayer player;

        public GameObject UI;
        public GameObject Tutorial;


        public Text text;

        public ParticleSystem hurt;

        public int winThreshold = 9;

        List<int> attackQueue;
        List<bool> attacking  = new List<bool> { false, false, false };

        int attackNumber = 0;
        public int dodgeCount = 0;
        bool cooldown = false;
        bool paused = true;

        // Start is called before the first frame update
        void Start()
        {
            AnalyticsService.Instance.CustomData("CoffeeDodge", new Dictionary<string, object>());
            audioSource = GetComponent<AudioSource>();
            animator = Boss.GetComponent<Animator>();
            animator.StopPlayback();
            player = FindObjectOfType<CoffeeDodgePlayer>();
            paused = true;
            StartCoroutine(StartScreen());
        }

        // Update is called once per frame
        void Update()
        {
            if (!paused)
            {
                text.text = dodgeCount.ToString();
                if (!cooldown)
                {
                    if (attacking[0])
                    {
                        if (player.isTouching == CoffeeDodgePositionEnum.LEFT)
                        {
                            Debug.Log("Left Hit");
                            dodgeCount--;
                            StartCoroutine(Hurt());
                            StartCoroutine(CooldownTimer());
                        }
                        else
                        {
                            dodgeCount++;
                            StartCoroutine(CooldownTimer());
                        }
                    }
                    if (attacking[1])
                    {
                        if (player.isTouching == CoffeeDodgePositionEnum.MIDDLE)
                        {
                            Debug.Log("Middle Hit");
                            dodgeCount--;
                            StartCoroutine(Hurt());
                            StartCoroutine(CooldownTimer());
                        }
                        else
                        {
                            dodgeCount++;
                            StartCoroutine(CooldownTimer());
                        }
                    }
                    if (attacking[2])
                    {
                        if (player.isTouching == CoffeeDodgePositionEnum.RIGHT)
                        {
                            Debug.Log("Right Hit");
                            dodgeCount--;
                            StartCoroutine(Hurt());
                            StartCoroutine(CooldownTimer());
                        }
                        else
                        {
                            dodgeCount++;
                            StartCoroutine(CooldownTimer());
                        }
                    }
                    if (dodgeCount < 0) dodgeCount = 0;
                }
            }
        }

        public void CallEndMinigame()
        {
            StartCoroutine(EndMinigame(true));
        }

        public void CheckForTnight()//I love Fortnite
        {
            if (dodgeCount >= winThreshold)
            {
                StartCoroutine(EndMinigame(true));
            }
            else
            {
                StartCoroutine(QueueAttacks(3));
            }
        }

        IEnumerator QueueAttacks(int numAttacks)
        {
            yield return new WaitForSeconds(0.5f);

            //Manually initializing the attack queue like this guarantees the attacks will be spread out evenly.
            //However, it also means we cannot attack more than three times per round. Need to randomly generate the list if we want more.
            attackQueue = new List<int> { 0, 1, 2 };
            attackNumber = 0;

            //Shuffle list
            //https://answers.unity.com/questions/486626/how-can-i-shuffle-alist.html
            for (int i = 0; i < attackQueue.Count; i++)
            {
                int temp = attackQueue[i];
                int randomIndex = Random.Range(i, attackQueue.Count);
                attackQueue[i] = attackQueue[randomIndex];
                attackQueue[randomIndex] = temp;
            }
            while (attackQueue.Count > numAttacks)
            {
                attackQueue.RemoveAt(0);
            }

            foreach (var lane in attackQueue)
            {
                StartCoroutine(Attack(lane));
                yield return new WaitForSeconds(0.75f);
            }
        }

        IEnumerator StartScreen()
        {
            yield return new WaitForSeconds(2f);
            Tutorial.SetActive(false);
            UI.SetActive(true);
            paused = false;
            StartCoroutine(QueueAttacks(2));
        }

        IEnumerator Attack(int laneNum)
        {
            string laneName = laneNum == 2 ? "Right" : laneNum == 0 ? "Left" : "Middle";
            animator.Play("Base Layer.BossThrow");
            yield return new WaitForSeconds(0.23f); //Currently just manually inputting the animation length, but should make a dynamic system.
            var coffee = Instantiate(CoffeePrefab);
            var coffeeTransform = coffee.GetComponent<Transform>();
            var coffeeAnimator = coffeeTransform.Find("coffee-cup").GetComponent<Animator>();
            coffeeAnimator.Play("Base Layer.Coffee" + laneName);
            yield return new WaitForSeconds(1f);
            attacking[laneNum] = true;
            audioSource.PlayOneShot(splash);
            yield return new WaitForSeconds(0.25f);
            attacking[laneNum] = false;
            yield return new WaitForSeconds(0.30f);
            Destroy(coffee);

            if (++attackNumber >= attackQueue.Count)
            {
                attackNumber = 0;
                CheckForTnight();
            }
        }

        IEnumerator Hurt()
        {
            ParticleSystem ps = Instantiate(hurt, player.transform);
            ps.Play();
            yield return new WaitForSeconds(2f);
            Destroy(ps);
        }

        IEnumerator CooldownTimer()
        {
            cooldown = true;
            yield return new WaitForSeconds(0.3f);
            cooldown = false;
        }
    }
}