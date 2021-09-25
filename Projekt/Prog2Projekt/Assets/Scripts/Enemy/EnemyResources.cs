using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class EnemyResources : MonoBehaviour
{
    [SerializeField] private int baseHealth = 100;
    [Space]



    public Action OnEnemyDeath;
    



    private int health = 0;

    private HitBroadcast hitBroadcast;


    // Start is called before the first frame update
    void Start()
    {
        health = baseHealth;

        hitBroadcast = GetComponent<HitBroadcast>();
        if(hitBroadcast == null)
        {
            hitBroadcast = GetComponentInChildren<HitBroadcast>();
        }

        hitBroadcast.onHit += OnHitCallback;
    }

    private void OnDestroy()
    {
        hitBroadcast.onHit -= OnHitCallback;
    }

    private void OnHitCallback(int _health, Vector3 _pos)
    {
        ChangeHealth(_health);
        
    }

    public void ChangeHealth(int _amount)
    {
        health = Mathf.Clamp(health + _amount, 0, baseHealth);


        if (health <= 0)
        {

            OnEnemyDeath?.Invoke();

            Destroy(gameObject);
        }
    }


}
