using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float moveSpeed = 15f;
    public Rigidbody2D rb;
    private Vector2 movement;
    private bool inArea;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() //inputs
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (inArea && (Input.GetKeyDown("joystick button 0")))
        {
            Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Interact")
        {
            inArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Interact")
        {
            inArea = false;
        }
    }

    void FixedUpdate() //movement, physics
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private void Interact()
    {
        Debug.Log("Interact Success");
    }

}
