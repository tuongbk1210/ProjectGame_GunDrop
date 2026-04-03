using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healitem : MonoBehaviour
{
    [SerializeField]
    AudioClip pickItem;
    SoundController soundController;

    public int healAmount = 50;

    private void Start()
    {
        soundController = GetComponent<SoundController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth player = collision.GetComponent<PlayerHealth>();
            if(player != null)
            {
                player.Heal(healAmount);
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Collider2D>().enabled = false;
                soundController.PlayAudio(pickItem);
            }
            Destroy(gameObject,1f);
        }
    }
}
