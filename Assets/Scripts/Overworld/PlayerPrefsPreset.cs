using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsPreset : MonoBehaviour
{
    [SerializeField] private string[] EventsSetToTriggered;

    void Start()
    {
        PlayerPrefs.DeleteAll();

        if (EventsSetToTriggered != null)                 // are there any previous events to check?
        {
            foreach (string i in EventsSetToTriggered)    // iterates through the list
            {
                PlayerPrefs.SetInt(i, 1);                 // sets events to triggered
            }
        }
    }
}
