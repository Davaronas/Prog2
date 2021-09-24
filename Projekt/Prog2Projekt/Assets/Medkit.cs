using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Medkit : MonoBehaviour
{
    [SerializeField] private int containsHealth = 10;



    public void SetAmount(int _a)
    {
        containsHealth = _a;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerResources _pr))
        {
            _pr.ChangeHealth(containsHealth);
            Destroy(gameObject);
        }
    }
}
