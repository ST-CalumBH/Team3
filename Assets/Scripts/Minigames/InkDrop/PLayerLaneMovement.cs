using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerLaneMovement : MonoBehaviour
{
    public float moveSpeed = 15f;
    public Rigidbody2D rb;
    public SpriteRenderer sprRend;
    private Vector2 movement;
    public ParticleSystem hurt;
    public Animator animController;
    public float walkSFXInterval = 1f;
    bool walkCooldown;
    MinigameSFX mSFX;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mSFX = GetComponent<MinigameSFX>();
    }

    private void Update() //inputs
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        if (movement.x > 0)
        {
            animController.Play("Walk");
            sprRend.flipX = false;
            if (walkCooldown == false)
            {
                StartCoroutine(WalkSFX());
            }
        }
        else if (movement.x < 0)
        {
            animController.Play("Walk");
            sprRend.flipX = true;
            if (walkCooldown == false)
            {
                StartCoroutine(WalkSFX());
            }
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

    IEnumerator WalkSFX()
    {
        walkCooldown = true;
        mSFX.PlaySound(0);
        yield return new WaitForSeconds(walkSFXInterval);
        walkCooldown = false;
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

