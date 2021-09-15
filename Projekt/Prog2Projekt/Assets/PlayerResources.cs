using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    [SerializeField] private readonly int baseHealth = 100;
    [SerializeField] private readonly int baseMainAmmo = 100;
    [SerializeField] private readonly int baseSecondaryAmmo = 50;


    private int health = 0;
    private int mainAmmo = 0;
    private int secondaryAmmo = 0;


    void Start()
    {
        health = baseHealth;
        mainAmmo = baseMainAmmo;
        secondaryAmmo = baseSecondaryAmmo;
    }

    void Update()
    {
        
    }

    public void ChangeHealth(int _amount)
    {
        health = Mathf.Clamp(_amount,0, baseHealth);

        if(health <= 0)
        {
            // die
        }
    }
}
