using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VirusAttack {
    public class Virus : MonoBehaviour
    {
        private float moveSpeed = 4f;

        [SerializeField] private minigameManager manager;

        void Start()
        {
            manager = GameObject.Find("minigameManager").GetComponent<minigameManager>();
            moveSpeed = Random.Range(1f, 2f);
        }

        void Update()
        {
            transform.parent.Translate(moveSpeed * Time.deltaTime * -1, 0, 0);
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player") || (collision.gameObject.CompareTag("CleanUp")))
            {
                manager.loseLife();
                Destroy(transform.parent.gameObject);
            }

            if (collision.gameObject.CompareTag("Bullet"))
            {
                manager.gainPoint();
                Destroy(collision.gameObject);
                Destroy(transform.parent.gameObject);
            }
        }
    }
}