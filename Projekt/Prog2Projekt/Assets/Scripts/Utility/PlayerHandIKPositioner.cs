using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandIKPositioner : MonoBehaviour
{
    [SerializeField] private Transform rightHandTarget;
    [SerializeField] private Transform leftHandTarget;


    private PlayerEquipment playerEquipment = null;

    private Transform rightHandTargetTransform = null;
    private Transform leftHandTargetTransform = null;



    private void Start()
    {
        playerEquipment = transform.root.GetComponent<PlayerEquipment>();
        playerEquipment.OnWeaponEquipped += SetIkTransforms;
    }



    private void OnDestroy()
    {
        playerEquipment.OnWeaponEquipped -= SetIkTransforms;
    }


    private void SetIkTransforms(Transform _r, Transform _l)
    {
        rightHandTargetTransform = _r;
        leftHandTargetTransform = _l;
    }

    private void Update()
    {
        SetIkPositions();
    }

    private void SetIkPositions()
    {
        if (rightHandTargetTransform != null)
        {
            rightHandTarget.position = rightHandTargetTransform.position;
            rightHandTarget.rotation = rightHandTargetTransform.rotation;
        }


        if (leftHandTargetTransform != null)
        {
            leftHandTarget.position = leftHandTargetTransform.position;
            leftHandTarget.rotation = leftHandTargetTransform.rotation;
        }
    }
}
