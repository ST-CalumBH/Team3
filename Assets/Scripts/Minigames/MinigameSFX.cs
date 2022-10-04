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

    public void PlaySound(int soundNum)
    {
        if (audioClips != null)
        {
            mgAudioSource.PlayOneShot(audioClips[soundNum]);
        }
        else
        {
            Debug.Log("Audio clips is empty");
        }
    }

    public void PlaySound(int soundNum, float vol)
    {
        if (audioClips != null)
        {
            mgAudioSource.PlayOneShot(audioClips[soundNum],vol);
        }
        else
        {
            Debug.Log("Audio clips is empty");
        }
    }

    public IEnumerator PlaySoundWait(int soundNum, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (audioClips != null)
        {
            mgAudioSource.PlayOneShot(audioClips[soundNum]);
        }
        else
        {
            Debug.Log("Audio clips is empty");
        }
    }
    public IEnumerator PlaySoundWait(int soundNum, float vol, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (audioClips != null)
        {
            mgAudioSource.PlayOneShot(audioClips[soundNum], vol);
        }
        else
        {
            Debug.Log("Audio clips is empty");
        }
    }
}
