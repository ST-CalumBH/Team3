using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadDialogue : MonoBehaviour
{
    [SerializeField] DialogueUI dialogueBox; //Canvas with DialogueUI script attached 
    [SerializeField] DialogueObject dialogueObject; //Dialogue object to be played
    bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        if (isActive == false)
        {
            StartCoroutine(PlayDialogue());
        }
    }
    
    IEnumerator PlayDialogue()
    {
        dialogueBox.ShowDialogue(dialogueObject); //the line that plays the script from dialogue object
        isActive = true;
        yield return new WaitUntil(() => dialogueBox.IsOpen == false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
