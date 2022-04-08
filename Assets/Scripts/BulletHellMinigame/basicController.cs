using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicController : MonoBehaviour
{
    public float moveSpeed = 15f;
    public Rigidbody2D rb;
    private Vector2 movement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() //inputs
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate() //movement, physics
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Hazard")
        {

        }
    }
}
