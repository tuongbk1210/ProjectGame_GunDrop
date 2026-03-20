using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPointDamage : MonoBehaviour
{
    public int damage = 50;
    private bool isAttacking = false;
    private bool hasHit = false;

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

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!isAttacking) return;
        if (hasHit) return;

        string ownerTag = transform.root.tag;

        if (other.gameObject.layer != gameObject.layer) return;

        if (ownerTag == "Enemies" && other.CompareTag("Player"))
        {
            other.GetComponentInParent<PlayerHealth>()?.TakeDamage(damage);
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
            hasHit = true;
        }
    }
}
