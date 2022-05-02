using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    [SerializeField] private string minigameScene;

    public void LoadMinigame()
    {
        SceneManager.LoadSceneAsync(minigameScene, LoadSceneMode.Additive); //loadScene or loadSceneAsync?
    }

    public void CloseMinigame()
    {
        //playerController.unfreezePlayer();
        SceneManager.UnloadSceneAsync(minigameScene);
    }
}
