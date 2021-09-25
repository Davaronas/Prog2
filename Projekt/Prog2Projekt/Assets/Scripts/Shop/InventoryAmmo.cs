using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryAmmo : MonoBehaviour
{
    [SerializeField] private int ammoAmount;
    [SerializeField] private int price;

    private PlayerResources playerResources = null;
    private PlayerEquipment playerEquipment = null;

    private void Start()
    {
        playerResources = FindObjectOfType<PlayerResources>();
        playerEquipment = FindObjectOfType<PlayerEquipment>();
    }

    public void RemoteCall_BuyAmmo()
    {
        playerEquipment.SwitchToMain();
        playerResources.ChangeSecondaryAmmo(50, false);
        playerResources.ChangeMainAmmo(ammoAmount,true);
    }
}
