using UnityEngine;
using Overworld;

namespace Dialogue {
    public class DialogueActivator : MonoBehaviour, IInteractable
    {
        [SerializeField] private DialogueObject dialogueObject;


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && other.TryGetComponent(out playerController player))
            {
                player.Interactable = this;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player") && other.TryGetComponent(out playerController player))
            {
                if (player.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
                {
                    player.Interactable = null;
                }
            }
        }

    public void Interact(playerController player)
        {
            player.DialogueUI.ShowDialogue(dialogueObject);
            player.freezePlayer();
        }
    }
}