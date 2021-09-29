using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerCamera))]
public class PlayerControl : MonoBehaviour
{
    private PlayerMovement playerMovement = null;
    private PlayerCamera playerCamera = null;
    private PlayerEquipment playerEquipment = null;
    private Shop shop = null;

    private int blockActions = 0;   // there will be possibly several things that block action at the same time, so this won't be just a boolean


    private  List<Controls.MovementDirections> movementDirections_ = new List<Controls.MovementDirections>();


    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerCamera = GetComponent<PlayerCamera>();
        playerEquipment = GetComponent<PlayerEquipment>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;


        shop = FindObjectOfType<Shop>();
        shop.OnShopOpened += ShopOpenedCallback;
        shop.OnShopClosed += ShopClosedCallback;
        
    }

    private void OnDestroy()
    {
        shop.OnShopOpened -= ShopOpenedCallback;
        shop.OnShopClosed -= ShopClosedCallback;
    }

    // Update is called once per frame
    void Update()
    {
        if (blockActions == 0)
        {
            Movement();
            Rotation();

            Jetpack();

            MainAttack();

            SwitchWeapon();
        }




    }

   
    private void ShopOpenedCallback()
    {
        blockActions++;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void ShopClosedCallback()
    {
        blockActions--;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }



    private void DEV_OpenShop()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if (shop.isOpened)
            {
                shop.CloseShop();
            }
            else
            {
                shop.OpenShop();
            }
        }
    }

    private void Movement()
    {
        movementDirections_.Clear();

        if (Input.GetKey(Controls.Forward))
        {
            movementDirections_.Add(Controls.MovementDirections.Forward);
        }

        if (Input.GetKey(Controls.Back))
        {
            movementDirections_.Add(Controls.MovementDirections.Back);
        }

        if (Input.GetKey(Controls.Left))
        {
            movementDirections_.Add(Controls.MovementDirections.Left);
        }

        if (Input.GetKey(Controls.Right))
        {
            movementDirections_.Add(Controls.MovementDirections.Right);
        }

        playerMovement.Move(movementDirections_.ToArray());
    }

    private void Rotation()
    {
        playerCamera.RotatePlayer(Input.GetAxis("Mouse X"));
        playerCamera.RotateCamera(Input.GetAxis("Mouse Y"));
    }


    private void Jetpack()
    {
        if(Input.GetKey(Controls.Jetpack))
        {
            playerMovement.UseJetpack();
        }

        if(Input.GetKeyUp(Controls.Jetpack))
        {
            playerMovement.EndJetpackUse();
        }
      
    }
    private void MainAttack()
    {
        if(Input.GetKey(Controls.MainAttack))
        {
            playerEquipment.FireEquippedWeapon();
        }

        
    }

    private void SwitchWeapon()
    {
        if(Input.GetKeyDown(Controls.SwitchToSecondary))
        {
            playerEquipment.SwitchToSecondary();
        }    
        else if(Input.GetKeyDown(Controls.SwitchToMain))
        {
            playerEquipment.SwitchToMain();
        }
    }
}
