using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryGrenade : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private int price = 80;

    private PlayerEquipment playerEquipment = null;
    private PlayerResources playerResources = null;



    private void Start()
    {
        playerEquipment = FindObjectOfType<PlayerEquipment>();
        playerResources = FindObjectOfType<PlayerResources>();
    }

    public void RemoteCall_BuyGrenade()
    {
        if (playerResources.GetMoney() >= price)
        {
            if (!playerEquipment.AreGrenadesFull())
            {
                playerEquipment.AddGrenade();
                playerResources.ChangeMoney(-price);
            }
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        Perk.OnSimpleHoverGlobal?.Invoke($"Gives 1 grenade for {price} credits");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Perk.OnSimpleHoverEndGlobal?.Invoke();
    }
}
