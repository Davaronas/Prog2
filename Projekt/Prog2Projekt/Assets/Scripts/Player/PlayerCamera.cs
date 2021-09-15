using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class PlayerCamera : MonoBehaviour
{

    [SerializeField] private float rotateSensitivity = 5f;
    [SerializeField] private int maxY_Rotation = 80;
    [SerializeField] private int minY_Rotation = -80;
    [SerializeField] private Camera playerCamera = null;

      private CharacterController characterController;
   // private Rigidbody rb = null;



    private Vector3 rotationVector_ = Vector3.zero;
    private float yLook_ = 0;
    private float xLook_ = 0;
    private float currentCameraRotation_X = 90;

    
    void Start()
    {
          characterController = GetComponent<CharacterController>();
    }




    public void RotatePlayer(float _x)
    {
        if(_x == 0) { return; }

        yLook_ = _x * rotateSensitivity * Time.deltaTime;

        rotationVector_ = transform.up * yLook_;

         transform.rotation *= Quaternion.Euler(rotationVector_);
    }

    public void RotateCamera(float _y)
    {
        if (_y == 0) { return; }

         xLook_ =  _y * rotateSensitivity * Time.deltaTime;

        currentCameraRotation_X -= xLook_;
        currentCameraRotation_X = Mathf.Clamp(currentCameraRotation_X, minY_Rotation, maxY_Rotation);
        playerCamera.transform.localEulerAngles = new Vector3(currentCameraRotation_X, 0f, 0f);

       

    }
}
