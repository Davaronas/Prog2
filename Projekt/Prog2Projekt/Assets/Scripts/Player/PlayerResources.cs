using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerUI))]
[RequireComponent(typeof(HitBroadcast))]
public class PlayerResources : MonoBehaviour
{
    [SerializeField] private  int baseHealth = 100;
    [SerializeField] private  int baseMainAmmo = 100;
    [SerializeField] private  int baseSecondaryAmmo = 50;
    [Space]
    [SerializeField] private float baseJetpackEnergy = 50f;
    [SerializeField][Tooltip("This number also gets multiplied by the delta time because we are draining it in update")] 
    private float jetpackCost = 2f;
    [SerializeField] private float jetpackRechargeTime = 5f;

    private PlayerEquipment playerEquipment = null;

    private int health = 0;
    private int mainAmmo = 0;
    private int secondaryAmmo = 0;

    private int money = 0;

    private float jetpackEnergy = 0;

    private PlayerUI playerUI = null;
    private HitBroadcast hitBroadcast = null;

    private Coroutine rechargeJetpack_ = null;

    //  PLAYER RESOURCES START NEEDS TO RUN BEFORE PLAYER EQUIPMENT START (EquipSecondary)

    void Start()
    {
        health = baseHealth;
        mainAmmo = baseMainAmmo;
        secondaryAmmo = baseSecondaryAmmo;
        jetpackEnergy = baseJetpackEnergy;

        playerUI = GetComponent<PlayerUI>();
        playerUI.Health(health, baseHealth);
        playerUI.Ammo(secondaryAmmo, baseSecondaryAmmo);

        hitBroadcast = GetComponent<HitBroadcast>();

        hitBroadcast.OnHit += OnHitCallback;
        EnemyResourceDrop.OnEnemyDeathGlobal += OnEnemyDeathGlobalCallback;

        playerEquipment = GetComponent<PlayerEquipment>();
        
    }

    private void OnDestroy()
    {
        hitBroadcast.OnHit -= OnHitCallback;
        EnemyResourceDrop.OnEnemyDeathGlobal -= OnEnemyDeathGlobalCallback;
    }


    private void OnEnemyDeathGlobalCallback(int _amount)
    {
        ChangeMoney(_amount);
    }

    public void ChangeMoney(int _amount)
    {
        money += _amount;
        playerUI.Money(money);
    }


    private void OnHitCallback(int _health, Vector3 _pos)
    {
        ChangeHealth(_health);
        playerUI.DamageIndicator(_pos);
    }

    public void ChangeHealth(int _amount)
    {
        health = Mathf.Clamp(health + _amount,0, baseHealth);
        playerUI.Health(health, baseHealth);


        if(health <= 0)
        {
            // die
        }
    }

    public void ChangeSecondaryAmmo(int _amount)
    {
        secondaryAmmo = Mathf.Clamp(secondaryAmmo + _amount,0,baseSecondaryAmmo);

        if (playerEquipment.IsSecondarySelected())
        {
            playerUI.Ammo(secondaryAmmo, baseSecondaryAmmo);
        }
    }

    public void ChangeSecondaryAmmo(int _amount, bool _showUI)
    {
        secondaryAmmo = Mathf.Clamp(secondaryAmmo + _amount, 0, baseSecondaryAmmo);

        if (_showUI)
        {
            playerUI.Ammo(secondaryAmmo, baseSecondaryAmmo);
        }
    }

    public void ChangeMainAmmo(int _amount)
    {
        mainAmmo = Mathf.Clamp(mainAmmo + _amount,0,baseMainAmmo);

        if (!playerEquipment.IsSecondarySelected())
        {
            playerUI.Ammo(mainAmmo, baseMainAmmo);
        }
    }

    public void ChangeMainAmmo(int _amount, bool _showUI)
    {
        mainAmmo = Mathf.Clamp(mainAmmo + _amount, 0, baseMainAmmo);

        if (_showUI)
        {
            playerUI.Ammo(mainAmmo, baseMainAmmo);
        }
    }

    public void ChangeJetpackEnergy()
    {
        jetpackEnergy = Mathf.Clamp(jetpackEnergy - (jetpackCost * Time.deltaTime), 0, baseJetpackEnergy);

        if(rechargeJetpack_ != null)
        {
            StopCoroutine(rechargeJetpack_);
            rechargeJetpack_ = null;
        }


        playerUI.Jetpack(jetpackEnergy,baseJetpackEnergy);
    }

    private void SetJetpackToFull()
    {
        jetpackEnergy = baseJetpackEnergy;

        playerUI.Jetpack(jetpackEnergy, baseJetpackEnergy);
    }


    public int GetCurrentSecondaryAmmo()
    {
        return secondaryAmmo;
    }

    public int GetCurrentMainAmmo()
    {
        return mainAmmo;
    }

    public float GetJetpackCost()
    {
        return jetpackCost;
    }

    public float GetJetpackEnergy()
    {
        return jetpackEnergy;
    }

    public void EndJetpackUse()
    {

        if(rechargeJetpack_ == null)
        {
            rechargeJetpack_ = StartCoroutine(RechargeJetpack());
        }
    }

    IEnumerator RechargeJetpack()
    {
        yield return new WaitForSeconds(jetpackRechargeTime);
        SetJetpackToFull();
    }
   
}
