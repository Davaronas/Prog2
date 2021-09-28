using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMelee : Projectile
{
    private Rigidbody rb = null;
    private ParticleSystem ps = null;


    private void Awake()
    {
        ps = transform.root.GetComponentInChildren<ParticleSystem>();
        ps.transform.parent = null;

        Destroy(gameObject, ps.main.duration + ps.main.startLifetime.constantMax); // amint az összes particle elpusztult akkor pusztítsuk el a gameobjectet, ezután ne tudjon sebezni
        Destroy(ps.gameObject, 2f); // a particle rendszert is pusztítsuk el, ez igazából mindegy mikor csak a performance miatt jó
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;

        
    }


   
   
}
