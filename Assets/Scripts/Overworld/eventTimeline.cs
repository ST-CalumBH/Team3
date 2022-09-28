using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace Overworld {
    public class eventTimeline : MonoBehaviour
    {
        private enum eventType { onAwake, onTriggerEnter, onInteraction };  // onAwake has bugs atm

        [SerializeField] private eventIcon icon;                            // might use subscriber and publisher events to prevent dependancies
        [SerializeField] private PlayableDirector cutscene;

        private GameObject player;
        private playerController playerController;
        private bool inArea;

        [SerializeField] private string nextScene;                          // bruh I guess the scene transition collider script is doing this job

        [Header("Event Related")]
        [Tooltip("Do not use for repeatable conversations")]
        [SerializeField] private string eventName;
        [SerializeField] private eventType _type;
        [SerializeField] private string[] previousEventsRequired;

        private BoxCollider2D triggerArea;

        void Start()
        {
            player = GameObject.FindWithTag("Player");
            playerController = player.GetComponent<playerController>();

            if (TryGetComponent(out BoxCollider2D box))
            {
                triggerArea = box;
            }

            eventNameChecker();
        }

        void Update()
        {
            if (inArea && (Input.GetKeyDown(KeyCode.E)) && _type == eventType.onInteraction )                                    // event interactor
            {
                beginCutsceneTimeline();
            }
        }

        public void beginCutsceneTimeline()                             // might have to make event interactions based on this script
        {
            playerController.freezePlayer();
            icon.changeActiveState(false);
            cutscene.Play();
            PlayerPrefs.SetInt(eventName, 1);                           // now it is triggered
            if (triggerArea != null) { triggerArea.enabled = false; }
        }

        private void eventNameChecker()                         // checks whether a one-time event should be played or not
        {

            if (_type == eventType.onAwake)
            {
                icon.changeActiveState(false);
            }

            if (eventName != null)
            {
                if (PlayerPrefs.GetInt(eventName, 0) == 1)      // has the named event already been triggered? also the event icon shouldn't appear for onAwake and onTriggerEnter
                {
                    gameObject.SetActive(false);                // disables event trigger
                    icon.changeActiveState(false);              // disables floating icon
                    return;                                     // stops the code here
                }
            }
            
            if (previousEventsRequired != null)                 // are there any previous events to check?
            {
                foreach (string i in previousEventsRequired)    // iterates through the list
                {
                    if (PlayerPrefs.GetInt(i, 0) == 1)          // has the event been triggered?
                    {
                        gameObject.SetActive(false);            // if so, do not proceed with event and make it inactive
                        return;
                    }
                }
            }

            if (_type == eventType.onAwake)
            {
                beginCutsceneTimeline();

                playerController.freezePlayer();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                if (_type == eventType.onTriggerEnter)
                {
                    beginCutsceneTimeline();
                }

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
    }
}