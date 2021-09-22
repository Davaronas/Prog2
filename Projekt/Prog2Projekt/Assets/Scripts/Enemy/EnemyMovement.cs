using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyBehaviour))]
public class EnemyMovement : MonoBehaviour
{

    [SerializeField] private bool alwaysTurnToPlayer = false;

    private PlayerMovement player = null;
    private NavMeshAgent navMeshAgent = null;
    private EnemyBehaviour enemyBehaviour = null;

    private Vector3 playerPos_noY_ = Vector3.zero;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyBehaviour = GetComponent<EnemyBehaviour>();
    }



    private void FixedUpdate()
    {
        navMeshAgent.SetDestination(player.transform.position);

        if(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            enemyBehaviour.Attack(player.transform.position,player.GetVelocity());

         
        }

        playerPos_noY_ = player.transform.position;
        playerPos_noY_.y = 0;


        if (alwaysTurnToPlayer)
        {
            transform.LookAt(playerPos_noY_);
        }
    }
}
