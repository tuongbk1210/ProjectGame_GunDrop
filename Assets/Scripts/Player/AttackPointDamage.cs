using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPointDamage : MonoBehaviour
{
    public int damage = 50;
    private bool isAttacking = false;
    private bool hasHit = false;

    SpriteRenderer sr;
    Transform parentTransform;


    public void StartAttack()
    {
        isAttacking = true;
        hasHit = false;
        Invoke(nameof(EndAttackKnife), 0.2f);
    }

    public void EndAttackKnife()
    {
        isAttacking = false;
    }

    public void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        parentTransform = transform.root;
      
    }

    private void LateUpdate()
    {
        SyncWithParent();
    }

    private void SyncWithParent()
    {
        if (parentTransform == null) return;

        gameObject.layer = parentTransform.gameObject.layer;

        SpriteRenderer parentSR = parentTransform.GetComponent<SpriteRenderer>();
        if (sr != null && parentSR != null)
        {
            sr.sortingLayerID = parentSR.sortingLayerID;
            sr.sortingOrder = parentSR.sortingOrder + 1;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!isAttacking) return;
        if (hasHit) return;

        string ownerTag = transform.root.tag;

        if (other.gameObject.layer != gameObject.layer) return;

        if (ownerTag == "Enemies" && other.CompareTag("Player"))
        {
            other.GetComponentInParent<PlayerHealth>()?.TakeDamage(damage);
            var player = other.GetComponentInParent<PlayerHealth>();
            player?.Showblood();
            hasHit = true;
        }

        else if (ownerTag == "Player" && other.CompareTag("Enemies"))
        {
            EnemyLevel1 enemyLevel1 = other.GetComponentInParent<EnemyLevel1>();
            if (enemyLevel1 != null)
            {
                enemyLevel1.Hurt();
            }
            other.GetComponentInParent<HealthbarEnemy>()?.TakeDamage(damage);
            var enemy = other.GetComponentInParent<HealthbarEnemy>();
            enemy?.Showblood();
            hasHit = true;
        }
    }
}
