using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spatulaController : Minigame
{
    public float moveSpeed = 15f;
    public Rigidbody2D rb;

    private Vector2 movement;

    private GameObject enemy;
    private chickenMovement controller;

    private bool swatCooldown = false;
    private bool inArea = false;
    private bool isGrowing = true;
    private int scaleTimer = 0;
    private Vector3 scaleChange = new Vector3(0.005f, 0.005f, 0.005f);

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy = GameObject.Find("chickenKnight");
        controller = enemy.GetComponent<chickenMovement>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Swat();
        }

        ChangeSize();
    }

    void FixedUpdate() //movement, physics
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void Swat()
    {
        if (swatCooldown == false)
        {
            Debug.Log("Swat Action");

            swatCooldown = true;
        }
    }

    public void HitCheck()
    {
        if (inArea)
        {
            Debug.Log("Chicken has been HIT!");
            controller.Hit();
            StartCoroutine(EndMinigame());
        }
    }

    public void ChangeSize()
    {
        if (swatCooldown == true)
        {
            if (isGrowing)
            {
                transform.localScale -= scaleChange;
                scaleTimer++;

                if (scaleTimer == 20)
                {
                    isGrowing = false;
                    scaleTimer = 0;
                    HitCheck();
                }
            }
            else
            {
                transform.localScale += scaleChange;
                scaleTimer++;

                if (scaleTimer == 20)
                {
                    isGrowing = true;
                    scaleTimer = 0;
                    swatCooldown = false; // IEnumerator goes here
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("Within Area");
            inArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        inArea = false;
    }
}
