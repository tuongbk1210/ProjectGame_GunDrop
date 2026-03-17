using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPointDamage : MonoBehaviour
{
    public int damage = 50;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemies"))
        {
            HealthbarEnemy enemy = other.GetComponent<HealthbarEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
