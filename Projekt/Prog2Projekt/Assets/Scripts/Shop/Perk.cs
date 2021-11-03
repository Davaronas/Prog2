using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class Perk : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static class PerkData
    {
        public static int Endurance1_DamageReduction = 15;
        public static int Endurance2_DamageReduction = 33;
        public static int Endurance3_DamageReduction = 50;
        public static int Endurance4_DamageReduction = 66;
        public static int Endurance5_DamageReduction = 80;

        public static int Ammunition1_AmmoSpareChance = 20;
        public static int Ammunition2_AmmoPickedUpPlus = 20;
        public static int Ammunition3_AmmoSpareChance = 40;
        public static int Ammunition4_AmmoPickedUpPlus = 40;
        public static int Ammunition5_AmmoSpareChance = 60;


        public static int Mobility2_MovementSpeed = 2;
        public static int Mobility4_MovementSpeed = 4;


        public static string GetPerkName(PerkType _pt, int _l)
        {
            string _name = "";
            switch (_pt)
            {
                case PerkType.Endurance:
                    _name = "Endurance";
                    break;

                case PerkType.Ammunition:
                    _name = "Ammunition";
                    break;

                case PerkType.Mobility:
                    _name = "Mobility";
                    break;

            }

            switch (_l)
            {
                case 1:
                    _name += " I";
                    break;
                case 2:
                    _name += " II";
                    break;
                case 3:
                    _name += " III";
                    break;
                case 4:
                    _name += " IV";
                    break;
                case 5:
                    _name += " V";
                    break;
            }

            return _name;
        }

        public static string GetPerkContent(PerkType _pt, int _l)
        {
            string _content = "";
            switch (_pt)
            {
                case PerkType.Endurance:
                    switch (_l)
                    {
                        case 1:
                            _content = TooltipContent_Endurance1;
                            break;
                        case 2:
                            _content = TooltipContent_Endurance2;
                            break;
                        case 3:
                            _content = TooltipContent_Endurance3;
                            break;
                        case 4:
                            _content = TooltipContent_Endurance4;
                            break;
                        case 5:
                            _content = TooltipContent_Endurance5;
                            break;
                    }
                    break;

                case PerkType.Ammunition:
                    switch (_l)
                    {
                        case 1:
                            _content = TooltipContent_Ammunition1;
                            break;
                        case 2:
                            _content = TooltipContent_Ammunition2;
                            break;
                        case 3:
                            _content = TooltipContent_Ammunition3;
                            break;
                        case 4:
                            _content = TooltipContent_Ammunition4;
                            break;
                        case 5:
                            _content = TooltipContent_Ammunition5;
                            break;
                    }
                    break;

                case PerkType.Mobility:
                    switch (_l)
                    {
                        case 1:
                            _content = TooltipContent_Mobility1;
                            break;
                        case 2:
                            _content = TooltipContent_Mobility2;
                            break;
                        case 3:
                            _content = TooltipContent_Mobility3;
                            break;
                        case 4:
                            _content = TooltipContent_Mobility4;
                            break;
                        case 5:
                            _content = TooltipContent_Mobility5;
                            break;
                    }
                    break;

            }

           

            return _content;
        }

        public static string TooltipContent_Endurance1 = $"Gives {Endurance1_DamageReduction} percent damage reduction to all received damage.";
        public static string TooltipContent_Endurance2 = $"Gives {Endurance2_DamageReduction} percent damage reduction to all received damage.";
        public static string TooltipContent_Endurance3 = $"Gives {Endurance3_DamageReduction} percent damage reduction to all received damage.";
        public static string TooltipContent_Endurance4 = $"Gives {Endurance4_DamageReduction} percent damage reduction to all received damage.";
        public static string TooltipContent_Endurance5 = $"Gives {Endurance5_DamageReduction} percent damage reduction to all received damage.";

        public static string TooltipContent_Ammunition1 = $"Gives a {Ammunition1_AmmoSpareChance} percent chance to not use ammo when you fire your main weapon.";
        public static string TooltipContent_Ammunition2 = $"Every ammo you pick up or buy gives {Ammunition2_AmmoPickedUpPlus} percent more ammo.";
        public static string TooltipContent_Ammunition3 = $"Gives a {Ammunition3_AmmoSpareChance} percent chance to not use ammo when you fire your main weapon.";
        public static string TooltipContent_Ammunition4 = $"Every ammo you pick up or buy gives {Ammunition4_AmmoPickedUpPlus} percent more ammo.";
        public static string TooltipContent_Ammunition5 = $"Gives a {Ammunition5_AmmoSpareChance} percent chance to not use ammo when you fire your main weapon.";

        public static string TooltipContent_Mobility1 = "Actives your jetpack";
        public static string TooltipContent_Mobility2 = $"Gives {Mobility2_MovementSpeed} movement speed";
        public static string TooltipContent_Mobility3 = "You can dash a short distance";
        public static string TooltipContent_Mobility4 = $"Gives {Mobility4_MovementSpeed} movement speed";
        public static string TooltipContent_Mobility5 = "You can dash a short distance twice";
    }


    


    public enum PerkType { Endurance, Ammunition, Mobility}
    
    [SerializeField] private PerkType perkType = PerkType.Endurance;
    [SerializeField] [Range(1, 5)] private int perkLevel = 1;
    [SerializeField] private Image checkMark;
    [SerializeField] private int cost = 100;
    
    public bool isActive = false;
    [SerializeField] private Perk[] prerequisites = null;

    private delegate void AttachedMethodDelegate(int _amount);
    private AttachedMethodDelegate attachedMethod;
    private int invokeValue = 0;


    public static Action<string, string, string> OnPerkHoverGlobal;
    public static Action OnPerkHoverEndGlobal;

    public static Action<string> OnSimpleHoverGlobal;
    public static Action OnSimpleHoverEndGlobal;



    private PlayerModifiers playerModifers = null;
    private PlayerMovement playerMovement = null;
    private PlayerResources playerResources = null;

    private BuyConfirmPanel buyConfirmPanel = null;

    private void Awake()
    {

        // ezt awakebe kell megszerezni mert Srartban a Shop kikapcsolja és a FindObjectOfType nem fogja megtalálni ha a Shop start hamarabb fut le mint ez a start
        buyConfirmPanel = FindObjectOfType<BuyConfirmPanel>();
    }

    void Start()
    {
        playerModifers = FindObjectOfType<PlayerModifiers>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerResources = FindObjectOfType<PlayerResources>();
        

        switch(perkType)
        {
            case PerkType.Endurance:
                attachedMethod += playerModifers.SetDamageReduction;
                switch (perkLevel)
                {
                    case 1:
                        invokeValue = PerkData.Endurance1_DamageReduction;
                        break;
                    case 2:
                        invokeValue = PerkData.Endurance2_DamageReduction;
                        break;
                    case 3:
                        invokeValue = PerkData.Endurance3_DamageReduction;
                        break;
                    case 4:
                        invokeValue = PerkData.Endurance4_DamageReduction;
                        break;
                    case 5:
                        invokeValue = PerkData.Endurance5_DamageReduction;
                        break;

                }
                break;
            case PerkType.Ammunition:
                switch (perkLevel)
                {
                    case 1:
                        attachedMethod += playerModifers.SetChanceToNotUseAmmo;
                        invokeValue = PerkData.Ammunition1_AmmoSpareChance;
                        break;
                    case 2:
                        attachedMethod += playerModifers.SetPlusAmmoPickedUp;
                        invokeValue = PerkData.Ammunition2_AmmoPickedUpPlus;
                        break;
                    case 3:
                        attachedMethod += playerModifers.SetChanceToNotUseAmmo;
                        invokeValue = PerkData.Ammunition3_AmmoSpareChance;
                        break;
                    case 4:
                        attachedMethod += playerModifers.SetPlusAmmoPickedUp;
                        invokeValue = PerkData.Ammunition4_AmmoPickedUpPlus;
                        break;
                    case 5:
                        attachedMethod += playerModifers.SetChanceToNotUseAmmo;
                        invokeValue = PerkData.Ammunition5_AmmoSpareChance;
                        break;

                }
                break;
            case PerkType.Mobility:
                switch(perkLevel)
                {
                    case 1:
                        attachedMethod += playerMovement.EnableJetpack;
                        break;
                    case 2:
                        attachedMethod += playerModifers.SetMovementSpeedIncrease;
                        invokeValue = PerkData.Mobility2_MovementSpeed;
                        break;
                    case 3:
                        attachedMethod += playerMovement.EnableDash1;
                        break;
                    case 4:
                        attachedMethod += playerModifers.SetMovementSpeedIncrease;
                        invokeValue = PerkData.Mobility4_MovementSpeed;
                        break;
                    case 5:
                        attachedMethod += playerMovement.EnableDash2;
                        break;

                }
               
                break;
        }
        
    }

    void Update()
    {
        
    }

    public void RemoteCall_BuyCheck()
    {
        if (isActive) { return; }

        // prerequisites check
        for (int i = 0; i < prerequisites.Length; i++)
        {
            if (!prerequisites[i].isActive)
            {
                // show player that it cannot be purchased yet
                return;
            }
        }

        if(playerResources.GetMoney() < cost) { return; }

        buyConfirmPanel.SetItem(this, PerkData.GetPerkName(perkType, perkLevel), cost.ToString());

    }


    public void BuyPerk()
    {
        // vonjuk le a pénzt

        playerResources.ChangeMoney(-cost);
        isActive = true;
        checkMark.enabled = true;
        attachedMethod.Invoke(invokeValue);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnPerkHoverGlobal?.Invoke(PerkData.GetPerkName(perkType, perkLevel), PerkData.GetPerkContent(perkType, perkLevel), cost.ToString());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnPerkHoverEndGlobal?.Invoke();
    }
}
