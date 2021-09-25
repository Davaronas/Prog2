using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;


[RequireComponent(typeof(EnemyResources))]
public class EnemyResourceDrop : MonoBehaviour
{
    [SerializeField][Range(0,100)] private int dropChanceMedkit = 1;
    [SerializeField] [Range(0, 100)] private int dropChanceAmmo = 1;
    [SerializeField] private int dropHealth = 5;
    [SerializeField] private int dropAmmo = 10;
    [Space]
    [SerializeField] private int moneyDrop = 20;
    [Space]
    [SerializeField] private GameObject medkit = null;
    [SerializeField] private GameObject ammo = null;


    private EnemyResources enemyResources = null;

    public static Action<int> OnEnemyDeathGlobal;

    private int randomNumber_ = 0;

    private void Start()
    {
        enemyResources = GetComponent<EnemyResources>();
        enemyResources.OnEnemyDeath += DropCheck;
    }

    private void OnDestroy()
    {
        enemyResources.OnEnemyDeath -= DropCheck;
    }


    private void DropCheck()
    {
        randomNumber_ = Random.Range(0, 201);
        print(randomNumber_ + " " + dropChanceMedkit + " " + (dropChanceMedkit + dropChanceAmmo));

        if (randomNumber_ <= dropChanceMedkit)
        {
            Medkit _medkit = Instantiate(medkit, transform.position + Vector3.up, Quaternion.identity).GetComponent<Medkit>();
            _medkit.SetAmount(dropHealth);
        }
        else if (randomNumber_ <= dropChanceMedkit + dropChanceAmmo)
        {
            Ammo _ammo = Instantiate(ammo, transform.position + Vector3.up, Quaternion.identity).GetComponent<Ammo>();
            _ammo.SetAmount(dropAmmo);
        }


        OnEnemyDeathGlobal?.Invoke(moneyDrop);
    }

}
