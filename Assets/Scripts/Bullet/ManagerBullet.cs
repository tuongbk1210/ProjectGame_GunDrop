using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ManagerBullet : MonoBehaviour
{
    [Header("Damage")]
    public int damageKnife = 50;
    public int danmageHandGonNormal = 15;
    public int damageHandGun = 25;
    public int damageAk = 20;
    public int damageHandShot = 30;

    public float speed = 10f;

    Rigidbody2D rb;
    [SerializeField]
    AudioClip impactWall;
    SoundController soundController;

    Vector2 startPosition;
    public float maxDistance = 8f;

    public WeaponManager.WeaponType weaponType;
    public WeaponType weaponTypeEnemy;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        rb.velocity = transform.right * speed;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, rb.velocity);
        soundController = FindObjectOfType<SoundController>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(startPosition, transform.position);
        if (distance >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    int GetDamage()
    {
        float distance = Vector2.Distance(startPosition, transform.position);
        switch (weaponType)
        {
            case WeaponManager.WeaponType.Pistol:
                if (distance <= 4f)
                    return damageHandGun;
                else
                {
                    return danmageHandGonNormal;
                }
            case WeaponManager.WeaponType.Shotgun:
                return damageHandShot;

        }
        return 0;
    }

    int GetDamageEnemy()
    {
        float distance = Vector2.Distance(startPosition, transform.position);
       
        switch (weaponTypeEnemy)
        {
            case WeaponType.Handgun:
                if (distance <= 4f)
                    return damageHandGun;
                else
                {
                    return danmageHandGonNormal;
                }
            case WeaponType.Shotgun:
                return damageHandShot;
            case WeaponType.AK:
                return damageAk;
        }
        return 0;
    }


    public void SetLayer(SpriteRenderer owner)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sortingLayerID = owner.sortingLayerID;
        sr.sortingOrder = owner.sortingOrder;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth player = collision.GetComponent<PlayerHealth>();
        EnemyLevel1 enemyLevel1 = collision.GetComponent<EnemyLevel1>();

        if (collision.CompareTag("Player"))
        {
            if (player != null)
            {
                player.TakeDamage(GetDamageEnemy());
            }
            Destroy(gameObject);
        }
        if (collision.CompareTag("Enemies"))
        {
            if (enemyLevel1 != null)
            {
                enemyLevel1.Hurt();
            }
            HealthbarEnemy enemy = collision.GetComponent<HealthbarEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(GetDamage());
            }
            Destroy(gameObject);
        }
        if (collision.CompareTag("Wall"))
        {
            AudioSource.PlayClipAtPoint(impactWall, transform.position);
            rb.velocity = Vector2.zero;
            rb.gravityScale = 1;
            Destroy(gameObject);

        }
    }
}
