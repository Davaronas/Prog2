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

    private bool enableFollowingPlayer = false;
    private float stoppingDistanceAfterSpawning = 0.5f;

    private Vector3 firstMovePosition = Vector3.zero;

    private float originalStoppingDistance = 0;

    public void Initialize(Vector3 _firstMovementPosition)
    {

        player = FindObjectOfType<PlayerMovement>();
       
        enemyBehaviour = GetComponent<EnemyBehaviour>();

        enableFollowingPlayer = false;
        navMeshAgent = GetComponent<NavMeshAgent>();
        firstMovePosition = _firstMovementPosition;
        navMeshAgent.destination = firstMovePosition;
        originalStoppingDistance = navMeshAgent.stoppingDistance;
        navMeshAgent.stoppingDistance = stoppingDistanceAfterSpawning;
     //   transform.position = firstMovePosition;

    }


    

   
    





    private void FixedUpdate()
    {
        if(navMeshAgent == null) 
        {
            return; 
        }


        if (enableFollowingPlayer)
        {
            
            navMeshAgent.SetDestination(player.transform.position);

            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                enemyBehaviour.Attack(player.transform.position, player.GetVelocity());

            }

            playerPos_noY_ = player.transform.position;
            playerPos_noY_.y = 0;


            if (alwaysTurnToPlayer)
            {
                transform.LookAt(playerPos_noY_);
            }
        }
        else // check if we reached our first destination after spawning
        {
            // print();
         //   print(navMeshAgent.SetDestination(firstMovePosition));
           
            if (navMeshAgent.hasPath)
            {
               // print(navMeshAgent.remainingDistance + " " + stoppingDistanceAfterSpawning + " " + (navMeshAgent.remainingDistance <= stoppingDistanceAfterSpawning));
                if (navMeshAgent.remainingDistance <= stoppingDistanceAfterSpawning)
                {
                    enableFollowingPlayer = true;
                    navMeshAgent.stoppingDistance = originalStoppingDistance;
                }
            }

        }
    }
}
