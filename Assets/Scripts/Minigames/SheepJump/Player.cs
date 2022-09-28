using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.Services.Analytics;

namespace SheepJump
{
    public class Player : MonoBehaviour
    {
        public float gravity;
        public Vector2 velocity;
        public float maxAcceleration = 10;
        public float maxXVelocity = 100;
        public float acceleration = 10;
        public float distance = 0;
        public float jumpVelocity = 20;
        public float groundHeight = 2;
        public bool isGrounded = false;
        [SerializeField] GameObject blackout;
        [SerializeField] float baasPerSecond = 1f; //higher is slower.


        public bool isHoldingJump = false;
        public float maxHoldJumpTime = 0.15f;
        public float holdJumpTimer = 0.0f;
        bool ended = false;
        float counter = 0;
        float secondVariable;

        private Animator animator;
        MinigameSFX mSFX;


        void Start()
        {
            AnalyticsService.Instance.CustomData("SheepJump", new Dictionary<string, object>());
            animator = GetComponent<Animator>();
            mSFX = GetComponent<MinigameSFX>();
            counter = 0; 
            secondVariable = Random.Range(50, 151);
        }


        void Update()
        {
            if (!ended)
            {
                if (isGrounded)
                {
                    if (!mSFX.mgAudioSource.isPlaying)
                    {
                        mSFX.PlaySound(0);
                    }
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        isGrounded = false;
                        velocity.y = jumpVelocity;
                        isHoldingJump = true;
                        mSFX.PlaySound(1);
                    }
                }

                /*if (Input.GetKeyUp(KeyCode.Space))
                {
                    isHoldingJump = false;
                }
                */
            }
        }

        //calculating the coming down from the jump
        private void FixedUpdate()
        {
            if (!ended)
            {
                Vector2 pos = transform.position;

                if (!isGrounded)
                {
                    if (isHoldingJump)
                    {
                        holdJumpTimer += Time.fixedDeltaTime;
                        if (holdJumpTimer >= maxHoldJumpTime)
                        {
                            isHoldingJump = false;
                        }

                    }
                    pos.y += velocity.y * Time.fixedDeltaTime;
                    if (!isHoldingJump)
                    {
                        velocity.y += gravity * Time.fixedDeltaTime;
                    }


                    //ground collision
                    if (pos.y <= groundHeight)
                    {
                        pos.y = groundHeight;
                        isGrounded = true;
                        holdJumpTimer = 0;

                    }
                }

                //gaining distance
                distance += velocity.x * Time.fixedDeltaTime;
                if (distance >= 500f)
                {
                    StartCoroutine(EndGame());
                }
                // UI score/ counter thing


                //speed up on ground and cap speed
                if (isGrounded)
                {
                    float velocityRatio = velocity.x / maxXVelocity;
                    acceleration = maxAcceleration * (1 - velocityRatio);

                    velocity.x += acceleration * Time.fixedDeltaTime;
                    if (velocity.x >= maxXVelocity)
                    {
                        velocity.x = maxXVelocity;
                    }
                }

                Vector2 obstOrigin = new Vector2(pos.x, pos.y);
                RaycastHit2D obstHitX = Physics2D.Raycast(obstOrigin, Vector2.right, velocity.x * Time.fixedDeltaTime);
                if (obstHitX.collider != null)
                {
                    Fences fences = obstHitX.collider.GetComponent<Fences>();
                    if (fences != null)
                    {

                        StartCoroutine(hitObstacle(fences));
                    }
                }
                
                if (counter == (secondVariable / baasPerSecond))
                {
                    mSFX.PlaySound(3, 0.5f);
                    counter = 0;
                    secondVariable = Random.Range(50, 151);
                }
                else
                {
                    counter++;
                }
                transform.position = pos;
            }
        }

        IEnumerator hitObstacle(Fences fences)
        {
            ended = true;
            Destroy(fences.gameObject);
            blackout.SetActive(true);
            velocity.x = 0f;
            velocity.y = 0f;
            mSFX.PlaySound(2, 0.25f);
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("homeBedroomScene");
        }

        IEnumerator EndGame()
        {
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("homeBedroomScene");
        }
    }
}