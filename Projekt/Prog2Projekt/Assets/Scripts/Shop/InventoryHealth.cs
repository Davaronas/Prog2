using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class InventoryHealth : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private int healthAmount;
    [SerializeField] private int price;

    private PlayerResources playerResources = null;
    private RoundManager roundManager = null;




    private int currentPrice_ = 0;
    private string tooltipText_ = "";

    private void Start()
    {
        playerResources = FindObjectOfType<PlayerResources>();
        roundManager = FindObjectOfType<RoundManager>();
    }

    public void RemoteCall_BuyHealth()
    {
        currentPrice_ = price + roundManager.GetCurrentWaveNumber();

        if (playerResources.GetMoney() >= currentPrice_)
        {
            if (!playerResources.IsFullHealth())
            {
                playerResources.ChangeHealth(healthAmount);
                playerResources.ChangeMoney(-currentPrice_);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {


        currentPrice_ = price + roundManager.GetCurrentWaveNumber();
        tooltipText_ = $"Gives {healthAmount} health for {currentPrice_} credits";
        Perk.OnSimpleHoverGlobal?.Invoke(tooltipText_);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Perk.OnSimpleHoverEndGlobal?.Invoke();
    }
}
