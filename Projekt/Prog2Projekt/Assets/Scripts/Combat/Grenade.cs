using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private int damage = 100;
    [SerializeField] private float explosionTime = 5f;
    [SerializeField] private float explosionRadius = 10f;
    [Space]
    [SerializeField] private GameObject explosionEffect = null;
    [SerializeField] private float explosionLiveTime = 0.2f;
   
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
            GameObject _explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            LeanTween.scale(_explosion, new Vector3(explosionRadius * 2, explosionRadius * 2, explosionRadius * 2),explosionLiveTime);
            Destroy(_explosion, explosionLiveTime);
        }


        Collider[] _objectsHit = Physics.OverlapSphere(transform.position, explosionRadius);
        HitBroadcast _hb_out = null;

        print(_objectsHit.Length + " le");
        for (int i = 0; i < _objectsHit.Length; i++)
        {
            print(_objectsHit[i].name);
            if(_objectsHit[i].TryGetComponent(out _hb_out))
            {
                _hb_out.Hit(damage, transform.position);
            }
            _hb_out = null;
        }

        

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
