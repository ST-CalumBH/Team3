using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Resetting all events to untriggered, not sure about naming convention, should they literally be named "homeBeedroomSceneWakeUp"?
        // E = event
        PlayerPrefs.SetInt("E001", 0);                    // homeBedroomScene wake up
        PlayerPrefs.SetInt("E002", 0);                    // homeKitchenScene making breakfast
        PlayerPrefs.SetInt("E003", 0);                    // homeKitchenScene aftermath chicken battle

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame ()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
