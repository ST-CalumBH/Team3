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

    [Header("Event Related")]
    [Tooltip("Do not use for repeatable conversations, also add event to MenuScript so it resets on NewGame")]
    public string eventName;

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

        if (eventNameChecker() && isOnStart)                                      // is this a named event? eg.E001
        {
            StartCoroutine(PlayDialogue());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (eventNameChecker() && isCollider)
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

    private bool eventNameChecker()                     // checks whether a one-time event should be played or not
    {
        if (eventName == null)
        {
            return true;                                // if it has no name, just let it through, most likely a repeatable event eg. object dialogue
        }
        else
        {
            if (PlayerPrefs.GetInt(eventName, 0) == 0)     // is the named event untriggered?
            {
                PlayerPrefs.SetInt(eventName, 1);       // now it is triggered
                return true;
            }

            return false;
        }
    }
}
