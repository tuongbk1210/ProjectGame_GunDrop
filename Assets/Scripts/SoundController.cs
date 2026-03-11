using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource audioSource;
    public AudioSource bgMusicSource;

    [Header("Sound Clips")]
    [SerializeField]
    AudioClip gunBullet;
    [SerializeField]
    AudioClip akBullet;
    [SerializeField]
    AudioClip knife;
    [SerializeField]
    AudioClip bgMusic;

    public void PlayAudio(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
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
