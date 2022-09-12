using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MinigameSFX : MonoBehaviour
{
    AudioSource mgAudioSource;
    public AudioClip[] audioClips;
    // Start is called before the first frame update
    void Start()
    {
        mgAudioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(int songNum)
    {
        if (audioClips == null)
        {
            mgAudioSource.PlayOneShot(audioClips[songNum]);
        }
        else
        {
            Debug.Log("Audio clips is empty");
        }
    }
}
