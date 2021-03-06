using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


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
      //  Application.targetFrameRate = 60;


        shop = FindObjectOfType<Shop>();
        waveDisplay = FindObjectOfType<WaveDisplay>();
       

        enemyManager = GetComponent<EnemyManager>();
       Invoke(nameof(FirstWaveDisplay), 1f); // ez az?rt kell mert az els? n?h?ny frame-et nem szaggatva l?thatjuk ?s a kiir?s is rosszul fog kin?zni
       Invoke(nameof(StartRound), timeDelay);

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
        PlayerPrefs.SetInt("RecordWave", roundNumber - 1);
    }

    private void StartRound()
    {
       
        enemyManager.SpawnEnemies(GetPowerPoint(roundNumber), GetPowerLevel(roundNumber));
    }



    public int GetCurrentWaveNumber()
    {
        return roundNumber;
    }

    public void PlayerDied()
    {
        PlayerPrefs.SetInt("CurrentWave", roundNumber - 1);
        if(roundNumber - 1 >= PlayerPrefs.GetInt("RecordWave",0))
        {
            PlayerPrefs.SetInt("RecordWave", roundNumber - 1);
        }
        SceneManager.LoadScene(0);
    }
}
