using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPPlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public IInteractable Interactable { get; set; }
    public PauseMenu menu;
    private bool playerFreeze;

    // Update is called once per frame
    void Update()
    {
        if (playerFreeze == false)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");


            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);
        }


        if (menu.isGamePaused && playerFreeze == false)//checks if the game is paused and if the player is unfrozen, then freezes the player
        {
            freezePlayer();
        }
        else if (!menu.isGamePaused && playerFreeze == true)//checks if the game is unpaused and if the player is frozen, then unfreezes the player
        {
            unfreezePlayer();
        }
    }
    public void freezePlayer()
    {
        playerFreeze = true;
    }

    public void unfreezePlayer()
    {
        playerFreeze = false;
    }
}
