using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    [SerializeField] private Vector3 rotationEuler = Vector3.zero;


    private void Update()
    {
        transform.rotation *= Quaternion.Euler(rotationEuler * Time.deltaTime);
    }
}
