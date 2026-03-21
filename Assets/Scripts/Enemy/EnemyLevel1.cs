using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevel1 : MonoBehaviour, IEnemy

{
    public EmenyState currentState;
    public WeaponType weaponType;
    public MapLevel mapLevel;

    Transform player;
    private AnimationController animationController;
    private Rigidbody2D rg;

    public LayerMask obstacleLayer;
    public float avoidDistance = 0.6f;

    EnemyLevel1Attack enemyLevel1Attack;

    [Header("Move")]
    [SerializeField]
    float moveSpeed = 3f;
    [SerializeField]
    float moveSpeedKnife = 3.5f;

    [Header("Detect")]
    [SerializeField]
    float detectRadius = 7f;
    [SerializeField]
    float detectAttack = 4f;

    [Header("Patrol")]
    [SerializeField]

    public float attackCoooldown = 3f;
    private float lastAttackTime;

    [SerializeField]
    float keepDistance = 3f;

    public AttackPointDamage attackPointDamage;

    void Awake()
    {
        animationController = GetComponent<AnimationController>();
        rg = GetComponent<Rigidbody2D>();
        enemyLevel1Attack = GetComponent<EnemyLevel1Attack>();
    }

    void Start()
    {
        currentState = EmenyState.Idle;
        SetUPIdleAnimation();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ApplyMapLevel();
    }

    void Update()
    {
        switch (currentState)
        {
            case EmenyState.Idle:
                Idle(); break;
            case EmenyState.Chase:
                Chase(); break;
            case EmenyState.Hurt:
                Hurt(); break;
            case EmenyState.Dead:
                Dead(); break;
        }

        float distance = Vector2.Distance(transform.position, player.position);

        if (player == null || currentState == EmenyState.Dead) return;


        if (player.gameObject.layer != gameObject.layer)
        {
            currentState = EmenyState.Idle;
        }
        else if (distance <= detectRadius)
        {
            currentState = EmenyState.Chase;

            if (distance <= detectAttack)
            {
                if (distance <= 1f && weaponType == WeaponType.Knife)
                {
                    if (CanAttack())
                    {
                        lastAttackTime = Time.time;
                        enemyLevel1Attack.KnifeAttack();
                    }
                }
                else if (distance > 1f && weaponType == WeaponType.Knife)
                {
                    currentState = EmenyState.Chase;
                }
                else
                {
                    if (CanAttack())
                    {
                        lastAttackTime = Time.time;

                        Attack();
                        animationController.PlayerAttack();
                    }
                }
            }
            else
            {
                currentState = EmenyState.Chase;
            }
        }
        else
        {
            currentState = EmenyState.Idle;
        }
    }

    bool CanAttack()
    {
        return Time.time >= lastAttackTime + attackCoooldown;
    }


    public void SetUPIdleAnimation()
    {
        switch (weaponType)
        {
            case WeaponType.Knife:
                animationController.Idle(1);
                break;
            case WeaponType.Handgun:
                animationController.Idle(0);
                break;
            case WeaponType.AK:
                animationController.Idle(2);
                break;
            case WeaponType.Shotgun:
                animationController.Idle(3);
                break;
        }
    }
    public void Idle()
    {
        animationController.Move(0.5f);
    }

    public void Chase()
    {
        if (player == null) return;

        Vector2 toPlayer = player.position - transform.position;
        float distance = toPlayer.magnitude;
        Vector2 direction = toPlayer.normalized;

        Vector2 moveDir = Vector2.zero;

        WeaponManager playerWeapon = player.GetComponent<WeaponManager>();
        bool playerUseKnife = playerWeapon != null && playerWeapon.weaponType == WeaponManager.WeaponType.Knife;

        if (weaponType == WeaponType.Knife || playerUseKnife)
        {
            moveDir = direction;
        }
        else
        {
            float buffer = 0.2f;
            if (distance > keepDistance + buffer)
            {
                moveDir = direction;
            }
            else if (distance < keepDistance - buffer)
            {
                moveDir = -direction;
            }
            else
            {
                moveDir = Vector2.zero;
            }
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDir, avoidDistance, obstacleLayer);
        if (hit.collider != null)
        {
            moveDir = Vector2.Perpendicular(moveDir);
        }

        float speed = (weaponType == WeaponType.Knife) ? moveSpeedKnife : moveSpeed;
        Vector2 newPos = rg.position + moveDir * speed * Time.deltaTime;
        rg.MovePosition(newPos);
        //xoay veef mat player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    public void Hurt()
    {
        Invoke(nameof(RecoverFromHurt), 0.3f);
    }

    void RecoverFromHurt()
    {
        currentState = EmenyState.Chase;
    }

    void Attack()
    {
        switch (weaponType)
        {
            case WeaponType.Handgun:
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

    public void StartAttackKnife()
    {
        attackPointDamage.StartAttack();
    }

    public void EndAttackKnife()
    {
        attackPointDamage.EndAttackKnife();
    }
    public void Dead() {
        currentState = EmenyState.Dead;
        rg.velocity = Vector2.zero;
        enemyLevel1Attack.isDeal = true;
        Collider2D col = GetComponent<Collider2D>();
        if(col != null)
        {
            col.enabled = false;
        }
        rg.isKinematic = true;
        CancelInvoke();
        StopAllCoroutines();
    }

    void ApplyMapLevel()
    {
        string layerName = "";

        switch (mapLevel)
        {
            case MapLevel.Level1:
                layerName = "Layer 1";
                break;
            case MapLevel.Level2:
                layerName = "Layer 2";
                break;
            case MapLevel.Level3:
                layerName = "Layer 3";
                break;
        }

        int layerIndex = LayerMask.NameToLayer(layerName);
        if(layerIndex == -1)
        {
            Debug.Log("Layer không tồn tại" + layerName);
            return;
        }
        gameObject.layer = layerIndex;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if(sr != null)
        {
            sr.sortingLayerName = layerName;
        }
    }
}
