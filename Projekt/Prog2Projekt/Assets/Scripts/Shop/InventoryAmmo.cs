using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class InventoryAmmo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private int ammoAmount;
    [SerializeField] private int price;

    private PlayerResources playerResources = null;
    private PlayerEquipment playerEquipment = null;
    private RoundManager roundManager = null;


    private int currentPrice_ = 0;
    private string tooltipText_ = "";

    private void Start()
    {
        playerResources = FindObjectOfType<PlayerResources>();
        playerEquipment = FindObjectOfType<PlayerEquipment>();
        roundManager = FindObjectOfType<RoundManager>();
    }

    public void RemoteCall_BuyAmmo()
    {
        currentPrice_ = price + roundManager.GetCurrentWaveNumber();

        if (playerResources.GetMoney() >= currentPrice_)
        {
            if (!playerResources.IsBothAmmoTypeFull())
            {
                playerEquipment.SwitchToMain();
                playerResources.ChangeSecondaryAmmo(50, false);
                playerResources.ChangeMainAmmo(ammoAmount, true);
                playerResources.ChangeMoney(-currentPrice_);
            }
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {


        currentPrice_ = price + roundManager.GetCurrentWaveNumber();
        tooltipText_ = $"Gives {ammoAmount} ammo for {currentPrice_} credits";
        Perk.OnSimpleHoverGlobal?.Invoke(tooltipText_);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Perk.OnSimpleHoverEndGlobal?.Invoke();
    }
}
