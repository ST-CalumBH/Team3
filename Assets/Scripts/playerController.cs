using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float moveSpeed = 15f;
    public Rigidbody2D rb;
    private Vector2 movement;

    private bool inArea;
    private bool playerFreeze;

    [SerializeField] private sceneManager scenemanager;

    [SerializeField] private GameObject enemyMinigame;
    [SerializeField] private enemyMinigameLoader minigameLoader;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inArea = false;
        playerFreeze = false;
    }

    private void Update() //inputs
    {
        if (playerFreeze == false)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            if (inArea && (Input.GetKeyDown(KeyCode.Z)))
            {
                Interact(minigameLoader);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Interact")
        {
            inArea = true;
            enemyMinigame = other.gameObject;
            minigameLoader = enemyMinigame.GetComponent<enemyMinigameLoader>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        inArea = false;
    }

    void FixedUpdate() //movement, physics
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void freezePlayer()
    {
        playerFreeze = true;
    }

    public void unfreezePlayer()
    {
        playerFreeze = false;
    }

    private void Interact(enemyMinigameLoader minigame)
    {
        freezePlayer();
        minigame.InstantiateMinigame();
        
        Debug.Log("Interact Successful");
    }
}
