using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();    
    }

    public void Idle(int typeWeapon)
    {
        animator.SetInteger("typeWeapon", typeWeapon);
    }
    public void Move(float speed)
    {
        animator.SetFloat("speed", speed);
    }

    public void Attack(bool shoot)
    {
        animator.SetBool("shoot", shoot);
    }

    public void PlayerAttack()
    {
        animator.SetTrigger("attack");
    }
    public void Dead()
    {
        animator.SetBool("dead", true);
    }
}
