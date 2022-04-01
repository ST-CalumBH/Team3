using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    public void LoadMinigame()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive); //loadScene or loadSceneAsync?
    }

    public void CloseMinigame()
    {
        //playerController.unfreezePlayer();
        SceneManager.UnloadSceneAsync(1);
    }
}
