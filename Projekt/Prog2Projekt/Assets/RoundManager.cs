using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    private EnemyManager enemyManager = null;

    [SerializeField] private int roundNumber = 1;
   
    void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
        enemyManager.SpawnEnemies(roundNumber * 5, Mathf.CeilToInt(roundNumber/3));
    }

   
}
