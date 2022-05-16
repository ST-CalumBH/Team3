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

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void beginCutsceneTimeline()
    {
        icon.changeToActiveState();
        cutscene.Play();
    }
}
