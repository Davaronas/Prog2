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
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;

        
    }

   
}
