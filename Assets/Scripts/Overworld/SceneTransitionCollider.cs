using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionCollider : MonoBehaviour
{
    [SerializeField] private string nextScene;

    [Tooltip("Put down the spawn point of the scene it is transitioning to")]
    [SerializeField] private int newSpawnPoint = 0;  // default value so it doesn't crash

    playerController player;
    
    public Animator transition;

    public float transitionTime = 1f;

    private bool inArea;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inArea == true)
        {
            player = FindObjectOfType<playerController>();
            player.freezePlayer();
            SetSpawnPoint();
            changeScene();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inArea = false;
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

    public void SetSpawnPoint()
    {
        PlayerPrefs.SetInt("SpawnPoint", newSpawnPoint);
    }
}
