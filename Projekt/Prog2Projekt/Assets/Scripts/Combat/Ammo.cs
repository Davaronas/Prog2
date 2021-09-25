using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]

public class Ammo : MonoBehaviour
{
    [SerializeField] private int containsAmmo = 20;

    public void SetAmount(int _a)
    {
        containsAmmo = _a;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerResources _pr))
        {
            _pr.ChangeSecondaryAmmo(50);
            _pr.ChangeMainAmmo(containsAmmo);
            Destroy(gameObject);
        }
    }
}
