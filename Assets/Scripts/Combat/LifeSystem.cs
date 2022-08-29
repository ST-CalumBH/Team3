using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Combat {

    // https://www.youtube.com/watch?v=CPKAgyp8cno&ab_channel=ResoCoder
    public class LifeSystem : MonoBehaviour
    {
        public static LifeSystem Instance {  get; private set; }

        public int playerLives = 5;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            if (playerLives >= 0)
            {
                Debug.Log("Game Over!");
                // something should happen here?
            }
        }
    }
}