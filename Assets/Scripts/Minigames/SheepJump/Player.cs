using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace SheepJump {
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


    public bool isHoldingJump = false;
    public float maxHoldJumpTime = 0.15f;
    public float holdJumpTimer = 0.0f;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isGrounded = false;
                velocity.y = jumpVelocity;
                isHoldingJump = true;

            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isHoldingJump = false;
        }
    }

    //calculating the coming down from the jump
    private void FixedUpdate()
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
        if(distance >= 1000f)
        {
            SceneManager.LoadScene("homeBedroomScene");
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
                hitObstacle(fences);
            }
        }

        transform.position = pos;
    }

    void hitObstacle(Fences fences)
    {
        Destroy(fences.gameObject);
        SceneManager.LoadScene("homeBedroomScene");
    }

    
}
}