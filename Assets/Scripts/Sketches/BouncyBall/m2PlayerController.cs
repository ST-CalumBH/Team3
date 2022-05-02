using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m2PlayerController : MonoBehaviour
{
    public float moveSpeed = 15f;
    public Rigidbody2D rb;
    private Vector2 movement;
    
    [SerializeField] private m2SceneManager sceneManager;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void Update() //inputs
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            Debug.Log("Collision");
            sceneManager.failed = true;
        }
    }

    void FixedUpdate() //movement, physics
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }   
}

