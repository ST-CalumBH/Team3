using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InkDrop {
    public class PlayerLaneMovement : MonoBehaviour
{
    public float moveSpeed = 15f;
    public Rigidbody2D rb;
    public SpriteRenderer sprRend;
    private Vector2 movement;
    public ParticleSystem hurt;
    public Animator animController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() //inputs
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        if (movement.x > 0)
        {
            animController.Play("Walk");
            sprRend.flipX = false;
        }
        else if (movement.x < 0)
        {
            animController.Play("Walk");
            sprRend.flipX = true;
        }
        else
        {
            animController.Play("Idle");
        }
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
        ParticleSystem ps = Instantiate(hurt, new Vector3(this.transform.position.x,this.transform.position.y+3f,this.transform.position.z),this.transform.rotation);
        ps.Play();
        yield return new WaitForSeconds(2f);
        Destroy(ps);
    }

    void FixedUpdate() //movement, physics
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
}
