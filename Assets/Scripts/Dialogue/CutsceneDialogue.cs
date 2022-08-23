using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue {
    public class CutsceneDialogue : MonoBehaviour
    {
        [SerializeField] private DialogueObject[] dialogue;
        [SerializeField] private float[] duration;

        private int indexDiag = 0;
        private bool state;

        private DialogueUI diagUI;

        public void Start()
        {
            diagUI = GameObject.FindObjectOfType<DialogueUI>();
        }

        public void PlayDialogue()
        {
            diagUI.DialogueLength(duration[indexDiag]);
            diagUI.ShowDialogue(dialogue[indexDiag]);
            indexDiag++;
        }

        public void ChangeProgress(int flag)
        {
            if (flag == 0) { state = false; }
            else { state = true; }

            diagUI.SwapProgressState(state);
        }
    }
}
