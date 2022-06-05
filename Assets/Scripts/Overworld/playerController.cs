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

    float playerSpeed;

    [SerializeField] private AudioClip carpetSFX;

    AudioSource AS;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        AS = GetComponent<AudioSource>();
        inArea = false;
        playerFreeze = false;


        Interactable?.Interact(this); // should launch Dialogue on Awake, but doesn't
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
        else
        {
            movement.x = 0f;
            movement.y = 0f;

            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", 0);
            animator.SetFloat("Speed", 0);
        }

       /* if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("homeBedroomScene");
        }*/
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
        //playerSpeed = moveSpeed;
        playerFreeze = true;
        //moveSpeed = 0f;
        //animator.SetFloat("Horizontal", 0);
        //animator.SetFloat("Vertical", 0);
        //animator.SetFloat("Speed", 0);
    }

    public void unfreezePlayer()
    {
        playerFreeze = false;
        //moveSpeed = playerSpeed;
    }

    private void Interact(eventTimeline timeline)
    {
        freezePlayer();
        timeline.beginCutsceneTimeline();
    }

    public void PlayAudioClip()
    {
        if (!AS.isPlaying)
        {
            AS.PlayOneShot(carpetSFX);
        }
    }
}
