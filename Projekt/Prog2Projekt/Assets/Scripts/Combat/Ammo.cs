using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]

public class Ammo : MonoBehaviour
{
    [SerializeField] private int containsAmmo = 20;
    [SerializeField] private float destroyTime = 40f;

    public void SetAmount(int _a)
    {
        containsAmmo = _a;
        Invoke(nameof(DestroyDrop), destroyTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerResources _pr))
        {
            if (!_pr.IsBothAmmoTypeFull())
            {
                _pr.ChangeSecondaryAmmo(50);
                _pr.ChangeMainAmmo(containsAmmo);
                Destroy(gameObject);
            }
        }
    }


    private void DestroyDrop()
    {
        Destroy(gameObject);
    }
}
