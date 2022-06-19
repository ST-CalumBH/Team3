using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            //failure
            Destroy(this.gameObject);
            minigame.CallEndMinigame(false);
        }
        if (collision.collider.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            minigame.inkCollected++;
            minigame.CallShotCooldown();
        }
    }
}