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
        enemyManager.SpawnEnemies(GetPowerPoint(roundNumber), GetPowerLevel(roundNumber));
    }

   

    private int GetPowerPoint(int _round)
    {
        return _round * 7;
    }

    private int GetPowerLevel(int _round)
    {
        return Mathf.CeilToInt((float)_round / 5);
    }
}
