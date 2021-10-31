using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RoundManager : MonoBehaviour
{

    [Space]
    [SerializeField] private int roundNumber = 1;
    [SerializeField] private float timeDelay = 4f;

    private EnemyManager enemyManager = null;
    private Shop shop = null;
    private WaveDisplay waveDisplay = null;
   
    void Start()
    {
        shop = FindObjectOfType<Shop>();
        waveDisplay = FindObjectOfType<WaveDisplay>();
       

        enemyManager = GetComponent<EnemyManager>();
     //  Invoke(nameof(FirstWaveDisplay), 1f); // ez azért kell mert az elsõ néhány frame-et nem szaggatva láthatjuk és a kiirás is rosszul fog kinézni
        //Invoke(nameof(StartRound), timeDelay);

    }

    private void FirstWaveDisplay()
    {
        waveDisplay.Display(roundNumber);
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
        waveDisplay.Display(roundNumber);
        Invoke(nameof(StartRound), timeDelay);
    }

    private void StartRound()
    {
       
        enemyManager.SpawnEnemies(GetPowerPoint(roundNumber), GetPowerLevel(roundNumber));
    }
}
