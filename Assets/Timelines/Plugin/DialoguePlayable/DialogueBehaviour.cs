using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Dialogue {

    [Serializable]
    public class DialogueBehaviour : PlayableBehaviour
    {
        [SerializeField]
        public DialogueObject dialogue;

        [SerializeField]
        public float duration;

        [SerializeField]
        public DialogueUI diagUI;

        public void Start()
        {
            diagUI = GameObject.FindObjectOfType<DialogueUI>();

            diagUI.DialogueLength(duration);
            diagUI.SwapProgressState(false);

            diagUI.ShowDialogue(dialogue);
        }

        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
            var duration = playable.GetDuration();
            var count = playable.GetTime() + info.deltaTime;

            if ((info.effectivePlayState == PlayState.Paused && count > duration) || playable.GetGraph().GetRootPlayable(0).IsDone())
            {
                // Execute your finishing logic here:
                Debug.Log("Clip done!");
            }
        }
    }
}
