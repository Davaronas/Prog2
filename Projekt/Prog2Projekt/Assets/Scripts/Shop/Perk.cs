using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perk : MonoBehaviour
{
    public static class PerkData
    {
        public static int Endurance1_DamageReduction = 15;
        public static int Endurance2_DamageReduction = 33;
        public static int Endurance3_DamageReduction = 50;
        public static int Endurance4_DamageReduction = 66;
        public static int Endurance5_DamageReduction = 80;

        public static int Ammunition1_AmmoSpareChance = 15;
        public static int Ammunition2_AmmoPickedUpPlus = 20;
        public static int Ammunition3_AmmoSpareChance = 30;
        public static int Ammunition4_AmmoPickedUpPlus = 40;
        public static int Ammunition5_AmmoSpareChance = 45;


        public static int Mobility2_MovementSpeed = 1;
        public static int Mobility4_MovementSpeed = 2;
    }


    


    public enum PerkType { Endurance, Ammunition, Mobility}
    [SerializeField] private PerkType perkType = PerkType.Endurance;
    [SerializeField] [Range(1, 5)] private int perkLevel = 1;
    [SerializeField] private int cost = 100;

    private delegate void AttachedMethodDelegate(int _amount);
    private AttachedMethodDelegate attachedMethod;
    private int invokeValue = 0;



    private PlayerModifiers playerModifers = null;
    private PlayerMovement playerMovement = null;

    void Start()
    {
        playerModifers = FindObjectOfType<PlayerModifiers>();
        playerMovement = FindObjectOfType<PlayerMovement>();

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

    public void RemoteCall_BuyPerk()
    {
        // money check

        // are you sure check ?

        attachedMethod.Invoke(invokeValue);
    }
}
