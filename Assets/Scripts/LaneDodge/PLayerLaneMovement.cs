using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerLaneMovement : MonoBehaviour
{
    public float moveSpeed = 15f;
    public Rigidbody2D rb;
    private Vector2 movement;
    public LaneDodgeMinigame manager;
    public ParticleSystem hurt;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() //inputs
    {
        movement.x = Input.GetAxisRaw("Horizontal");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            manager.dodgeCount = 0;
            hurt.Play();
        }
    }

    void FixedUpdate() //movement, physics
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}

