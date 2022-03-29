using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minigame1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
        //SceneLoader closes this scene, mainScene becomes unfrozen and play continues
    }

    void Lose()
    {
        Debug.Log("You lose a life!");
        //SceneLoader lose a life kept in a variable in persistent manager
        //all lives lost = game over scene?
    }
}
