using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI {
    public class PauseMenu : MonoBehaviour
    {
    
        public bool isGamePaused = false;

        //public float playerSpeed;

        [SerializeField] GameObject pauseMenuUI;


        // Start is called before the first frame update
        void Start()
        {
            pauseMenuUI.SetActive(false);
            isGamePaused = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isGamePaused)
                {
                    Resume();
                } else
                {
                    Pause();
                }
            }
        }

        public void Resume ()
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            isGamePaused = false;
        }

        void Pause ()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            isGamePaused = true;
        }

        public void LoadMenu(int sceneID)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(sceneID);
        }

        public void LoadOptions()
        {
            Debug.Log("Loading options...");
        }
        public void QuitGame()
        {
            Debug.Log("Quitting game...");
            Application.Quit();
        }
    }
}