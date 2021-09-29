using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DamageIndicator : MonoBehaviour
{

    public bool isPrefab = false;


    private RectTransform rectTransformImage = null;
    private Image image = null;
    private Transform player = null;

    private Vector3 origin;

    private Vector3 dir_ = Vector3.zero;
    private Quaternion lookRot_ = Quaternion.identity;
    private Vector3 northDirection_ = Vector3.zero;

    [SerializeField] private float destroyTime = 5f;

    public void SetHitOriginPosition(Vector3 _origin)
    {
        player = FindObjectOfType<PlayerResources>().transform;

        isPrefab = false;
        image = GetComponent<Image>();
        rectTransformImage = image.GetComponent<RectTransform>();

        Color _transitionColor = image.color;
        _transitionColor.a = 0;

        origin = _origin;
        LeanTween.color(rectTransformImage, _transitionColor, destroyTime);
        Destroy(gameObject, destroyTime);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPrefab) { return; }

        dir_ = origin - player.position;
        lookRot_ = Quaternion.LookRotation(dir_);
        lookRot_.z = -lookRot_.y;
        lookRot_.x = 0;
        lookRot_.y = 0;

        northDirection_ = new Vector3(0, 0, player.eulerAngles.y);


        transform.rotation = lookRot_ * Quaternion.Euler(northDirection_);

    }
}
