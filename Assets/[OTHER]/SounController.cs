using System;
using System.Collections;
using UnityEngine;

public class SounController : MonoBehaviour
{
    public AudioClip[] clips;
    private AudioSource audioSource;
    private float actualCliplenght = 0;

    private bool ResetAfterPlay;
    private bool IsPlayingSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public AudioClip GetSound(int index)
    {
        return clips[index];
    }
    
    public AudioClip GetRandomSound()
    {
        return clips[UnityEngine.Random.Range(0, clips.Length)];
    }
    
    public void PlaySound(AudioClip clip)
    {
        
        actualCliplenght = clip.length;
        
        if(audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        audioSource.PlayOneShot(clip);
        StartCoroutine(WaitForClipEnd());
    }


    private void Reset()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.spatialBlend = 1;
        audioSource.playOnAwake = false;
    }

    private IEnumerator WaitForClipEnd()
    {
        IsPlayingSound = true;  
        yield return new WaitForSeconds(actualCliplenght);
        actualCliplenght = 0;
        IsPlayingSound = false;
    }
    
    public bool IsPlaying()
    {
        return IsPlayingSound;
    }
}
