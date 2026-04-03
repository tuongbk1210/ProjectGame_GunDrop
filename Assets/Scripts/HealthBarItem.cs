using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthBarItem : MonoBehaviour
{

    [SerializeField]
    int maxHealth = 120;
    int currentHealth;

    [Header("Explosion")]
    public GameObject explosionVFX;
    [Header("Drop")]
    public GameObject healItemPrefab;
    [Range(0, 1)]
    public float dropChange = 0.2f;

    [SerializeField]
    AudioClip explose;
    [SerializeField]
    AudioClip itemAppear;
    SoundController soundController;

    public void Start()
    {
        currentHealth = maxHealth;
        soundController = GetComponent<SoundController>();

    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Explose();
        }
    }

    void Explose()
    {
        if(explosionVFX != null)
        {
           GameObject vfx =  Instantiate(explosionVFX, transform.position, Quaternion.identity);
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            soundController.PlayAudio(explose);
            Destroy(vfx, 1f);
        }
        float rand = Random.value;
        if (rand <= dropChange)
        {
            Instantiate(healItemPrefab, transform.position, Quaternion.identity);
            soundController.PlayAudio(itemAppear);
        }
        Destroy(gameObject,1f);
    }
}
