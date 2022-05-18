using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialogueOnOpen : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject dialogueObject;

    [SerializeField] public GameObject playerObject;
    [SerializeField] public playerController player;

    public IInteractable Interactable { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<playerController>();

        player.Interactable = this;
    }

    public void Interact(playerController player)
    {
        player.DialogueUI.ShowDialogue(dialogueObject);
    }
}
