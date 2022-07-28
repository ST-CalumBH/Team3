using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class eventTimeline : MonoBehaviour
{
    [SerializeField] private eventIcon icon;                            // might use subscriber and publisher events to prevent dependancies
    [SerializeField] private PlayableDirector cutscene;

    [SerializeField] private string nextScene;

    [Header("Event Related")]
    [Tooltip("Do not use for repeatable conversations")]
    [SerializeField] private string eventName;
    [SerializeField] private string[] previousEventsRequired;

    void Start()
    {
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
        if (previousEventsRequired != null)                 // are there any previous events to check?
        {
            foreach (string i in previousEventsRequired)    // iterates through the list
            {
                if (PlayerPrefs.GetInt(i, 0) == 0)          // has the event NOT been triggered?
                {
                    return;                                 // if not, do not proceed with event
                }
            }
        }

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
