using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Dialogue;
using UI;

namespace Overworld {
    public class playerController : MonoBehaviour
    {
        public float moveSpeed = 15f;
        public Rigidbody2D rb;
        public Animator animator;
        public DialogueUI DialogueUI => dialogueUI;

        [SerializeField] private DialogueUI dialogueUI;
        [SerializeField] private AudioClip carpetSFX;

        [SerializeField] private bool playerFreeze;                 //boolean for determining if the player is frozen or not, controlling if the update function accepts input for 
        private float playerSpeed;                                  //keeps a reference of the player speed to apply when the player is unfrozen
        private Vector2 movement;
        private AudioSource AS;
        private PauseMenu menu;

        public iconE indicatorE;
        private int inAreaCounter = 0;                              // the 'E' indicator will only turn off if the player is no longer on top of any other collisions

        public IInteractable Interactable { get; set; }

        [Space(20)]
        [SerializeField] private Vector2[] spawnPoints;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            AS = GetComponent<AudioSource>();
            playerFreeze = false;
            playerSpeed = moveSpeed;

            menu = FindObjectOfType<PauseMenu>();

            MoveToSpawnPoint();

            // Debug.Log("Moved to spawn point: " + PlayerPrefs.GetInt("SpawnPoint"));
        }

        private void Update() //inputs
        {
            if (Time.timeScale == 0) return;                                                    // if Pause is on, skip the rest of update

            if (playerFreeze == false)
            {
                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");

                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", movement.y);
                animator.SetFloat("Speed", movement.sqrMagnitude);

                if (movement.x == 1 || movement.x == -1 || movement.y == 1 || movement.y == -1) // idle facing direction
                {
                    animator.SetFloat("lastMoveX", movement.x);
                    animator.SetFloat("lastMoveY", movement.y);
                }

                if (Input.GetKeyDown(KeyCode.E))                                                // dialogue interactor
                {
                    Interactable?.Interact(this);
                }
            }

            //if (menu.isGamePaused && playerFreeze == false)//checks if the game is paused and if the player is unfrozen, then freezes the player
            //{
            //    freezePlayer();
            //}
            //else if (!menu.isGamePaused && playerFreeze == true)//checks if the game is unpaused and if the player is frozen, then unfreezes the player
            //{
            //    unfreezePlayer();
            //}

            //if (dialogueUI.IsOpen && playerFreeze == false)
            //{
            //    freezePlayer();
            //}

            //if (!dialogueUI.IsOpen && playerFreeze == true)
            //{
            //    unfreezePlayer();
            //}

            /* if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("homeBedroomScene");
            }*/

            if (Input.GetKeyDown(KeyCode.O))                    // for testing purposes: sets player to spawn at the first item in the spawn list, resets events such as the kitchen
            {
                PlayerPrefs.DeleteAll();
                PlayerPrefs.SetInt("SpawnPoint", 0);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Interact" || other.tag == "Cutscene")
            {
                indicatorE.Show();
                inAreaCounter++;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag == "Interact" || other.tag == "Cutscene")
            {
                inAreaCounter--;  
            }
            

            if (inAreaCounter == 0)
            { 
                indicatorE.Hide();
            }
        }

        void FixedUpdate() //movement, physics
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }

        public void freezePlayer()
        {
            playerFreeze = true;
            moveSpeed = 0f;
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", 0);
            animator.SetFloat("Speed", 0);

            indicatorE.Hide();
            indicatorE.Disable();
        }

        public void unfreezePlayer()
        {
            playerFreeze = false;
            moveSpeed = playerSpeed;

            indicatorE.Enable();
        }

        public void PlayAudioClip()
        {
            if (!AS.isPlaying)
            {
                AS.PlayOneShot(carpetSFX);
            }
        }

        private void MoveToSpawnPoint()
        {
            if (spawnPoints.Length == 0) return;
            transform.position = spawnPoints[PlayerPrefs.GetInt("SpawnPoint", 0)]; // defaults to spawn at the first item on the list if a spawnpoint playerpref hasn't been made yet
        }
    }
}