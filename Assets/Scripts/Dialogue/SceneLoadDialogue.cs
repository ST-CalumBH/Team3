using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadDialogue : MonoBehaviour
{
    [SerializeField] private DialogueObject content; //Dialogue object to be played

    playerController player;
    float playerSpeed;
    bool isActive = false;

    // Start is called before the first frame update
    private void Start()
    {
        player = FindObjectOfType<playerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (isActive == false)
            {
                StartCoroutine(PlayDialogue());
            }
        }
    }

    IEnumerator PlayDialogue()
    {
        isActive = true;
        player.DialogueUI.ShowDialogue(content); //the line that plays the script from dialogue object
        playerSpeed = player.moveSpeed;
        player.freezePlayer();
        //player.moveSpeed = 0f;
        //player.animator.SetFloat("Horizontal", 0);
        //player.animator.SetFloat("Vertical", 0);
        //player.animator.SetFloat("Speed", 0);

        yield return new WaitUntil(() => player.DialogueUI.IsOpen == false);
        player.unfreezePlayer();
        player.moveSpeed = playerSpeed;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
