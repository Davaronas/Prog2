using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryWeapon : MonoBehaviour
{
    [SerializeField] private GameObject heldWeapon = null;
    [SerializeField] private int price = 200;
    [SerializeField] private bool isAlreadyOwned = false;

    private PlayerEquipment playerEquipment = null;

    private void Start()
    {
        playerEquipment = FindObjectOfType<PlayerEquipment>();
    }

    public void RemoteCall_BuyWeapon()
    {
        playerEquipment.EquipWeapon(heldWeapon);
    }

}
