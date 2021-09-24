using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHealth : MonoBehaviour
{
    [SerializeField] private int healthAmount;
    [SerializeField] private int price;

    private PlayerResources playerResources = null;

    private void Start()
    {
        playerResources = FindObjectOfType<PlayerResources>();
    }

    public void RemoteCall_BuyHealth()
    {
        playerResources.ChangeHealth(healthAmount);
    }
}
