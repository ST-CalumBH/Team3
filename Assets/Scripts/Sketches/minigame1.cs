using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;


namespace Sketches {
    public class minigame1 : Minigame
    {
        //[SerializeField] private GameObject player; //SerializedField is for viewing variables while testing, shouldn't be edited at all.
        //[SerializeField] private playerController playercontroller;

        //private void Start()
        //{
        //    player = GameObject.Find("Player");
        //    playercontroller = player.GetComponent<playerController>();
        //}

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
            //playercontroller.unfreezePlayer();
            StartCoroutine(EndMinigame(true));
        }

        void Lose()
        {
            Debug.Log("You lose a life!");
            //playercontroller.unfreezePlayer();
            StartCoroutine(EndMinigame(false));
            //a different function could be called here eg. sceneManager.LoseMinigame, that calls a function in player script to loseALife();
            //Life kept in a variable in persistent manager
            //all lives lost = game over scene
        }

        //public void DestroyMinigame()
        //{
        //    Destroy(transform.parent.gameObject);
        //}
    }
}
