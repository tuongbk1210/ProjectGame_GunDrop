using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarEnemy : MonoBehaviour
{
    [SerializeField]
    int maxHealth;
    int currentHealth;

    public HealthBar healthBar;

    AnimationController animationController;
    SoundController soundController;

    [SerializeField]
    AudioClip enemyDie;
    [SerializeField]
    GameObject healthbarEnemy;
    EnemyLevel1 enemyLevel1;
    bool isEnemyDie = false;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.UpdateBar(currentHealth, maxHealth);
        animationController = GetComponent<AnimationController>();
        soundController = GetComponent<SoundController>();
        enemyLevel1 = GetComponent<EnemyLevel1>();
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0 && !isEnemyDie)
        {
            isEnemyDie = true;
            currentHealth = 0;
            animationController.Move(0.5f);
            animationController.Dead();
            soundController.PlayAudio(enemyDie);
            healthbarEnemy.SetActive(false);
            GameManager.instance.AddScore(10);
            Destroy(gameObject, 3f);
            enemyLevel1.Dead();
        }
        healthBar.UpdateBar(currentHealth, maxHealth);
    }
}
