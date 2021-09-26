using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HitBroadcast : MonoBehaviour
{

    public Action<int, Vector3> OnHit;
    public Action OnHover;


    public void Hit(int _damage, Vector3 _pos)
    {
        OnHit?.Invoke(-_damage,_pos);
    }

    public void Hover()
    {
        OnHover?.Invoke();
    }

   
}
