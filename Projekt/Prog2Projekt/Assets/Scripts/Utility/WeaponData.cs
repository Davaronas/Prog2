using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour
{
   public struct WeaponInfo
    {
        public float rechargeRate;
        public float ammoUsage;
        public float fireRate;
        public float projectileSpeed;
        public float damage;
    }


    [SerializeField] private Weapon[] weaponDatabase = null;
    private static Weapon[] weaponDatabase_S = null;



    private static float fastestRechargeTime_ = Mathf.Infinity;
    private static float highestAmmoUsage_ = 0;
    private static float highestFireRate = Mathf.Infinity;
    private static float fastestProjectileSpeed_ = 0;
    private static float highestDamage_ = 0;


    private void Start()
    {
        weaponDatabase_S = weaponDatabase;
    }

    public static WeaponInfo GetSliderValues(Weapon _w)
    {
        WeaponInfo _newWeaponInfo = new WeaponInfo();


        for (int i = 0; i < weaponDatabase_S.Length; i++)
        {
            if (weaponDatabase_S[i].secondaryRechargeInterval < fastestRechargeTime_)
            {
                fastestRechargeTime_ = weaponDatabase_S[i].secondaryRechargeInterval;
            }

            if (weaponDatabase_S[i].ammoCost > highestAmmoUsage_)
            {
                highestAmmoUsage_ = weaponDatabase_S[i].ammoCost;
            }

            if (weaponDatabase_S[i].fireInterval < highestFireRate)
            {
                highestFireRate = weaponDatabase_S[i].fireInterval;
            }

            if (weaponDatabase_S[i].projectileSpeed > fastestProjectileSpeed_)
            {
                fastestProjectileSpeed_ = weaponDatabase_S[i].projectileSpeed;
            }

            if (weaponDatabase_S[i].damage > highestDamage_)
            {
                highestDamage_ = weaponDatabase_S[i].damage;
            }
        }


        if (_w.isSecondary)
        {
            _newWeaponInfo.rechargeRate = fastestRechargeTime_ / _w.secondaryRechargeInterval;
        }
        else
        {
            _newWeaponInfo.rechargeRate = -1;
        }

        _newWeaponInfo.ammoUsage = _w.ammoCost / highestAmmoUsage_;
        _newWeaponInfo.fireRate = highestFireRate / _w.fireInterval;
        _newWeaponInfo.projectileSpeed = _w.projectileSpeed / fastestProjectileSpeed_;
        _newWeaponInfo.damage = _w.damage / highestDamage_;



       fastestRechargeTime_ = Mathf.Infinity;
       highestAmmoUsage_ = 0;
       highestFireRate = Mathf.Infinity;
       fastestProjectileSpeed_ = 0;
       highestDamage_ = 0;


        return _newWeaponInfo;
    }





    public float GetRechargeRateRatio(Weapon _w)
    {
        if (!_w.isSecondary) { return -1; }

        float _fastest = Mathf.Infinity;
        for (int i = 0; i < weaponDatabase_S.Length; i++)
        {
            if(weaponDatabase_S[i].secondaryRechargeInterval < _fastest)
            {
                _fastest = weaponDatabase_S[i].secondaryRechargeInterval;
            }
        }

        return _fastest / _w.secondaryRechargeInterval;
    }

}
