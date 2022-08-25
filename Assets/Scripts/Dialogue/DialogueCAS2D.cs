using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Overworld;

namespace Dialogue {
    public class DialogueCAS2D : MonoBehaviour
    {
        [SerializeField] private DialogueObject dialogueObject;//Text to be said on collision
        public bool isCollider;//select if text should be played on collision with box collider
        public bool isOnStart;//select if text should be played on start
        private DialogueUI UI;
        private playerController player;
        private bool activated = false;

        [Header("Event Related")]
        [Tooltip("Do not use for repeatable conversations")]
        [SerializeField] private string eventName;
        [SerializeField] private string[] previousEventsRequired;                                   // what other events need to be activated before this is active?

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
            if (eventNameChecker() && isCollider) // eventNameChecker() && touching collider on entry?
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
            PlayerPrefs.SetInt(eventName, 1);
            player.freezePlayer();
            UI.ShowDialogue(dialogueObject); //gets the Dialogue UI component from the Canvas attached to the player object  
            yield return new WaitUntil(() => player.DialogueUI.IsOpen == false);
            player.unfreezePlayer();
        }

        private bool eventNameChecker()                         // checks whether a one-time event should be played or not
        {
            if (previousEventsRequired != null)                 // are there any previous events to check?
            {
                foreach (string i in previousEventsRequired)    // iterates through the list
                {
                    if (PlayerPrefs.GetInt(i,0) == 0)           // has the event NOT been triggered?
                    {
                        return false;                           // if not, do not proceed with event
                    }
                }
            }

            if (eventName != null)
            {
                if (PlayerPrefs.GetInt(eventName, 0) == 0)      // is the named event untriggered?
                {
                    return true;
                }
                else
                {
                    return false;                               // event was already triggered, so do not proceed with event
                }
            }
            else
            {
                return true;                                    // if it has no name, just let it through, most likely a repeatable event eg. object dialogue
            }
        }
    }
}