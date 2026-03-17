using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmenyLevel1Attack : MonoBehaviour
{
    private AnimationController animationController;
    private SoundController soundController;

    [SerializeField]
    Transform player;

    [Header("Damage")]
    public int damageKnife = 50;
    public int danmageHandGonNormal = 15;
    public int damageHandGon = 25;
    public int damageAk = 20;
    public int damageHandShot = 30;

    private bool checkKnifeAttack = false;

    [Header("AudioClip")]
    [SerializeField]
    AudioClip audioClipKnifeAttack;
    [SerializeField]
    AudioClip audioClipHandGonAttack;
    [SerializeField]
    AudioClip audioClipAkAttack;
    [SerializeField]
    AudioClip audioClipHandshotAttack;

    [Header("Distance Attack")]
    [SerializeField]
    float detectAttack = 4f;
    [SerializeField]
    float detectHandGunAttack = 2f;

    PlayerHealth playerHealth;

    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    Transform gunPoint;
    [SerializeField]
    float bulletSpeed = 10f;

    void Start()
    {
        animationController = GetComponent<AnimationController>();
        soundController = GetComponent<SoundController>();
        playerHealth = GetComponent<PlayerHealth>();
    }
    public void KnifeAttack()
    {
        animationController.Attack(true);
        checkKnifeAttack = true;
        soundController.PlayAudio(audioClipKnifeAttack);
    }
    public void HandGunAttack()
    {
        fireBullet();
        float distance = Vector3.Distance(player.position, gameObject.transform.position);
        if (distance <= detectAttack && distance > detectHandGunAttack)
        {
            playerHealth.TakeDamage(danmageHandGonNormal);
        }
        else if (distance <= detectHandGunAttack)
        {
            playerHealth.TakeDamage(damageHandGon);
        }
        soundController.PlayAudio(audioClipHandGonAttack);
    }

    public void AKAttack()
    {
        fireBullet();
        playerHealth.TakeDamage(damageAk);
        soundController.PlayAudio(audioClipAkAttack);
    }

    public void ShotGunAttack()
    {
        fireBullet();
        playerHealth.TakeDamage(damageHandShot);
        soundController.PlayAudio(audioClipHandshotAttack);
    }

    public void fireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, gunPoint.position, gunPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = gunPoint.right * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player") && checkKnifeAttack)
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damageKnife);
        }
    }
}
