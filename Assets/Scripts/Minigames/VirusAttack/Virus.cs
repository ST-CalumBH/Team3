using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VirusAttack {
    public class Virus : MonoBehaviour
    {
        private float moveSpeed = 2f;
        [SerializeField] private float topSpeed = 2f;
        [SerializeField] private float bottomSpeed = 4f;

        [SerializeField] private minigameManager manager;

        public MinigameSFX mSFX;

        void Start()
        {
            manager = GameObject.Find("minigameManager").GetComponent<minigameManager>();
            moveSpeed = Random.Range(bottomSpeed, topSpeed);
        }

        void Update()
        {
            transform.parent.Translate(moveSpeed * Time.deltaTime * -1, 0, 0);
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player") || (collision.gameObject.CompareTag("CleanUp")))
            {
                AudioSource.PlayClipAtPoint(mSFX.audioClips[0], Vector3.zero, 0.5f);
                manager.loseLife();
                Destroy(transform.parent.gameObject);
            }

            if (collision.gameObject.CompareTag("Bullet"))
            {
                AudioSource.PlayClipAtPoint(mSFX.audioClips[1], Vector3.zero, 0.4f);
                manager.gainPoint();
                Destroy(collision.gameObject);
                Destroy(transform.parent.gameObject);
            }
        }
    }
}