using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourAnimated : EnemyBehaviour
{
    [SerializeField] private Animator animator = null;

    private Vector3 targetPos = Vector3.zero;
    private Vector3 targetVelocity = Vector3.zero;

    public override void Attack(Vector3 _targetPos, Vector3 _targetVelocity)
    {
        if (canAttack)
        {

            animator.SetTrigger("Attack");
            targetPos = _targetPos;
            targetVelocity = _targetVelocity;
        }
        else
        {

            animator.SetBool("Move", false);
        }
    }

    public void RemoteCall_Attack()
    {
        FireProjectile(targetPos, targetVelocity);
        animator.ResetTrigger("Attack");
    }

}
