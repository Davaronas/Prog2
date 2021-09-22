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


    private  List<Controls.MovementDirections> movementDirections_ = new List<Controls.MovementDirections>();


    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerCamera = GetComponent<PlayerCamera>();
        playerEquipment = GetComponent<PlayerEquipment>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;



        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Rotation();

        Jetpack();

        MainAttack();

        SwitchWeapon();
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
