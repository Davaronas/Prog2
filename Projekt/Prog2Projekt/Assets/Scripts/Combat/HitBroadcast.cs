using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HitBroadcast : MonoBehaviour
{
    public Action<int, Vector3> onHit;


    public void Hit(int _damage, Vector3 _pos)
    {
        onHit?.Invoke(-_damage,_pos);
    }


}
