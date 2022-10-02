using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClickChicken {
    public class chickenMovement : MonoBehaviour
    {
        [SerializeField] private int lives;
        [SerializeField] private float btmRange;
        [SerializeField] private float topRange;
        [SerializeField] private float directionFrequency;

        [SerializeField] private float frequencyMultiplier; // 0.9 etc
        [SerializeField] private float movementMultiplier; // 1.1 etc

        [SerializeField] private chickenLifeDisplay controller;

        private Animator anim;
        private SpriteRenderer sprite;

        private bool win = false;
        private bool hitStun = false;

        private float x;
        private float y;
        private float blinkTimer = 0;
        private float blinkLength = 3f;
        private float directionElaspedTime = 0f;

        public MinigameSFX mSFX;

        void Start()
        {
            x = 0f;
            y = 0f;
            anim = GetComponent<Animator>();
            sprite = GetComponent<SpriteRenderer>();
            StartCoroutine(DirectionModifier());
            StartCoroutine(Movement());
        }

        IEnumerator Movement()
        {
            while (win == false)
            {
                transform.Translate(x * Time.deltaTime, y * Time.deltaTime, 0);
                
                yield return null;
            }

        }

        IEnumerator DirectionModifier()
        {
            while (true)
            {
                directionElaspedTime += Time.deltaTime;


                if (directionElaspedTime >= directionFrequency)
                {
                    directionElaspedTime = 0f;

                    x = Random.Range(btmRange, topRange);
                    y = Random.Range(btmRange, topRange);
                }

                yield return null;
            }
        }

        public void Hit()
        {
            if (hitStun == false)
            {
                lives--;
                controller.LoseLife();

                if (lives > 0) { StartCoroutine(DamageRecieved()); }    // prevent damage flash when turned into egg
                DifficultyMultiplier();                                 // makeas da game harder 
            }
            
            if (lives == 0)
            {
                anim.Play("hit");
                mSFX.PlaySound(1);
                win = true;
            }
        }

        IEnumerator DamageRecieved()
        {
            blinkTimer = 0;
            Color color = sprite.color;
            hitStun = true;
            mSFX.PlaySound(0);

            while (blinkTimer != blinkLength)
            {
                blinkTimer++;

                color.a = 0.3f;
                sprite.color = color;
                yield return new WaitForSeconds(0.1f);

                color.a = 1f;
                sprite.color = color;
                yield return new WaitForSeconds(0.1f);

                yield return null;
            }

            hitStun = false;
        }

        public int GetLives()
        {
            return lives;
        }

        private void DifficultyMultiplier()
        {
            directionFrequency *= frequencyMultiplier; 
            topRange *= movementMultiplier;
            btmRange *= movementMultiplier;
        }
    }
}