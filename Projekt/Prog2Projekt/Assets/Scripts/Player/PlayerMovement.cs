using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof( CharacterController))]
public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed = 5f;
    [SerializeField] private Vector3 gravity = Vector3.down;
    [SerializeField] private Vector3 jetpackForce = Vector3.up;
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashDuration = 0.1f;
    [SerializeField] private float dashRegenTime = 5f;


     private CharacterController characterController = null;
    private PlayerResources playerResources = null;
    private PlayerModifiers playerModifiers = null;
    private PlayerControl playerControl = null;
    private PlayerUI playerUI = null;
    // private Rigidbody rb = null;

    private bool isJetpackEnabled = false;

    

    private bool isDash1Active = false;
    private bool isDash2Active = false;

    private int forward_;
    private int back_;
    private int left_;
    private int right_;
    private Vector3 movementVector_ = Vector3.zero;
    private Vector3 constantForceVector = Vector3.zero;

    private float dashTimer_ = 0f;

    private float dash1RegenTimer_ = 0f;
    private float dash2RegenTimer_ = 0f;


    private void Start()
    {
         characterController = GetComponent<CharacterController>();
        playerResources = GetComponent<PlayerResources>();
        playerModifiers = GetComponent<PlayerModifiers>();
        playerControl = GetComponent<PlayerControl>();
        playerUI = GetComponent<PlayerUI>();

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
                           (-transform.right * left_)).normalized * (speed+playerModifiers.GetMovementSpeedIncrease()) * Time.deltaTime;



          characterController.Move(movementVector_ + (constantForceVector * Time.deltaTime));
    }

    public void Dash(Controls.MovementDirections[] _md)
    {
        if(_md.Length < 1) { return; }

        if (isDash1Active)
        {
            isDash1Active = false;
            StartCoroutine(RegenerateDash(1));
        }
        else if (isDash2Active)
        {
            isDash2Active = false;
            StartCoroutine(RegenerateDash(2));
        }
        else
        {
            return;
        }

        forward_ = 0;
        back_ = 0;
        left_ = 0;
        right_ = 0;


        for (int i = 0; i < _md.Length; i++)
        {
            switch (_md[i])
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
                           (-transform.right * left_)).normalized * dashSpeed * Time.deltaTime;

        constantForceVector = gravity;
        StartCoroutine(DashCoroutine(movementVector_));
        playerUI.Dashes(isDash1Active, isDash2Active);
        
    }

    public void EnableJetpack(int _delegateInvokeValue)
    {
        isJetpackEnabled = true;
        playerResources.SetJetpackToFull();
    }

    public void EnableDash1(int _delegateInvokeValue)
    {
        isDash1Active = true;
        playerUI.Dashes(isDash1Active, isDash2Active);
    }

    public void EnableDash2(int _delegateInvokeValue)
    {
        isDash2Active = true;
        playerUI.Dashes(isDash1Active, isDash2Active);
    }

    public void UseJetpack()
    {
        if (!isJetpackEnabled) { return; }

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
        if (!isJetpackEnabled) { return; }

        constantForceVector = gravity;
        playerResources.EndJetpackUse();
    }

    public Vector3 GetVelocity()
    {
        return characterController.velocity;
    }


    public IEnumerator DashCoroutine(Vector3 _mv)
    {
        dashTimer_ = dashDuration;
        playerControl.AddBlockAction();

        while (dashTimer_ > 0)
        {
            characterController.Move(_mv);
            dashTimer_ -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        playerControl.RemoveBlockAction();
    }

    public IEnumerator RegenerateDash(int _which)
    {
        switch(_which)
        {
            case 1:
                dash1RegenTimer_ = dashRegenTime;
                while(dash1RegenTimer_ > 0)
                {
                    dash1RegenTimer_ -= Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }

                dash1RegenTimer_ = 0;
                isDash1Active = true;
                playerUI.Dashes(isDash1Active, isDash2Active);

                break;
            case 2:
                dash2RegenTimer_ = dashRegenTime;
                while (dash2RegenTimer_ > 0)
                {
                    dash2RegenTimer_ -= Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }

                dash2RegenTimer_ = 0;
                isDash2Active = true;
                playerUI.Dashes(isDash1Active, isDash2Active);



                break;
        }
    }
}
