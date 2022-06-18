using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public InkDropMinigame minigame;

    private void Start()
    {
        minigame = GetComponentInParent<InkDropMinigame>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("CleanUp"))
        {
            //failure
            minigame.inkCollected = 0;
            Destroy(this.gameObject);

        }
        if (collision.collider.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            minigame.inkCollected++;
            minigame.CallShotCooldown();
        }
    }
}