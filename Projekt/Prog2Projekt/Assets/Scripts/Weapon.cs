using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Vector3 localPlayerPosOnCamera = Vector3.zero;
    public Vector3 localPlayerRotOnCameraEuler = Vector3.zero;
    [Space]
    [SerializeField] public bool isSecondary = false;
    [SerializeField] private float secondaryRechargeInterval = 0.6f;
    [SerializeField] private int ammoCost = 5;
    [SerializeField] private float fireInterval = 0.5f;
    [SerializeField] private float projectileSpeed = 20f;
    [SerializeField] private int damage = 10;
    [Space]
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform projectileSpawn;
    [SerializeField] private ParticleSystem pSystem;
    [SerializeField] private LayerMask raycastLayerMask;
    [Space]
    [SerializeField] private GameObject model;
   


    private PlayerResources playerResources = null;
    private PlayerUI playerUI = null;
    private PlayerEquipment playerEquipment = null;

    private RaycastHit ray_;


    private bool allowFire = true;

    private bool fired_ = false;

    void Start()
    {
        playerResources = FindObjectOfType<PlayerResources>();
        playerEquipment = FindObjectOfType<PlayerEquipment>();
        playerUI = FindObjectOfType<PlayerUI>();

        if(isSecondary)
        {
            StartCoroutine(RechargeSecondary());
        }
    }

    private void Update()
    {
        if(Physics.Raycast(projectileSpawn.position,projectileSpawn.forward,out ray_,Mathf.Infinity,raycastLayerMask))
        {
            playerUI.Crosshair(ray_.point,ray_.collider.GetComponent<HitBroadcast>());
        }
     
      
    }

    public void HideModel()
    {
        model.SetActive(false);
    }

    public void ShowModel()
    {
        model.SetActive(true);
    }

    public void Fire()
    {

        if (!allowFire) { return; }

        fired_ = false;

        if(isSecondary)
        {
            if(playerResources.GetCurrentSecondaryAmmo() >= ammoCost)
            {
                playerResources.ChangeSecondaryAmmo(-ammoCost);
                fired_ = true;
            }
        }
        else
        {
            if(playerResources.GetCurrentMainAmmo() >= ammoCost)
            {
                playerResources.ChangeMainAmmo(-ammoCost);
                fired_ = true;
            }
        }

        if(!fired_) { return; }



        Rigidbody _newProjectile = Instantiate(projectile, projectileSpawn.position, Quaternion.LookRotation(projectileSpawn.forward)).GetComponent<Rigidbody>();
        _newProjectile.AddForce(projectileSpawn.forward * projectileSpeed, ForceMode.VelocityChange);
        _newProjectile.GetComponent<Projectile>().SetDamage(damage);

        StartCoroutine(FireCooldown());

        // instantiate projectile
    }

    IEnumerator FireCooldown()
    {
        allowFire = false;
        yield return new WaitForSeconds(fireInterval);
        allowFire = true;
    }


    IEnumerator RechargeSecondary()
    {
        while (true)
        {
            yield return new WaitForSeconds(secondaryRechargeInterval);
            playerResources.ChangeSecondaryAmmo(1);
        }

    }
}
