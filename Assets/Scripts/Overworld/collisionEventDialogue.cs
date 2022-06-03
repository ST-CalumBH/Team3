using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionEventDialogue : MonoBehaviour
{
   [SerializeField] private DialogueObject dialogueObject;//Text to be said on collision

    playerController player;
    float playerSpeed;
    bool activated = false;

    private void Start()
    {
        player = FindObjectOfType<playerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (activated == false)
            {
                StartCoroutine(PlayDialogue());
            }
        }
    }

    IEnumerator PlayDialogue()
    {
        player.DialogueUI.ShowDialogue(dialogueObject); //gets the Dialogue UI component from the Canvas attached to the player object
        activated = true;
        playerSpeed = player.moveSpeed;
        player.freezePlayer();
        player.moveSpeed = 0f;
        player.animator.SetFloat("Horizontal", 0);
        player.animator.SetFloat("Vertical", 0);
        player.animator.SetFloat("Speed", 0);
        yield return new WaitUntil(() => player.DialogueUI.IsOpen == false);
        player.unfreezePlayer();
        player.moveSpeed = playerSpeed;
    }
}
