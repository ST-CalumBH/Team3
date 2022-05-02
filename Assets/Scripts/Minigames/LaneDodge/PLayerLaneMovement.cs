using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerLaneMovement : MonoBehaviour
{
    public float moveSpeed = 15f;
    public Rigidbody2D rb;
    private Vector2 movement;
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
            StartCoroutine(Hurt());
        }
    }

    IEnumerator Hurt()
    {
        ParticleSystem ps = Instantiate(hurt, this.transform);
        ps.Play();
        yield return new WaitForSeconds(2f);
        Destroy(ps);
    }

    void FixedUpdate() //movement, physics
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}

