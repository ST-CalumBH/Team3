using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour 
{
    public float moveSpeed = 15f;
    public Rigidbody2D rb;
    private Vector2 movement;

    private bool inArea;
    private bool playerFreeze;

    [SerializeField] private GameObject eventTrigger;
    [SerializeField] private eventTimeline container;

    [SerializeField] private DialogueUI dialogueUI;

    public DialogueUI DialogueUI => dialogueUI;

    public IInteractable Interactable { get; set; }

    public Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inArea = false;
        playerFreeze = false;

        Interactable?.Interact(this); // Launches Dialogue on Awake
    }

    private void Update() //inputs
    {
        if (playerFreeze == false)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);

            if (movement.x == 1 || movement.x == -1 || movement.y == 1 || movement.y == -1)
            {
                animator.SetFloat("lastMoveX", movement.x);
                animator.SetFloat("lastMoveY", movement.y);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                Interactable?.Interact(this);
            }

            if (inArea && (Input.GetKeyDown(KeyCode.E)))
            {
                Interact(container);
              
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("homeBedroomScene");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Interact")
        {
            inArea = true;
            eventTrigger = other.gameObject;
            container = eventTrigger.GetComponent<eventTimeline>();
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

    private void Interact(eventTimeline timeline)
    {
        freezePlayer();
        timeline.beginCutsceneTimeline();
        Debug.Log("Interact Successful");

    }
}
