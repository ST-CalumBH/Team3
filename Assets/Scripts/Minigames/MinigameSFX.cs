using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MinigameSFX : MonoBehaviour
{
    public AudioSource mgAudioSource;
    public AudioClip[] audioClips;
    // Start is called before the first frame update
    void Start()
    {
        mgAudioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(int songNum)
    {
        if (audioClips != null)
        {
            mgAudioSource.PlayOneShot(audioClips[songNum]);
        }
        else
        {
            Debug.Log("Audio clips is empty");
        }
    }

    public IEnumerator PlaySoundWait(int songNum, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (audioClips != null)
        {
            mgAudioSource.PlayOneShot(audioClips[songNum]);
        }
        else
        {
            Debug.Log("Audio clips is empty");
        }
    }
}
