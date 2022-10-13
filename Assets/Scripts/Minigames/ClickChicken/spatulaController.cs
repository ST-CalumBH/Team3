using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using UnityEngine.Analytics;

namespace ClickChicken {
    public class spatulaController : Minigame
    {
        public AudioSource whack;

        public float moveSpeed = 15f;
        public Rigidbody2D rb;

        private Vector2 movement;

        [SerializeField] private chickenMovement controller;
        [SerializeField] private minigameLifeDisplay lifeController;
        private Animator anim;

        private bool inArea = false;

        public float fireDelay = 1.5f;
        private float fireElaspedTime;

        void Start()
        {
            fireElaspedTime = fireDelay;

            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }

        void Update()
        {
            fireElaspedTime += Time.deltaTime;

            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            if (Input.GetKeyDown(KeyCode.Space) && fireElaspedTime >= fireDelay)
            {
                Swat();
            }
        }

        void FixedUpdate() //movement, physics
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }

        public void Swat()
        {
            anim.Play("swatting");
            fireElaspedTime = 0f;
        }

        public void HitCheck()
        {
            whack.Play();

            if (inArea)
            {
                controller.Hit();

                if (lifeController.GetLives() == 0) { StartCoroutine(EndMinigame(2f, true)); }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Enemy")
            {
                inArea = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag == "Enemy")
            {
                inArea = false;
            }
        }
    }
}