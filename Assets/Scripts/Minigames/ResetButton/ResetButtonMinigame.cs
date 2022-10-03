using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Combat;
using UnityEngine.Analytics;
using Unity.Services.Analytics;

namespace ResetButton
{
    public class ResetButtonMinigame : Minigame
    {
        public GameObject Hand;
        public GameObject Button;
        public Slider timebar;
        public Slider selector;
        public Transform handTransform;
        public GameObject selectorPanel;
        public GameObject Tutorial;
        public GameObject UI;

        public float bounceAngle = 45f;//degrees the hand varies from x axis either side
        public float zRotate = 1f;//Speed of movement
        public float hitZone = 0.2f;//decimal percentage of bar that is a hit
        public float maxTime = 15f;//max time measured in seconds
        public float cooldownLength = 0.5f;//number of seconds that the cooldown between missed hits lasts

        enum State { CLOCKWISE, COUNTERCLOCKWISE }

        State curState;
        float time = 0f;//current time variable, measured in seconds
        float zoneDegrees;
        float handRotation;
        bool cooldown;
        bool paused = true;
        float cooldownTimer = 0;
        float cooldownLengthFrames;
        MinigameSFX mSFX;


        // Start is called before the first frame update
        void Start()
        {
            cooldown = true;
            paused = true;
            mSFX = GetComponent<MinigameSFX>();
            cooldownLengthFrames = cooldownLength * 50f;
            handRotation = handTransform.rotation.eulerAngles.z;
            selector.maxValue = bounceAngle;
            selector.minValue = -bounceAngle;
            selector.value = (selector.maxValue + selector.minValue) / 2;
            curState = State.CLOCKWISE;
            RectTransform sp = selectorPanel.GetComponent<RectTransform>();
            sp.sizeDelta = new Vector2(1500 * hitZone, 70);
            zoneDegrees = bounceAngle * hitZone;
            timebar.maxValue = maxTime;
            timebar.value = 0f;
            AnalyticsService.Instance.CustomData("ResetButton", new Dictionary<string, object>());
            
            StartCoroutine(StartScreen());
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (!paused)
            {
                if (curState == State.COUNTERCLOCKWISE)
                {
                    handTransform.Rotate(handTransform.rotation.x, handTransform.rotation.y, zRotate);
                    selector.value += zRotate;
                }
                if (curState == State.CLOCKWISE)
                {
                    handTransform.Rotate(handTransform.rotation.x, handTransform.rotation.y, -zRotate);
                    selector.value -= zRotate;
                }
                if (handTransform.eulerAngles.z > (handRotation + bounceAngle))
                {
                    curState = State.CLOCKWISE;
                }
                if (handTransform.eulerAngles.z < (handRotation - bounceAngle))
                {
                    curState = State.COUNTERCLOCKWISE;
                }
                if (cooldown == true)
                {
                    if (cooldownTimer >= cooldownLengthFrames)
                    {
                        cooldown = false;
                        Debug.Log("Cooldown over");
                    }
                    else
                    {
                        cooldownTimer++;
                        Debug.Log(cooldownTimer.ToString());
                    }
                }
            }
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) || (Input.GetKeyDown(KeyCode.E)))
            {
                if (handTransform.eulerAngles.z > (handRotation - zoneDegrees) && handTransform.eulerAngles.z < (handRotation + zoneDegrees) && cooldown==false)
                {
                    mSFX.PlaySound(0);
                    mSFX.PlaySound(1);
                    zRotate = 0;
                    StartCoroutine(EndMinigame(true));
                }
                else
                {
                    mSFX.PlaySound(2);
                    mSFX.PlaySound(3);
                    cooldown = true;
                    cooldownTimer = 0;
                    Debug.Log("Cooldown Start");
                }    
            }
            if (time >= maxTime)
            {
                zRotate = 0;
                StartCoroutine(EndMinigame(false));
            }
        }

        IEnumerator StartScreen()
        {
            yield return new WaitForSeconds(1f);
            cooldown = true;
            cooldownTimer = 0;
            yield return new WaitForSeconds(1f);
            cooldown = true;
            cooldownTimer = 0;
            Tutorial.SetActive(false);
            UI.SetActive(true);
            paused = false;
            
            StartCoroutine(Timer());
        }

        IEnumerator Timer()
        {
            time += 0.01f;
            timebar.value = time;
            yield return new WaitForSeconds(0.01f);
            if (time <= maxTime)
            {
                StartCoroutine(Timer());
            }
        }

    }
}