using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyBehaviour))]
public class EnemyMovement : MonoBehaviour
{

    [SerializeField] private bool alwaysFacePlayer = false;
    [SerializeField] private bool attackOnTheMove = true;

    private PlayerMovement player = null;
    protected NavMeshAgent navMeshAgent = null;
    private EnemyBehaviour enemyBehaviour = null;

    private Vector3 playerPos_noY_ = Vector3.zero;

    private bool enableFollowingPlayer = false;
    private float stoppingDistanceAfterSpawning = 0.5f;

    private Vector3 firstMovePosition = Vector3.zero;

    private float originalStoppingDistance = 0;
    protected bool inRange = false;

    private void Start()
    {
        //DEV
        //Initialize(transform.position + (Vector3.forward * 3));
    }

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
            ChasePlayer();
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
                    Invoke(nameof(EnablePlayerFollowing), Time.deltaTime * 5); // ez megoldja azt a probl�m�t hogy az ellenf�l a kezd�pont el�r�se ut�n egyb�l t�mad
                    navMeshAgent.stoppingDistance = originalStoppingDistance;
                    navMeshAgent.SetDestination(player.transform.position);
                }
            }

        }
    }

    private void EnablePlayerFollowing()
    {
        enableFollowingPlayer = true;
    }

    private void ChasePlayer()
    {
        navMeshAgent.SetDestination(player.transform.position);

        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            if (navMeshAgent.stoppingDistance <= stoppingDistanceAfterSpawning)
            {
                return;
            }

            enemyBehaviour.Attack(player.transform.position, player.GetVelocity());
            inRange = true;
        }
        else
        {
            inRange = false;
        }

        playerPos_noY_ = player.transform.position;
        playerPos_noY_.y = 0;


        if (alwaysFacePlayer)
        {
            transform.LookAt(playerPos_noY_);
        }
    }
}
