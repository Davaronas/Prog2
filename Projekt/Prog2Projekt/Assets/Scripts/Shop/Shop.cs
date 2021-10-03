using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private Camera mainCamera = null;
    [SerializeField] private GameObject shopPanel = null;
    [Space]
    [SerializeField] private RectTransform perkTooltip = null;
    [SerializeField] private Text perkTooltip_name = null;
    [SerializeField] private Text perkTooltip_content = null;
    [SerializeField] private float perkTooltip_MinusY = 100f;
    [Space]
    [SerializeField] private RectTransform weaponTooltip = null;
    [SerializeField] private Text weaponTooltip_name = null;
    [SerializeField] private Text weaponTooltip_cost = null;
    [SerializeField] private Image weaponTooltip_rechargeRate = null;
    [SerializeField] private Image weaponTooltip_ammoUsage = null;
    [SerializeField] private Image weaponTooltip_fireRate = null;
    [SerializeField] private Image weaponTooltip_projectileSpeed = null;
    [SerializeField] private Image weaponTooltip_damage = null;
    [SerializeField] private float weaponTooltip_MinusY = 100f;


    public Action OnShopOpened;
    public Action OnShopClosed;


   [HideInInspector]  public bool isOpened = false;


   private bool isPerkTooltipActive = false;
    private bool isWeaponTooltipActive = false;


    private Vector3 minusTooltipVector_ = Vector3.zero;
    WeaponData.WeaponInfo weaponInfo_;



    private void Start()
    {
        shopPanel.SetActive(false);
        perkTooltip.gameObject.SetActive(false);
        minusTooltipVector_ = new Vector3(0, -perkTooltip_MinusY, 0);
        Perk.OnPerkHoverGlobal += OnPerkHoverCallback;
        Perk.OnPerkHoverEndGlobal += OnPerkHoverEndCallback;

        InventoryWeapon.OnWeaponHoverGlobal += OnWeaponHoverCallback;
        InventoryWeapon.OnWeaponHoverEndGlobal += OnWeaponHoverEndCallback;


    }

    private void OnDestroy()
    {
        Perk.OnPerkHoverGlobal -= OnPerkHoverCallback;
        Perk.OnPerkHoverEndGlobal -= OnPerkHoverEndCallback;

        InventoryWeapon.OnWeaponHoverGlobal -= OnWeaponHoverCallback;
        InventoryWeapon.OnWeaponHoverEndGlobal -= OnWeaponHoverEndCallback;
    }

    public void OpenShop() // round ends
    {
        shopPanel.SetActive(true);
        OnShopOpened?.Invoke();
        isOpened = true;
    }

    public void CloseShop() // round starts
    {
        shopPanel.SetActive(false);
        OnShopClosed?.Invoke();
        isOpened = false;
    }

    private void Update()
    {
        if(isPerkTooltipActive)
        {
            perkTooltip.position = Input.mousePosition + minusTooltipVector_;
        }

        if(isWeaponTooltipActive)
        {
            weaponTooltip.position = Input.mousePosition + minusTooltipVector_;
        }
    }


    private void OnPerkHoverCallback(string _n, string _c)
    {
        perkTooltip_name.text = _n;
        perkTooltip_content.text = _c;
        perkTooltip.gameObject.SetActive(true);
        isPerkTooltipActive = true;
    }

    private void OnPerkHoverEndCallback()
    {
        isPerkTooltipActive = false;
        perkTooltip.gameObject.SetActive(false);
    }


    private void OnWeaponHoverCallback(Weapon _w)
    {
        weaponTooltip_name.text = _w.weaponName;
        weaponInfo_ = WeaponData.GetSliderValues(_w);

        if (!_w.isSecondary)
        {
            weaponTooltip_rechargeRate.transform.parent.parent.gameObject.SetActive(false); // ez nem a legszebb
        }
        else
        {
            weaponTooltip_rechargeRate.transform.parent.parent.gameObject.SetActive(true); // ez nem a legszebb
            weaponTooltip_rechargeRate.fillAmount = weaponInfo_.rechargeRate;
        }

        weaponTooltip_ammoUsage.fillAmount = weaponInfo_.ammoUsage;
        weaponTooltip_projectileSpeed.fillAmount = weaponInfo_.projectileSpeed;
        weaponTooltip_damage.fillAmount = weaponInfo_.damage;
        weaponTooltip_fireRate.fillAmount = weaponInfo_.fireRate;
        weaponTooltip_cost.text = "Cost: " + _w.cost;


        weaponTooltip.gameObject.SetActive(true);
        isWeaponTooltipActive = true;
    }

    private void OnWeaponHoverEndCallback()
    {
        weaponTooltip.gameObject.SetActive(false);
    }

}
