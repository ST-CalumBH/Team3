using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue;

namespace Overworld {
    public class officeLobbyGameController : MonoBehaviour
    {
        [SerializeField] private playerController player;

        [SerializeField] private DialogueObject dialogueObject;
        [SerializeField] private DialogueObject correctDialogue;
        [SerializeField] private DialogueObject duckDialogue;
        [SerializeField] private DialogueObject blankDialogue;

        [SerializeField] private GameObject blackout;
        [SerializeField] private GameObject doorSelector;
        [SerializeField] private GameObject correctDoorMask;
        [SerializeField] private GameObject leftDoor;
        [SerializeField] private GameObject midDoor;
        [SerializeField] private GameObject rightDoor;

        [SerializeField] private GameObject Parent;


        Animator leftDoorAnim;
        Animator midDoorAnim;
        Animator rightDoorAnim;
        SpriteRenderer leftDoorSR;
        SpriteRenderer midDoorSR;
        SpriteRenderer rightDoorSR;
        SpriteRenderer spotlightMask;
        Animator dsAnimator;
        MinigameSFX mSFX;
        private bool gameStart = false;
        private bool gameOver = false;
        bool freezeStart = false;
        int curPosition = 1;


        // Start is called before the first frame update
        void Start()
        {
            mSFX = GetComponent<MinigameSFX>();
            doorSelector.SetActive(false);
            correctDoorMask.SetActive(false);
            dsAnimator = doorSelector.GetComponent<Animator>();
            leftDoorAnim = leftDoor.GetComponent<Animator>();
            midDoorAnim = midDoor.GetComponent<Animator>();
            rightDoorAnim = rightDoor.GetComponent<Animator>();
            leftDoorSR = leftDoor.GetComponent<SpriteRenderer>();
            midDoorSR = midDoor.GetComponent<SpriteRenderer>();
            rightDoorSR = rightDoor.GetComponent<SpriteRenderer>();
            leftDoorSR.enabled = false;
            midDoorSR.enabled = false;
            rightDoorSR.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (freezeStart)
            {
                player.freezePlayer();
            }
            if (gameStart == true)
            {
                
                if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
                {
                    switch (curPosition)
                    {
                        case 0:
                            Debug.Log("Left Wall");
                            break;
                        case 1:
                            dsAnimator.Play("Base Layer.MidToLeft");
                            mSFX.PlaySound(4);
                            curPosition = 0;
                            break;
                        case 2:
                            dsAnimator.Play("Base Layer.RightToMid");
                            mSFX.PlaySound(4);
                            curPosition = 1;
                            break;
                    }
                }
                else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
                {
                    switch (curPosition)
                    {
                        case 0:
                            dsAnimator.Play("Base Layer.LeftToMid");
                            mSFX.PlaySound(4);
                            curPosition = 1;
                            break;
                        case 1:
                            dsAnimator.Play("Base Layer.MidToRight");
                            mSFX.PlaySound(4);
                            curPosition = 2;
                            break;
                        case 2:
                            Debug.Log("Right Wall");
                            break;
                    }
                }

                if (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.Space))
                {
                    StartCoroutine(Check());
                }
            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!gameOver && other.tag == "Player")
            {
                player.freezePlayer();
                StartCoroutine(StartMinigame());
            }
        }

        private IEnumerator StartMinigame()
        {
            mSFX.PlaySound(6, 0.5f);
            blackout.SetActive(true);
            yield return new WaitForSeconds(4);
            blackout.SetActive(false);
            mSFX.PlaySound(0);
            yield return new WaitForSeconds(2);
            mSFX.PlaySound(3, 0.15f);
            player.DialogueUI.ShowDialogue(dialogueObject);
            freezeStart = true;
            yield return new WaitUntil(() => player.DialogueUI.IsOpen == false);
            yield return new WaitForSeconds(0.5f);
            gameStart = true;
            doorSelector.SetActive(true);
            mSFX.PlaySound(5);
            dsAnimator.Play("Base Layer.MidDoorIdle");
        }

        private IEnumerator Check()
        {
            GameObject.Find("Cursor").SetActive(false);
            gameStart = false;
            freezeStart = false;
            gameOver = true;
            mSFX.PlaySound(0, 0.25f);
            switch (curPosition)
            {
                case 0:
                    mSFX.PlaySound(1);
                    player.DialogueUI.ShowDialogue(correctDialogue);
                    leftDoorSR.enabled = true;
                    leftDoorAnim.Play("Base Layer.CorrectDoor");
                    yield return new WaitUntil(() => player.DialogueUI.IsOpen == false);
                    break;
                case 1:
                    mSFX.PlaySound(2);
                    player.DialogueUI.ShowDialogue(duckDialogue);
                    midDoorSR.enabled = true;
                    midDoorAnim.Play("Base Layer.DuckDoor");
                    yield return new WaitUntil(() => player.DialogueUI.IsOpen == false);
                    leftDoorSR.enabled = true;
                    mSFX.PlaySound(0);
                    correctDoorMask.SetActive(true);
                    leftDoorAnim.Play("Base Layer.CorrectDoor");
                    break;
                case 2:
                    mSFX.PlaySound(2);
                    player.DialogueUI.ShowDialogue(blankDialogue);
                    rightDoorSR.enabled = true;
                    rightDoorAnim.Play("Base Layer.MissingDoor");
                    yield return new WaitUntil(() => player.DialogueUI.IsOpen == false);
                    leftDoorSR.enabled = true;
                    mSFX.PlaySound(0);
                    correctDoorMask.SetActive(true);
                    leftDoorAnim.Play("Base Layer.CorrectDoor");
                    break;
            }
            player.unfreezePlayer();
        } 
    }
}