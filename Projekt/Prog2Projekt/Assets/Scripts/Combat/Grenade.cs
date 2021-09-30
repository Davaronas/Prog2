using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private int damage = 100;
    [SerializeField] private float explosionTime = 5f;
    [SerializeField] private float explosionRadius = 10f;
    [SerializeField] private GameObject explosionEffect = null;
    void Start()
    {
        Invoke(nameof(Explode), explosionTime);
        GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)),ForceMode.VelocityChange); // egy kis random forgás hogy jobban nézzen ki ahogy eldobjuk
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void Explode()
    {
        if (explosionEffect)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }


        Collider[] _objectsHit = Physics.OverlapSphere(transform.position, explosionRadius);
        HitBroadcast _hb_out = null;

        for (int i = 0; i < _objectsHit.Length; i++)
        {
            if(_objectsHit[i].TryGetComponent(out _hb_out))
            {
                _hb_out.Hit(damage, transform.position);
            }
            _hb_out = null;
        }



        Destroy(gameObject);
    }
}
