using System.Collections;
using UnityEngine;
using TMPro;
using Overworld;

namespace Dialogue {
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] private GameObject dialogueBox;
        [SerializeField] private TMP_Text textLabel;

        private playerController player;

        public bool canProgress = true;
        public float dialogueDuration = 5f;
        public bool IsOpen { get; private set; }

        private bool cutsceneState = false;

        private TypewriterEffect typewriterEffect;

        private void Awake()
        {
            player = FindObjectOfType<playerController>();

            typewriterEffect = GetComponent<TypewriterEffect>();
            CloseDialogueBox();
        }

        public void ShowDialogue(DialogueObject dialogueObject)
        {
            IsOpen = true;
            dialogueBox.SetActive(true);
            StartCoroutine(StepThroughDialogue(dialogueObject));
        }

        private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
        {
            foreach (string dialogue in dialogueObject.Dialogue)
            {
                yield return RunTypingEffect(dialogue);

                textLabel.text = dialogue;

                yield return null;

                if (canProgress == true)
                {
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0));
                }
                else
                {
                    yield return new WaitForSeconds(dialogueDuration);
                }
            }

            CloseDialogueBox();
        }

        private IEnumerator RunTypingEffect(string dialogue)
        {
            typewriterEffect.Run(dialogue, textLabel);

            while (typewriterEffect.IsRunning)
            {
                yield return null;

                if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0)) && canProgress == true)
                {
                    typewriterEffect.Stop();
                }
            }
        }

        private void CloseDialogueBox()
        {
            IsOpen = false;
            dialogueBox.SetActive(false);
            textLabel.text = string.Empty;

            if (player != null && cutsceneState == false) { player.unfreezePlayer(); }
        }

        public void SwapProgressState(bool state)                                  // dialogue can swap from automatic to manual. true(1) = manual, false(0) = auto
        {
            canProgress = state;
        }

        public void DialogueLength(float duration)
        {
            dialogueDuration = duration;
        }

        public void CutsceneState(bool state)                                       // make sure to set this to true when a cutscene is happening and turn it off once the cutscene is done
        {
            cutsceneState = state;                                                  // eg. bool true - cutscene mode is true, meaning dialogue box does not unfreeze player and dialogue is false, and dialogue is automatic
            canProgress = !state;
        }

        public bool checkCutsceneState() { return cutsceneState; }
    }

}