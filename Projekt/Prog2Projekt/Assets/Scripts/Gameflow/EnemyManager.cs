using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private float checkForEnemiesInterval = 2f;
    [Space]
    [SerializeField] private GameObject[] enemyTypes = null;

    private EnemySpawn[] enemySpawns = null;

    private RoundManager roundManager = null;


    private EnemyResources[] enemiesLeft_ = null;
    private int spawnIndex_ = 0;


    void Start()
    {
        roundManager = GetComponent<RoundManager>();
    }

    public void SpawnEnemies(int _powerPoint, int _powerLevel)
    {
        enemySpawns = FindObjectsOfType<EnemySpawn>();

      

            StartCoroutine(SpawnEnemies(CollectRandomEnemies(_powerPoint,_powerLevel), _powerPoint,_powerLevel));
        


       
    }


    IEnumerator SpawnEnemies(GameObject[] _enemies, int _powerPoint, int _powerLevel)
    {
        

        for (int j = 0; j < _enemies.Length; j++)
        {
            enemySpawns[spawnIndex_].SpawnEnemy(_enemies[j]);
            spawnIndex_++;
            if (spawnIndex_ == enemySpawns.Length)
            {
                spawnIndex_ = 0;
            }
            yield return new WaitForSeconds(spawnInterval);
        }

        StartCoroutine(CheckForEnemies());
    }
       
    


    private GameObject[] CollectRandomEnemies(int _powerPoint, int _powerLevel)
    {
        List<GameObject> _enemies = new List<GameObject>();
        int _randomNumber = 0;

        while(_powerPoint > 0)
        {
            roll:
            _randomNumber = Random.Range(0, enemyTypes.Length);
            EnemyResources _eR = enemyTypes[_randomNumber].GetComponent<EnemyResources>();

            if (_eR.powerLevel <= _powerLevel)
            {
                _enemies.Add(enemyTypes[_randomNumber]);
                _powerPoint -= _eR.powerPoint;
            }
            else
            {
                goto roll;
            }
        }



        return _enemies.ToArray();
    }


    

    
    void Update()
    {
        
    }

    IEnumerator CheckForEnemies()
    {
        enemiesLeft_ = FindObjectsOfType<EnemyResources>();

        while (enemiesLeft_.Length > 0)
        {
            yield return new WaitForSeconds(checkForEnemiesInterval);
            enemiesLeft_ = FindObjectsOfType<EnemyResources>();
        }

        roundManager.RoundEnded();
    }



}

