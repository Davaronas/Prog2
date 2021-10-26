using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementAnimated : EnemyMovement
{
    [SerializeField] private Animator animator = null;

    private void Update()
    {
        if(!inRange)
        {
            animator.SetBool("Move", true);
        }
    }
}
