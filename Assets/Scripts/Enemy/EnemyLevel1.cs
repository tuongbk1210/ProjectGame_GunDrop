using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevel1 : MonoBehaviour, IEnemy

{
    public enum EmenyState
    {
        Idle,
        Patrol,
        Chase,
        Hurt,
        Dead
    }

    public enum WeaponType
    {
        Knife,
        Pistol,
        AK,
        Shotgun
    }

    public EmenyState currentState;
    public WeaponType weaponType;

    public Transform player;
    [SerializeField]
    float speed = 0.5f;
    public BoxCollider2D knifeCollider;
    private AnimationController animationController;
    private Rigidbody2D rg;

    public LayerMask obstacleLayer;
    public float avoidDistance = 0.6f;

    EmenyLevel1Attack enemyLevel1Attack;

    [Header("Move")]
    [SerializeField]
    float moveSpeed = 3f;
    [SerializeField]
    float moveSpeedKnife = 4f;

    [Header("Detect")]
    [SerializeField]
    float detectRadius = 6f;
    [SerializeField]
    float detectAttack = 4f;
    //[SerializeField]
    //float detectHandGunAttack = 2f;

    [Header("Patrol")]
    [SerializeField]
    Vector2 location_1 = new Vector2(-2, 15);
    [SerializeField]
    Vector2 location_2 = new Vector2(2.8f, 14.3f);
    Vector2 target;

    void Start()
    {
        currentState = EmenyState.Patrol;
        animationController = GetComponent<AnimationController>();
        rg = GetComponent<Rigidbody2D>();
        enemyLevel1Attack = GetComponent<EmenyLevel1Attack>();
        transform.position = location_1;
        target = location_2;
        if(weaponType == WeaponType.Knife)
        {
            knifeCollider.enabled = true;
        }
        else
        {
            knifeCollider.enabled = false;
        }
    }

    void Update()
    {
        switch (currentState)
        {
            case EmenyState.Idle:
                Idle(); break;
            case EmenyState.Patrol:
                Patrol(); break;
            case EmenyState.Chase:
                Chase(); break;
            case EmenyState.Hurt:
                Hurt(); break;
            case EmenyState.Dead:
                Dead(); break;
        }

        float distance = Vector2.Distance(transform.position, player.position);
        if (player.gameObject.layer != gameObject.layer)
        {
            currentState = EmenyState.Idle;
        }
        if (distance <= detectRadius && player.gameObject.layer == gameObject.layer)
        {
            currentState = EmenyState.Chase;
            
            if (distance <= detectAttack)
            {
                if (distance <= 1f && weaponType == WeaponType.Knife)
                {
                    enemyLevel1Attack.KnifeAttack();
                } else if(distance > 1f && weaponType == WeaponType.Knife)
                {
                    currentState = EmenyState.Chase;
                    animationController.Attack(false);
                }else
                {
                    Attack();
                    animationController.Attack(true);
                }
            }
            else
            {
                animationController.Attack(false);
                currentState = EmenyState.Idle;
            }
        }
        else
        {
            currentState = EmenyState.Patrol;
        }
    }

    public void Idle()
    {
        animationController.Move(0.5f);
    }


    public void Patrol()
    {
        Vector2 newPos = Vector2.MoveTowards(rg.position, target, moveSpeed * Time.deltaTime);
        Vector2 direction = target - (Vector2)rg.position;
        rg.MovePosition(newPos);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        if (Vector2.Distance(rg.position, target) < 0.1f)
        {
            target = (target == location_1) ? location_2 : location_1;
        }
        animationController.Move(speed);
    }
    public void Chase()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, avoidDistance, obstacleLayer);
        if (hit.collider != null)
        {
            direction = Vector2.Perpendicular(direction);
        }
        Vector2 newPos;
        if (weaponType == WeaponType.Knife)
        {
            newPos = rg.position + direction * moveSpeedKnife * Time.deltaTime;
        }
        else
        {
            newPos = rg.position + direction * moveSpeed * Time.deltaTime;
        }

        rg.MovePosition(newPos);
        //xoay
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    public void Hurt() { }


    void Attack()
    {
        switch (weaponType)
        {
            //case WeaponType.Knife:
            //    enemyLevel1Attack.KnifeAttack();
            //    break;
            case WeaponType.Pistol:
                enemyLevel1Attack.HandGunAttack();
                break;
            case WeaponType.AK:
                enemyLevel1Attack.AKAttack();
                break;
            case WeaponType.Shotgun:
                enemyLevel1Attack.ShotGunAttack();
                break;
        }

    }
    public void Dead() { }
}
