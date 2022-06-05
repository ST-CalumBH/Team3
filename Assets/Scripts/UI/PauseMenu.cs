using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
   
    public static bool isGamePaused = false;

    public float playerSpeed;

    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] playerController player;


    // Start is called before the first frame update
    void Start()
    {
        pauseMenuUI.SetActive(false);
        playerSpeed = player.moveSpeed;
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
        player.unfreezePlayer();
        player.moveSpeed = playerSpeed;
        isGamePaused = false;
    }

    void Pause ()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        player.freezePlayer();
        player.moveSpeed = 0f;
        player.animator.SetFloat("Horizontal", 0);
        player.animator.SetFloat("Vertical", 0);
        player.animator.SetFloat("Speed", 0);
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
