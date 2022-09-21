using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InkDrop {
    public class Projectile : MonoBehaviour
    {
        public InkDropMinigame minigame;

        private void Start()
        {
            minigame = FindObjectOfType<InkDropMinigame>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("CleanUp"))
            {
                minigame.mSFX.PlaySound(1);
                Destroy(this.gameObject);
                if (minigame.spareLife == false)
                {
                    minigame.CallEndMinigame(false);
                }
                else
                {
                    minigame.spareLife = false;
                }
            }
            if (collision.collider.CompareTag("Player"))
            {
                minigame.mSFX.PlaySound(0);
                Destroy(this.gameObject);
            }
        }
    }
}