using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI {
    public class MainMenu : MonoBehaviour
    {

        public void PlayGame()
        {
            StartCoroutine(sceneTransition());
        }
        IEnumerator sceneTransition() 
        {
            var crossfade = FindObjectOfType<Animator>();
            crossfade.SetTrigger("Start");
            yield return new WaitForSeconds(1f);
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("SpawnPoint", 0);
            Time.timeScale = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            
        }

        public void QuitGame ()
        {
            Debug.Log("QUIT!");
            Application.Quit();
        }
    }
}