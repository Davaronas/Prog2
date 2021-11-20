using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerEquipment : MonoBehaviour
{
    [SerializeField] private Transform playerCam = null;
    [Space]
    [SerializeField] private GameObject defaultPistol = null;
    [Space]
    [SerializeField] private GameObject grenadePrefab = null;
    [SerializeField] private int baseGrenades = 3;
    [SerializeField] private Transform grenadeThrowPoint = null;
    [SerializeField] private float grenadeThrowForce = 100f;


    private PlayerResources playerResources = null;
    private PlayerUI playerUI = null;
    private int grenades = 0;
    private Weapon secondaryWeapon = null;
    private Weapon mainWeapon = null;
    private Weapon equippedWeapon = null;

    public Action<Transform, Transform> OnWeaponEquipped;


    private Shop shop = null;

    //  PLAYER RESOURCES START NEEDS TO RUN BEFORE PLAYER EQUIPMENT START (EquipSecondary)

    void Start()
    {
        playerResources = GetComponent<PlayerResources>();
        playerUI = GetComponent<PlayerUI>();
        shop = FindObjectOfType<Shop>();
        EquipSecondary(defaultPistol);

        shop.OnShopOpened += SwitchToMain; // váltsunk át a fõ fegyverünkre ha van, hogy lássuk mennyi lõszerünk van és kell e venni

        playerUI.Grenades(grenades, baseGrenades);
    }

    private void OnDestroy()
    {
        shop.OnShopOpened -= SwitchToMain;
    }


    public void SwitchToSecondary()
    {
        if(secondaryWeapon != null)
        {
            if(mainWeapon != null)
            {
                if(mainWeapon == equippedWeapon)
                {
                    mainWeapon.HideModel();
                    mainWeapon.isEquipped = false;
                }
            }

            secondaryWeapon.ShowModel();
            playerResources.ChangeSecondaryAmmo(0,true); // csak ui frissités miatt
            equippedWeapon = secondaryWeapon;
            equippedWeapon.isEquipped = true;

            OnWeaponEquipped?.Invoke(equippedWeapon.rightHandIkPos, equippedWeapon.leftHandIkPos);

        }
    }

    public void SwitchToMain()
    {
        if (mainWeapon != null)
        {
            if (secondaryWeapon != null)
            {
                if (secondaryWeapon == equippedWeapon)
                {
                    secondaryWeapon.HideModel();
                    secondaryWeapon.isEquipped = false;
                }
            }

            mainWeapon.ShowModel();
            playerResources.ChangeMainAmmo(0,true); // csak ui frissités miatt
            equippedWeapon = mainWeapon;
            equippedWeapon.isEquipped = true;

            OnWeaponEquipped?.Invoke(equippedWeapon.rightHandIkPos, equippedWeapon.leftHandIkPos);

        }
    }

    public void EquipWeapon(GameObject _weapon)
    {
        if(_weapon.GetComponent<Weapon>().isSecondary)
        {
            EquipSecondary(_weapon);
        }
        else
        {
            EquipMain(_weapon);
        }

       
    }


    public void EquipMain(GameObject _main)
    {
        if(mainWeapon != null)
        {
            Destroy(mainWeapon.gameObject);
        }

        if(secondaryWeapon == equippedWeapon)
        {
            secondaryWeapon.HideModel();
            secondaryWeapon.isEquipped = false;
        }

        mainWeapon = Instantiate(_main, transform.position, Quaternion.identity, playerCam).GetComponent<Weapon>();
        mainWeapon.transform.localPosition = mainWeapon.localPlayerPosOnCamera;
        mainWeapon.transform.localRotation = Quaternion.Euler(secondaryWeapon.localPlayerRotOnCameraEuler);

        equippedWeapon = mainWeapon;
        equippedWeapon.isEquipped = true;

        playerResources.ChangeMainAmmo(0,true); // csak ui frissités miatt

        OnWeaponEquipped?.Invoke(equippedWeapon.rightHandIkPos, equippedWeapon.leftHandIkPos);
       
    }

    private void EquipSecondary(GameObject _secondary)
    {
        if(secondaryWeapon != null)
        {
            Destroy(secondaryWeapon.gameObject);
        }

        if(mainWeapon != null)
        {
            if(mainWeapon == equippedWeapon)
            {
                mainWeapon.HideModel();
                mainWeapon.isEquipped = false;
            }
        }

        secondaryWeapon = Instantiate(_secondary, transform.position, Quaternion.identity, playerCam).GetComponent<Weapon>(); 
        secondaryWeapon.transform.localPosition = secondaryWeapon.localPlayerPosOnCamera;
        secondaryWeapon.transform.localRotation = Quaternion.Euler(secondaryWeapon.localPlayerRotOnCameraEuler);

        equippedWeapon = secondaryWeapon;
        equippedWeapon.isEquipped = true;
 
        playerResources.ChangeSecondaryAmmo(0,true); // csak ui frissités miatt

        OnWeaponEquipped?.Invoke(equippedWeapon.rightHandIkPos, equippedWeapon.leftHandIkPos);

    }

    public void FireEquippedWeapon()
    {
        if(equippedWeapon != null)
        {
            equippedWeapon.Fire();
        }
    }

    public bool IsSecondarySelected()
    {
        if(equippedWeapon == secondaryWeapon)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ThrowGrenade()
    {
        if(grenades <= 0) { return; }

        Rigidbody _rb = Instantiate(grenadePrefab, grenadeThrowPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        _rb.AddForce(playerCam.forward * grenadeThrowForce, ForceMode.VelocityChange);
        grenades--;

        playerUI.Grenades(grenades, baseGrenades);
    }



    public void AddGrenade()
    {
        if(grenades < baseGrenades )
        {
            grenades++;

            playerUI.Grenades(grenades, baseGrenades);
        }
    }

    public bool AreGrenadesFull()
    {
        if(grenades == baseGrenades)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
