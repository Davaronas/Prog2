using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class InventoryWeapon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject heldWeaponGameObject = null;
    [SerializeField] private bool isAlreadyOwned = false;
    [SerializeField] private Image checkmark = null;

    private PlayerEquipment playerEquipment = null;
    private PlayerResources playerResources = null;
    private Weapon heldWeapon = null;

    public static Action<Weapon> OnWeaponHoverGlobal;
    public static Action OnWeaponHoverEndGlobal;

    private BuyConfirmPanel buyConfirmPanel = null;

    private void Awake()
    {
        buyConfirmPanel = FindObjectOfType<BuyConfirmPanel>();
    }

    private void Start()
    {
        playerEquipment = FindObjectOfType<PlayerEquipment>();
        heldWeapon = heldWeaponGameObject.GetComponent<Weapon>();
        playerResources = FindObjectOfType<PlayerResources>();

        if(isAlreadyOwned)
        {
            checkmark.enabled = true;
        }
    }

    public void RemoteCall_BuyCheck()
    {

        if(isAlreadyOwned)
        {
            playerEquipment.EquipWeapon(heldWeaponGameObject);
            return;
        }


        if(playerResources.GetMoney() < heldWeapon.cost) { return; }

        buyConfirmPanel.SetItem(this, heldWeapon.weaponName, heldWeapon.cost.ToString());
    }

    public void BuyWeapon()
    {
        playerResources.ChangeMoney(-heldWeapon.cost);
        playerEquipment.EquipWeapon(heldWeaponGameObject);
        isAlreadyOwned = true;
        checkmark.enabled = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnWeaponHoverGlobal?.Invoke(heldWeapon);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnWeaponHoverEndGlobal?.Invoke();
    }
}
