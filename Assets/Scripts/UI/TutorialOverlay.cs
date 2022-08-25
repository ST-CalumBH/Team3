using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
    public class TutorialOverlay : MonoBehaviour
    {
        [SerializeField] GameObject tutorialUI;

        // Start is called before the first frame update
        void Start()
        {
            Freeze();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameStart();
            }
        }

        void GameStart()
        {
            tutorialUI.SetActive(false);
            Time.timeScale = 1f;
        }

        void Freeze()
        {
            tutorialUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}