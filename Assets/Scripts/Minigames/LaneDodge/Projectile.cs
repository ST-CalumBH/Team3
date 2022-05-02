using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public LaneDodgeMinigame minigame;

    private void Start()
    {
        minigame = GetComponentInParent<LaneDodgeMinigame>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("CleanUp"))
        {
            Debug.Log("Clean Up");
            Destroy(this.gameObject);
            minigame.dodgeCount++;
        }
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Player");
            Destroy(this.gameObject);
            minigame.dodgeCount = 0;
        }
    }
}