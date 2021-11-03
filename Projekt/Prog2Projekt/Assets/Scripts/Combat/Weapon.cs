using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public  string weaponName = "";
    [Space]
    public  Vector3 localPlayerPosOnCamera = Vector3.zero;
    public  Vector3 localPlayerRotOnCameraEuler = Vector3.zero;
    [Space]
    public  bool isSecondary = false;
    public  float secondaryRechargeInterval = 0.6f;
    public int ammoCost = 5;
    public   float fireInterval = 0.5f;
    public  float projectileSpeed = 20f;
    public  int damage = 10;
    public int cost = 200;
    [Space]
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform projectileSpawn;
    [SerializeField] private ParticleSystem pSystem;
    [SerializeField] private LayerMask raycastLayerMask;
    [Space]
    [SerializeField] private GameObject model;
    [SerializeField] private Transform modelAnimationEndTransform;
    [SerializeField] private float animationSpeed = 1f;
    [Space]
    public Transform rightHandIkPos;
    public Transform leftHandIkPos;

    private Vector3 originalModelPosition = Vector3.zero;
    private Quaternion originalModelRotation = Quaternion.identity;
    private Coroutine animationCoroutine = null;

    private PlayerResources playerResources = null;
    private PlayerUI playerUI = null;
    private PlayerEquipment playerEquipment = null;

    private RaycastHit ray_;


    private bool allowFire = true;

    private bool fired_ = false;


    private float animationProgress_ = 0f;

    void Start()
    {
        playerResources = FindObjectOfType<PlayerResources>();
        playerEquipment = FindObjectOfType<PlayerEquipment>();

        playerUI = FindObjectOfType<PlayerUI>();

        originalModelPosition = model.transform.localPosition;
        originalModelRotation = model.transform.localRotation;

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

        Projectile _p;
        if (_newProjectile.TryGetComponent(out _p))
        {
            _newProjectile.GetComponent<Projectile>().SetDamage(damage);
        }

        StartCoroutine(FireCooldown());

        if (modelAnimationEndTransform != null)
        {
            if(animationCoroutine != null)
            {
                StopCoroutine(animationCoroutine);
                animationCoroutine = null;
            }

          animationCoroutine =  StartCoroutine(Animate());
        }


        // start animation

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




    IEnumerator Animate()
    {
        model.transform.localPosition = originalModelPosition;
        model.transform.localRotation = originalModelRotation;

        animationProgress_ = 0f;

        while (model.transform.localPosition != modelAnimationEndTransform.localPosition || model.transform.localRotation != modelAnimationEndTransform.localRotation)
        {
            model.transform.localPosition = Vector3.Slerp(model.transform.localPosition, modelAnimationEndTransform.transform.localPosition, animationProgress_ * (animationSpeed * Time.deltaTime));
            model.transform.localRotation = Quaternion.Slerp(model.transform.localRotation, modelAnimationEndTransform.localRotation, animationProgress_ * (animationSpeed * Time.deltaTime));
            animationProgress_ += animationSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();


        }

        yield return new WaitForEndOfFrame();

        animationProgress_ = 0f;

        while (model.transform.localPosition != originalModelPosition || model.transform.localRotation != originalModelRotation)
        {
            model.transform.localPosition = Vector3.Slerp(model.transform.localPosition, originalModelPosition, animationProgress_ * (animationSpeed * Time.deltaTime));
            model.transform.localRotation = Quaternion.Slerp(model.transform.localRotation, originalModelRotation, animationProgress_ * (animationSpeed * Time.deltaTime));
            animationProgress_ += animationSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForEndOfFrame();
    }
}
