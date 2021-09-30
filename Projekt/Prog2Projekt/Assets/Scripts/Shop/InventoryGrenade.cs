using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGrenade : MonoBehaviour
{
    private PlayerEquipment playerEquipment = null;

    private void Start()
    {
        playerEquipment = FindObjectOfType<PlayerEquipment>();
    }

    public void RemoteCall_BuyGrenade()
    {
        playerEquipment.AddGrenade();
    }
}
