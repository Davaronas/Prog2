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
    [SerializeField] private Text perkTooltip_cost = null;
    [SerializeField] private Vector3 perkTooltip_MinusY = Vector3.zero;
    [Space]
    [SerializeField] private RectTransform weaponTooltip = null;
    [SerializeField] private Text weaponTooltip_name = null;
    [SerializeField] private Text weaponTooltip_cost = null;
    [SerializeField] private Image weaponTooltip_rechargeRate = null;
    [SerializeField] private Image weaponTooltip_ammoUsage = null;
    [SerializeField] private Image weaponTooltip_fireRate = null;
    [SerializeField] private Image weaponTooltip_projectileSpeed = null;
    [SerializeField] private Image weaponTooltip_damage = null;
    [SerializeField] private Vector3 weaponTooltip_MinusY = Vector3.zero;
    [SerializeField] private BuyConfirmPanel buyConfirmPanel = null;
    [Space]
    [SerializeField] private RectTransform simpleTooltip = null;
    [SerializeField] private Text simpleTooltipText = null;
    [SerializeField] private Vector3 simpleTooltip_MinusY = Vector3.zero;


    public Action OnShopOpened;
    public Action OnShopClosed;


   [HideInInspector]  public bool isOpened = false;


   private bool isPerkTooltipActive = false;
    private bool isWeaponTooltipActive = false;
    private bool isSimpleTooltipActive = false;


    WeaponData.WeaponInfo weaponInfo_;




    private void Start()
    {
        shopPanel.SetActive(false);
        perkTooltip.gameObject.SetActive(false);

        buyConfirmPanel = FindObjectOfType<BuyConfirmPanel>();
        buyConfirmPanel.gameObject.SetActive(false);



        Perk.OnPerkHoverGlobal += OnPerkHoverCallback;
        Perk.OnPerkHoverEndGlobal += OnPerkHoverEndCallback;

        InventoryWeapon.OnWeaponHoverGlobal += OnWeaponHoverCallback;
        InventoryWeapon.OnWeaponHoverEndGlobal += OnWeaponHoverEndCallback;

       Perk.OnSimpleHoverGlobal += OnSimpleHoverCallback;
        Perk.OnSimpleHoverEndGlobal += OnSimpleHoverEndCallback;
    }

    private void OnDestroy()
    {
        Perk.OnPerkHoverGlobal -= OnPerkHoverCallback;
        Perk.OnPerkHoverEndGlobal -= OnPerkHoverEndCallback;

        InventoryWeapon.OnWeaponHoverGlobal -= OnWeaponHoverCallback;
        InventoryWeapon.OnWeaponHoverEndGlobal -= OnWeaponHoverEndCallback;

        Perk.OnSimpleHoverGlobal -= OnSimpleHoverCallback;
        Perk.OnSimpleHoverEndGlobal -= OnSimpleHoverEndCallback;
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
        if (isPerkTooltipActive)
        {
            perkTooltip.position = Input.mousePosition + perkTooltip_MinusY;
        }

        if (isWeaponTooltipActive)
        {
            weaponTooltip.position = Input.mousePosition + weaponTooltip_MinusY;
        }

        if (isSimpleTooltipActive)
        {
            simpleTooltip.position = Input.mousePosition + simpleTooltip_MinusY;
        }
    }


    private void OnPerkHoverCallback(string _n, string _c, string _co)
    {
        perkTooltip_name.text = _n;
        perkTooltip_content.text = _c;
        perkTooltip_cost.text = "Cost: " + _co;
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


    private void OnSimpleHoverCallback(string _text)
    {
        print("callback");

        simpleTooltipText.text = _text;
        simpleTooltip.gameObject.SetActive(true);
        isSimpleTooltipActive = true;
    }

    private void OnSimpleHoverEndCallback()
    {
        simpleTooltip.gameObject.SetActive(false);
    }

}
