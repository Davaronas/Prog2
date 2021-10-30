using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private int damage = 0;
    private Vector3 startPos = Vector3.zero;

    private HitBroadcast possibleTarget_;

    private bool hit = false;

    public virtual void SetDamage(int _damage)
    {
        damage = _damage;
        startPos = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        

        if (!hit)
        {

            hit = true;
           
            if (collision.gameObject.TryGetComponent(out possibleTarget_))
            {
                possibleTarget_.Hit(damage, startPos);
            }

           print(collision.gameObject.name + " " + (possibleTarget_ == null));
            Destroy(gameObject);
        }
    }
}
