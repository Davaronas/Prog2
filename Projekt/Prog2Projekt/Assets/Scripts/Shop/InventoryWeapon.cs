using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class InventoryWeapon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject heldWeaponGameObject = null;
    [SerializeField] private bool isAlreadyOwned = false;

    private PlayerEquipment playerEquipment = null;
    private Weapon heldWeapon = null;

    public static Action<Weapon> OnWeaponHoverGlobal;
    public static Action OnWeaponHoverEndGlobal;

    private void Start()
    {
        playerEquipment = FindObjectOfType<PlayerEquipment>();
        heldWeapon = heldWeaponGameObject.GetComponent<Weapon>();
    }

    public void RemoteCall_BuyWeapon()
    {
        playerEquipment.EquipWeapon(heldWeaponGameObject);
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
