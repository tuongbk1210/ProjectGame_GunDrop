using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    int maxHealth = 100;
    int currentHealth;

    public HealthBar healthBar;

    SoundController soundController;

    [SerializeField]
    AudioClip gameOver;

    [Header("UI")]
    public GameObject gameOverPanel;

    public GameObject bloodVfx;

    public void Start()
    {
        currentHealth = maxHealth;
        soundController = GetComponent<SoundController>();
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            soundController.PlayAudio(gameOver);
            Die();
        }
        healthBar.UpdateBar(currentHealth, maxHealth);
    }

    void Die()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Showblood()
    {
        if (bloodVfx != null)
        {
            bloodVfx.SetActive(true);
            StartCoroutine(DisableAfterTime(bloodVfx, 1f));
        }
    }

    IEnumerator DisableAfterTime(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        if (obj != null)
        {
            obj.SetActive(false);
        }

    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.UpdateBar(currentHealth, maxHealth);
    }
}


