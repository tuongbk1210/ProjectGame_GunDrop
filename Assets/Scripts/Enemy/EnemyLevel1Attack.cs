using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevel1Attack : MonoBehaviour
{
    private AnimationController animationController;
    private SoundController soundController;

    Transform player;

    [Header("Damage")]
    public int damageKnife = 50;
    public int danmageHandGonNormal = 15;
    public int damageHandGon = 25;
    public int damageAk = 20;
    public int damageHandShot = 30;

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
    float detectAttack = 7f;
    [SerializeField]
    float detectHandGunAttack = 4f;

    PlayerHealth playerHealth;

    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    Transform gunPoint;
    [SerializeField]
    float bulletSpeed = 10f;

    private EnemyLevel1 enemy;
    public bool isDeal = false;

    [SerializeField]
    GameObject muzzlePrefab;

    void Start()
    {
        enemy = GetComponent<EnemyLevel1>();
        animationController = GetComponent<AnimationController>();
        soundController = GetComponent<SoundController>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public void KnifeAttack()
    {
        if (!isDeal)
        {
            animationController.PlayerAttack();
            //checkKnifeAttack = true;
            soundController.PlayAudio(audioClipKnifeAttack);
        }
    }
    public void HandGunAttack()
    {
        if (!isDeal)
        {
            FireBullet();
            float distance = Vector3.Distance(player.position, gameObject.transform.position);
            if (playerHealth != null)
            {
                if (distance <= detectAttack && distance > detectHandGunAttack)
                {
                    playerHealth.TakeDamage(danmageHandGonNormal);
                }
                else if (distance <= detectHandGunAttack)
                {
                    playerHealth.TakeDamage(damageHandGon);
                }
            }
            soundController.PlayAudio(audioClipHandGonAttack);
        }
    }

    public void AKAttack()
    {
        if (!isDeal)
        {
            FireBullet();
            soundController.PlayAudio(audioClipAkAttack);
        }
    }

    public void ShotGunAttack()
    {
        if (!isDeal)
        {
            FireBullet();
            soundController.PlayAudio(audioClipHandshotAttack);
        }
    }

    public void FireBullet()
    {
        GameObject vfx = Instantiate(muzzlePrefab, gunPoint.transform.position, gunPoint.transform.rotation);
        GameObject bullet = Instantiate(bulletPrefab, gunPoint.position, gunPoint.rotation);
        bullet.layer = gameObject.layer;
        vfx.layer = gameObject.layer;
        SpriteRenderer sr = bullet.GetComponent<SpriteRenderer>();
        SpriteRenderer vfxSr = vfx.GetComponent<SpriteRenderer>();
        SpriteRenderer ownerSr = GetComponent<SpriteRenderer>();
        if (sr != null && ownerSr != null)
        {
            sr.sortingLayerID = ownerSr.sortingLayerID;
            sr.sortingOrder = ownerSr.sortingOrder;
        }
        if (sr != null && ownerSr != null)
        {
            vfxSr.sortingLayerID = ownerSr.sortingLayerID;
            vfxSr.sortingOrder = ownerSr.sortingOrder;
        }
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = gunPoint.right * bulletSpeed;
        ManagerBullet mb = bullet.GetComponent<ManagerBullet>();
        mb.weaponTypeEnemy = enemy.weaponType;
    }
}
