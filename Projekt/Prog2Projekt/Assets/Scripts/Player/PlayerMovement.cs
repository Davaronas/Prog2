using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof( CharacterController))]
public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed = 5f;
    [SerializeField] private Vector3 gravity = Vector3.down;


     private CharacterController characterController;
   // private Rigidbody rb = null;



    private int forward_;
    private int back_;
    private int left_;
    private int right_;
    private Vector3 movementVector_ = Vector3.zero;


    private void Start()
    {
         characterController = GetComponent<CharacterController>();
    }


    public void Move(Controls.MovementDirections[] _md)
    {

        forward_ = 0;
        back_ = 0;
        left_ = 0;
        right_ = 0;

        for (int i = 0; i < _md.Length; i++)
        {
            switch(_md[i])
            {
                case Controls.MovementDirections.Forward:
                    forward_ = 1;
                    break;
                case Controls.MovementDirections.Back:
                    back_ = 1;
                    break;
                case Controls.MovementDirections.Left:
                    left_ = 1;
                    break;
                case Controls.MovementDirections.Right:
                    right_ = 1;
                    break;
            }
        }

        movementVector_ = ((transform.forward * forward_) +
                          (-transform.forward * back_) +
                          (transform.right * right_) +
                           (-transform.right * left_)).normalized * speed * Time.deltaTime;



          characterController.Move(movementVector_ + (gravity * Time.deltaTime));
    }
}
