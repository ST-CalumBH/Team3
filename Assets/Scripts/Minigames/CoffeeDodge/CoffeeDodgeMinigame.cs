using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CoffeeDodge
{
    public class CoffeeDodgeMinigame : Minigame
    {
        [SerializeField] private AudioClip clang;

        AudioSource audioSource;

        public GameObject animatorGO;
        Animator animator;

        public Warning lWarningGO;
        public Warning mWarningGO;
        public Warning rWarningGO;

        public CoffeeDodgePlayer player;

        public List<Warning> warnings;

        public Text text;

        public ParticleSystem hurt;

        SpriteRenderer lWarning;
        SpriteRenderer mWarning;
        SpriteRenderer rWarning;

        List<int> attackQueue;
        int attackNumber = 0;
        public int dodgeCount = 0;
        public bool isAttacking;
        bool cooldown = false;

        // Start is called before the first frame update
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            animator = animatorGO.GetComponent<Animator>();
            animator.StopPlayback();
            warnings.Add(lWarningGO);
            warnings.Add(mWarningGO);
            warnings.Add(rWarningGO);
            lWarning = lWarningGO.GetComponent<SpriteRenderer>();
            mWarning = mWarningGO.GetComponent<SpriteRenderer>();
            rWarning = rWarningGO.GetComponent<SpriteRenderer>();
            lWarning.enabled = false;
            mWarning.enabled = false;
            rWarning.enabled = false;
            player = FindObjectOfType<CoffeeDodgePlayer>();
            StartCoroutine(QueueAttacks(2));
        }

        // Update is called once per frame
        void Update()
        {
            text.text = dodgeCount.ToString();
            if (!cooldown)
            {
                if (isAttacking == true)
                {
                    switch (attackQueue[attackNumber])
                    {
                        case 0:
                            if (player.isTouching == CoffeeDodgePositionEnum.LEFT)
                            {
                                Debug.Log("Left Hit");
                                dodgeCount--;
                                StartCoroutine(Hurt());
                                StartCoroutine(CooldownTimer());
                            }
                            break;
                        case 1:
                            if (player.isTouching == CoffeeDodgePositionEnum.MIDDLE)
                            {
                                Debug.Log("Middle Hit");
                                dodgeCount--;
                                StartCoroutine(Hurt());
                                StartCoroutine(CooldownTimer());
                            }
                            break;
                        case 2:
                            if (player.isTouching == CoffeeDodgePositionEnum.RIGHT)
                            {
                                Debug.Log("Right Hit");
                                dodgeCount--;
                                StartCoroutine(Hurt());
                                StartCoroutine(CooldownTimer());
                            }
                            break;
                    }
                }
            }
        }


        public void CallShowWarning()
        {
            StartCoroutine(ShowWarning(attackQueue[attackNumber]));
        }

        public void CallEndMinigame()
        {
            StartCoroutine(EndMinigame());
        }

        public void CheckForTnight()//I love Fortnite
        {
            if (dodgeCount == 5)
            {
                StartCoroutine(EndMinigame());
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
            StartCoroutine(ShowWarning(attackQueue[attackNumber]));
        }

        IEnumerator ShowWarning(int laneNum)
        {
            warnings[laneNum].StartFlash();

            if (++attackNumber >= attackQueue.Count)
            {
                yield return new WaitForSeconds(warnings[laneNum].GetTotalFlashTime() + 0.5f);
                attackNumber = 0;
                StartCoroutine(Attack(attackQueue[attackNumber]));
            }
            else
            {
                yield return new WaitForSeconds(warnings[laneNum].GetTotalFlashTime() + 0.4f);
                StartCoroutine(ShowWarning(attackQueue[attackNumber]));
            }

        }
        IEnumerator Attack(int laneNum)
        {
            string laneName = laneNum == 2 ? "Right" : laneNum == 0 ? "Left" : "Mid";
            animator.Play("Base Layer." + laneName + " Lane");
            yield return new WaitForSeconds(0.30f); //Currently just manually inputting the animation length, but should make a dynamic system.
            isAttacking = true;
            audioSource.PlayOneShot(clang);
            yield return new WaitForSeconds(0.44f);
            isAttacking = false;
            yield return new WaitForSeconds(0.05f);
            animator.Play("Base Layer.Neutral");

            if (++attackNumber >= attackQueue.Count)
            {
                attackNumber = 0;
                dodgeCount++;
                CheckForTnight();
            }
            else
            {
                yield return new WaitForSeconds(0.05f);
                StartCoroutine(Attack(attackQueue[attackNumber]));
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
            yield return new WaitForSeconds(1.5f);
            cooldown = false;
        }
    }
}