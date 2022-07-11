using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionCollider : MonoBehaviour
{
    [SerializeField] private string nextScene;

    playerController player;
    
    public Animator transition;

    public float transitionTime = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player = FindObjectOfType<playerController>();
            player.freezePlayer();
            changeScene();
        }
    }

    public void changeScene()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(nextScene);
    }
}
