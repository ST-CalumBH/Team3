using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueComboActivatorScript : MonoBehaviour
{
    [SerializeField] private DialogueObject dialogueObject;//Text to be said on collision
    public bool isCollider;//select if text should be played on collision with box collider
    public bool isOnStart;//select if text should be played on start
    private DialogueUI UI;
    private playerController player;
    private bool activated = false;

    private void Start()
    {
        UI = FindObjectOfType<DialogueUI>();
        if(FindObjectOfType<playerController>())
        {
            player = FindObjectOfType<playerController>();
        }
        else
        {
            player = gameObject.AddComponent<playerController>();
        }
        if(isOnStart)
        {
            StartCoroutine(PlayDialogue());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isCollider)
        {
            if (other.tag == "Player")
            {
                if (activated == false)
                {
                    activated = true;
                    StartCoroutine(PlayDialogue());
                }
            }
        }
    }

    IEnumerator PlayDialogue()
    {
        player.freezePlayer();
        UI.ShowDialogue(dialogueObject); //gets the Dialogue UI component from the Canvas attached to the player object  
        yield return new WaitUntil(() => player.DialogueUI.IsOpen == false);
        player.unfreezePlayer();
    }
}
