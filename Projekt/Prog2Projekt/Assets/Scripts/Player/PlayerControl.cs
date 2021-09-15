using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerCamera))]
public class PlayerControl : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerCamera playerCamera;


    private  List<Controls.MovementDirections> movementDirections_ = new List<Controls.MovementDirections>();


    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerCamera = GetComponent<PlayerCamera>();

        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Rotation();
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
}
