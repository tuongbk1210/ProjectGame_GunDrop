using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePausemanager : MonoBehaviour
{
    public GameObject pauseGame;
    public GameObject playGame;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        if (pauseGame != null)
        {
            pauseGame.SetActive(false);
            playGame.SetActive(true);
        }
    }

    public void PlayGame()
    {
        Time.timeScale = 1f;
        if (pauseGame != null)
        {
            pauseGame.SetActive(true);
            playGame.SetActive(false);
        }
    }
}
