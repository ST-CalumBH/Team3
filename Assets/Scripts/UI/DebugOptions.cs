using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI {
    public class DebugOptions : MonoBehaviour
    {

        public void LaunchScene(string scene = "minigame_SheepJump")
        {
            StartCoroutine(sceneTransition(scene));
        }
        IEnumerator sceneTransition(string scene) 
        {
            var crossfade = FindObjectOfType<Animator>();
            crossfade.SetTrigger("Start");
            yield return new WaitForSeconds(1f);
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("SpawnPoint", 0);
            SceneManager.LoadScene(scene);
        }

        public void FlushAnalytics()
        {
            AnalyticsService.Instance.Flush();
        }
    }
}