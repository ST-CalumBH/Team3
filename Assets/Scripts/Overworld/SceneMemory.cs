using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMemory : MonoBehaviour
{
    public static SceneMemory Instance { get; private set; }

    public string sceneName;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
