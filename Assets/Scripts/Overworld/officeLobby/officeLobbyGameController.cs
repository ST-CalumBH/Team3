using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class officeLobbyGameController : MonoBehaviour
{
    [SerializeField] private playerController player;
    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private DialogueObject correctDialogue;
    [SerializeField] private DialogueObject duckDialogue;
    [SerializeField] private DialogueObject blankDialogue;
    [SerializeField] private GameObject doorSelector;
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
    Animator dsAnimator;
    SpriteRenderer dsSpriteRenderer;
    private bool gameStart = false;
    bool freezeStart = false;
    int curPosition = 1;
    float playerSpeed;


    // Start is called before the first frame update
    void Start()
    {
        dsAnimator = doorSelector.GetComponent<Animator>();
        dsSpriteRenderer = doorSelector.GetComponent<SpriteRenderer>();
        leftDoorAnim = leftDoor.GetComponent<Animator>();
        midDoorAnim = midDoor.GetComponent<Animator>();
        rightDoorAnim = rightDoor.GetComponent<Animator>();
        leftDoorSR = leftDoor.GetComponent<SpriteRenderer>();
        midDoorSR = midDoor.GetComponent<SpriteRenderer>();
        rightDoorSR = rightDoor.GetComponent<SpriteRenderer>();
        dsSpriteRenderer.enabled = false;
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
                        curPosition = 0;
                        break;
                    case 2:
                        dsAnimator.Play("Base Layer.RightToMid");
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
                        curPosition = 1;
                        break;
                    case 1:
                        dsAnimator.Play("Base Layer.MidToRight");
                        curPosition = 2;
                        break;
                    case 2:
                        Debug.Log("Right Wall");
                        break;
                }
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
                StartCoroutine(Check());
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerSpeed = player.moveSpeed;
            player.moveSpeed = 0f;
            player.animator.SetFloat("Horizontal", 0);
            player.animator.SetFloat("Vertical", 0);
            player.animator.SetFloat("Speed", 0);
            StartCoroutine(StartMinigame());
        }
    }

    private IEnumerator StartMinigame()
    {
        player.DialogueUI.ShowDialogue(dialogueObject);
        freezeStart = true;
        yield return new WaitUntil(() => player.DialogueUI.IsOpen == false);
        gameStart = true;
        dsSpriteRenderer.enabled = true;
        dsAnimator.Play("Base Layer.MidDoorIdle");
    }

    private IEnumerator Check()
    {
        dsSpriteRenderer.enabled = false;
        switch (curPosition)
        {
            case 0:
                player.DialogueUI.ShowDialogue(correctDialogue);
                leftDoorSR.enabled = true;
                leftDoorAnim.Play("Base Layer.CorrectDoor");
                yield return new WaitUntil(() => player.DialogueUI.IsOpen == false);
                break;
            case 1:
                player.DialogueUI.ShowDialogue(duckDialogue);
                midDoorSR.enabled = true;
                midDoorAnim.Play("Base Layer.DuckDoor");
                yield return new WaitUntil(() => player.DialogueUI.IsOpen == false);
                leftDoorSR.enabled = true;
                leftDoorAnim.Play("Base Layer.CorrectDoor");
                break;
            case 2:
                player.DialogueUI.ShowDialogue(blankDialogue);
                rightDoorSR.enabled = true;
                rightDoorAnim.Play("Base Layer.MissingDoor");
                yield return new WaitUntil(() => player.DialogueUI.IsOpen == false);
                leftDoorSR.enabled = true;
                leftDoorAnim.Play("Base Layer.CorrectDoor");
                break;
        }
        Parent.SetActive(false);
        player.unfreezePlayer();
        player.moveSpeed = playerSpeed;
        yield return new WaitForSeconds(0f);
        
    } 
}
