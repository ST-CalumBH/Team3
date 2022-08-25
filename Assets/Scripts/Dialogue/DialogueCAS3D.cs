using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Becky;

namespace Dialogue {
    public class DialogueCAS3D : MonoBehaviour
    {
        [SerializeField] private DialogueObject dialogueObject;//Text to be said on collision
        public bool isCollider;//select if text should be played on collision with box collider
        public bool isOnStart;//select if text should be played on start
        private DialogueUI UI;
        private FPPlayerMovement player;
        private bool activated = false;

        private void Start()
        {
            UI = FindObjectOfType<DialogueUI>();
            if(FindObjectOfType<FPPlayerMovement>())
            {
                player = FindObjectOfType<FPPlayerMovement>();
            }
            else
            {
                player = gameObject.AddComponent<FPPlayerMovement>();
            }
            if(isOnStart)
            {
                StartCoroutine(PlayDialogue());
            }
        }

        private void OnTriggerEnter(Collider other)
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
            yield return new WaitUntil(() => UI.IsOpen == false);
            player.unfreezePlayer();
        }
    }
}