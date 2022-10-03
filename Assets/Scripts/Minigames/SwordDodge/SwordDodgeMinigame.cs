using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Combat;
using UnityEngine.Analytics;
using Unity.Services.Analytics;

namespace SwordDodge
{
    public class SwordDodgeMinigame : Minigame
    {
        public GameObject animatorGO;
        public Warning lWarningGO;
        public Warning mWarningGO;
        public Warning rWarningGO;
        public SwordDodgePlayer player;
        public List<Warning> warnings;
        public ParticleSystem hurt;

        [SerializeField] GameObject Tutorial;

        SpriteRenderer lWarning;
        SpriteRenderer mWarning;
        SpriteRenderer rWarning;
        Animator animator;
        MinigameSFX mSFX;
        int laneNum;
        public int dodgeCount = 0;
        public bool isAttacking;
        bool cooldown = false;
        bool paused = true;

        // Start is called before the first frame update
        void Start()
        {
            AnalyticsService.Instance.CustomData("SwordDodge", new Dictionary<string, object>());
            mSFX = GetComponent<MinigameSFX>();
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
            player = FindObjectOfType<SwordDodgePlayer>();
            laneNum = player.curPosition;;
            StartCoroutine(StartScreen());
        }

        // Update is called once per frame
        void Update()
        {
            if (!paused)
            {
                if (!cooldown)
                {
                    if (isAttacking == true)
                    {
                        switch (laneNum)
                        {
                            case 0:
                                if (player.isTouching == PositionEnum.LEFT)
                                {
                                    //Debug.Log("Left Hit");
                                    Debug.Log("Reset");
                                    dodgeCount = 0;
                                    StartCoroutine(Hurt());
                                    StartCoroutine(CooldownTimer());
                                }
                                else
                                {
                                    dodgeCount++;
                                    StartCoroutine(CooldownTimer());
                                }
                                break;
                            case 1:
                                if (player.isTouching == PositionEnum.MIDDLE)
                                {
                                    //Debug.Log("Middle Hit");
                                    Debug.Log("Reset");
                                    dodgeCount = 0;
                                    StartCoroutine(Hurt());
                                    StartCoroutine(CooldownTimer());
                                }
                                else
                                {
                                    dodgeCount++;
                                    StartCoroutine(CooldownTimer());
                                }
                                break;
                            case 2:
                                if (player.isTouching == PositionEnum.RIGHT)
                                {
                                    //Debug.Log("Right Hit");
                                    Debug.Log("Reset");
                                    dodgeCount = 0;
                                    StartCoroutine(Hurt());
                                    StartCoroutine(CooldownTimer());
                                }
                                else
                                {
                                    dodgeCount++;
                                    StartCoroutine(CooldownTimer());
                                }
                                break;
                        }
                    }
                }
            }
        }

        public void Slash()
        {
            switch (laneNum)
            {
                case 0:
                    StartCoroutine(LeftAttack());
                    break;
                case 1:
                    StartCoroutine(MiddleAttack());
                    break;
                case 2:
                    StartCoroutine(RightAttack());
                    break;
                default:
                    StartCoroutine(MiddleAttack());
                    break;
            };

        }

        public void CallShowWarning()
        {
            StartCoroutine(ShowWarning());
        }

        public void CallEndMinigame()
        {
            StartCoroutine(EndMinigame(true));
        }

        public void CheckForTnight()//I love Fortnite
        {
            if (dodgeCount == 3)
            {
                StartCoroutine(EndMinigame(true));
            }
            else
            {
                StartCoroutine(ShowWarning());
            }
        }

        IEnumerator ShowWarning()
        {
            yield return new WaitForSeconds(1f);
            warnings[laneNum].StartFlash();
            yield return new WaitForSeconds(warnings[laneNum].GetTotalFlashTime() + 1f);
            Slash();
        }

        IEnumerator LeftAttack()
        {
            animator.Play("Base Layer.Left Lane");
            yield return new WaitForSeconds(0.30f); //Currently just manually inputting the animation length, but should make a dynamic system.
            isAttacking = true;
            mSFX.PlaySound(0);
            mSFX.PlaySound(1);
            yield return new WaitForSeconds(0.74f);
            isAttacking = false;
            yield return new WaitForSeconds(0.05f);
            animator.Play("Base Layer.Neutral");
            laneNum = player.curPosition;
            CheckForTnight();
        }
        IEnumerator MiddleAttack()
        {
            animator.Play("Base Layer.Mid Lane");
            yield return new WaitForSeconds(0.30f);
            isAttacking = true;
            mSFX.PlaySound(0);
            mSFX.PlaySound(1);
            yield return new WaitForSeconds(0.74f);
            isAttacking = false;
            yield return new WaitForSeconds(0.05f);
            animator.Play("Base Layer.Neutral");
            laneNum = player.curPosition;
            CheckForTnight();
        }
        IEnumerator RightAttack()
        {
            animator.Play("Base Layer.Right Lane");
            yield return new WaitForSeconds(0.30f);
            isAttacking = true;
            mSFX.PlaySound(0);
            mSFX.PlaySound(1);
            yield return new WaitForSeconds(0.74f);
            isAttacking = false;
            yield return new WaitForSeconds(0.05f);
            animator.Play("Base Layer.Neutral");
            laneNum = player.curPosition;
            CheckForTnight();
        }

        IEnumerator Hurt()
        {
            ParticleSystem ps = Instantiate(hurt, player.transform);
            ps.Play();
            mSFX.PlaySound(2);
            yield return new WaitForSeconds(2f);
            Destroy(ps);
        }

        IEnumerator CooldownTimer()
        {
            cooldown = true;
            yield return new WaitForSeconds(1.5f);
            cooldown = false;
        }
        IEnumerator StartScreen()
        {
            yield return new WaitForSeconds(2f);
            Tutorial.SetActive(false);
            paused = false;

            StartCoroutine(ShowWarning());
        }
    }
}