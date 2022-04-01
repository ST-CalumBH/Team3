using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minigame1 : MonoBehaviour
{
    [SerializeField] private sceneManager scenemanager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Win();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Lose();
        }
    }

    void Win()
    {
        Debug.Log("You win!");
        scenemanager.CloseMinigame();
        //SceneLoader closes this scene, mainScene becomes unfrozen and play continues
    }

    void Lose()
    {
        Debug.Log("You lose a life!");
        scenemanager.CloseMinigame(); //a different function could be called here eg. sceneManager.LoseMinigame, that calls a function in player script to loseALife();
        //SceneLoader lose a life kept in a variable in persistent manager
        //all lives lost = game over scene?
    }
}
