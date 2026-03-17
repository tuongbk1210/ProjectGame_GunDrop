using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [Header("Audio Sources")]
    AudioSource audioSource;
    AudioSource bgMusicSource;

    public void PlayAudio(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    private void Awake()
    {
        bgMusicSource = GetComponent<AudioSource>();   
        audioSource =   GetComponent<AudioSource>();
    }
    public void PlayMusic(AudioClip clip)
    {
        bgMusicSource.clip = clip;
        bgMusicSource.Play();
    }

    public void StopMusic()
    {
        bgMusicSource.Stop();
    }
}
