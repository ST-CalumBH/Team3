using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class eventTimeline : MonoBehaviour
{
    [SerializeField] private eventIcon icon;
    [SerializeField] private PlayableDirector cutscene;

    [SerializeField] private string nextScene;

    [Header("Event Related")]
    [Tooltip("Do not use for repeatable conversations, also add event to MenuScript so it resets on NewGame")]
    public string eventName;

    void Start()
    {
        // testing purposes if event has already been triggered
        //PlayerPrefs.SetInt(eventName,1);
        eventNameChecker();
    }


    void Update()
    {
        
    }

    public void beginCutsceneTimeline()
    {
        icon.changeActiveState();
        cutscene.Play();
        PlayerPrefs.SetInt(eventName, 1);                   // now it is triggered
    }

    private void eventNameChecker()                         // checks whether a one-time event should be played or not
    {
        if (eventName != null)
        {
            if (PlayerPrefs.GetInt(eventName, 0) == 1)      // has the named event already been triggered?
            {
                gameObject.SetActive(false);                // disables event trigger
                icon.changeActiveState();                   // disables floating icon
            }
        }
    }
}
