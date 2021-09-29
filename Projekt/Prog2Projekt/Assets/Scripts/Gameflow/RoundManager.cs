using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    private EnemyManager enemyManager = null;

    [SerializeField] private int roundNumber = 1;
    [SerializeField] private float timeDelay = 4f;

    private Shop shop = null;
   
    void Start()
    {
        shop = FindObjectOfType<Shop>();

        enemyManager = GetComponent<EnemyManager>();
        Invoke(nameof(StartRound), timeDelay);

    }

   

    private int GetPowerPoint(int _round)
    {
        return _round * 7;
    }

    private int GetPowerLevel(int _round)
    {
        return Mathf.CeilToInt((float)_round / 5);
    }

    public void RoundEnded()
    {
        roundNumber++;
        Invoke(nameof(OpenShop), timeDelay);
    }


    private void OpenShop()
    {
        shop.OpenShop();
    }

    public void RemoteCall_RoundStart()
    {
        shop.CloseShop();
        Invoke(nameof(StartRound), timeDelay);
    }

    private void StartRound()
    {
        enemyManager.SpawnEnemies(GetPowerPoint(roundNumber), GetPowerLevel(roundNumber));
    }
}
