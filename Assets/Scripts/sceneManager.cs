using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    public void LoadMinigame()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive); //loadSceneAsync?
    }

    public void CloseMinigame()
    {
        SceneManager.UnloadSceneAsync(1);
    }
}
