using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    AudioClip bgMusic;

    SoundController soudController;
    void Start()
    {
        soudController = GetComponent<SoundController>();
        soudController.PlayMusic(bgMusic);
    }

    void Update()
    {
        
    }
}
