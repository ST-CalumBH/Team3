using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class existCheck : MonoBehaviour
{
    [SerializeField] private string[] previousEventsRequired;

    void Start()
    {
        eventNameChecker();
    }

    private void eventNameChecker()                         // checks whether a one-time event should be played or not
    {
        if (previousEventsRequired != null)                 // are there any previous events to check?
        {
            foreach (string i in previousEventsRequired)    // iterates through the list
            {
                if (PlayerPrefs.GetInt(i, 0) == 0)          // has the event NOT been triggered?
                {
                    gameObject.SetActive(false);            // if so, do not proceed with event and make it inactive
                    return;
                }
            }
        }
    }
}
