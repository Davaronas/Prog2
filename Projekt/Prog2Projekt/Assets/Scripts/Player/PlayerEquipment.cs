using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    [SerializeField] private Transform playerCam = null;
    [Space]
    [SerializeField] private GameObject defaultPistol = null;
    [SerializeField] private GameObject machinePistol = null;

    private PlayerResources playerResources = null;

    [Space]
   [SerializeField] private Weapon secondaryWeapon = null;
    [SerializeField] private Weapon mainWeapon = null;

    [SerializeField] private Weapon equippedWeapon = null;


    //  PLAYER RESOURCES START NEEDS TO RUN BEFORE PLAYER EQUIPMENT START (EquipSecondary)

    void Start()
    {
        playerResources = GetComponent<PlayerResources>();
        EquipSecondary(defaultPistol);

        

       // Invoke(nameof(TestMain), 5f);
    }


    private void TestMain()
    {
        EquipMain(machinePistol);
    }
    
    void Update()
    {
        
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
            playerResources.ChangeSecondaryAmmo(0,true); // csak ui frissitÚs miatt
            equippedWeapon = secondaryWeapon;
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
            playerResources.ChangeMainAmmo(0,true); // csak ui frissitÚs miatt
            equippedWeapon = mainWeapon;
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

        playerResources.ChangeMainAmmo(0,true); // csak ui frissitÚs miatt
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
 
        playerResources.ChangeSecondaryAmmo(0,true); // csak ui frissitÚs miatt
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
}
