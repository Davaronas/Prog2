using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof( CharacterController))]
public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed = 5f;
    [SerializeField] private Vector3 gravity = Vector3.down;
    [SerializeField] private Vector3 jetpackForce = Vector3.up;


     private CharacterController characterController = null;
    private PlayerResources playerResources = null;
   // private Rigidbody rb = null;



    private int forward_;
    private int back_;
    private int left_;
    private int right_;
    private Vector3 movementVector_ = Vector3.zero;
    private Vector3 constantForceVector = Vector3.zero;


    private void Start()
    {
         characterController = GetComponent<CharacterController>();
        playerResources = GetComponent<PlayerResources>();

        constantForceVector = gravity;
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



          characterController.Move(movementVector_ + (constantForceVector * Time.deltaTime));
    }

    public void UseJetpack()
    {
        if(playerResources.GetJetpackEnergy() > 0)
        {
            constantForceVector = jetpackForce;
            playerResources.ChangeJetpackEnergy();
        }
        else
        {
            EndJetpackUse();
        }
    }

    public void EndJetpackUse()
    {
        constantForceVector = gravity;
        playerResources.EndJetpackUse();
    }

    public Vector3 GetVelocity()
    {
        return characterController.velocity;
    }
}
