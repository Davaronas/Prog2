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
    private int grenades = 0;
    private Weapon secondaryWeapon = null;
    private Weapon mainWeapon = null;
    private Weapon equippedWeapon = null;

    public Action<Transform, Transform> OnWeaponEquipped;




    //  PLAYER RESOURCES START NEEDS TO RUN BEFORE PLAYER EQUIPMENT START (EquipSecondary)

    void Start()
    {
        playerResources = GetComponent<PlayerResources>();
        EquipSecondary(defaultPistol);
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
                }
            }

            secondaryWeapon.ShowModel();
            playerResources.ChangeSecondaryAmmo(0,true); // csak ui frissités miatt
            equippedWeapon = secondaryWeapon;

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
                }
            }

            mainWeapon.ShowModel();
            playerResources.ChangeMainAmmo(0,true); // csak ui frissités miatt
            equippedWeapon = mainWeapon;

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
        }

        mainWeapon = Instantiate(_main, transform.position, Quaternion.identity, playerCam).GetComponent<Weapon>();
        mainWeapon.transform.localPosition = mainWeapon.localPlayerPosOnCamera;
        mainWeapon.transform.localRotation = Quaternion.Euler(secondaryWeapon.localPlayerRotOnCameraEuler);

        equippedWeapon = mainWeapon;

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
            }
        }

        secondaryWeapon = Instantiate(_secondary, transform.position, Quaternion.identity, playerCam).GetComponent<Weapon>(); 
        secondaryWeapon.transform.localPosition = secondaryWeapon.localPlayerPosOnCamera;
        secondaryWeapon.transform.localRotation = Quaternion.Euler(secondaryWeapon.localPlayerRotOnCameraEuler);

        equippedWeapon = secondaryWeapon;
 
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
    }



    public void AddGrenade()
    {
        if(grenades < baseGrenades )
        {
            grenades++;
        }
    }
}
