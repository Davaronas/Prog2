using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    private enum TypeUI { Helmet, Simplistic}


    [SerializeField] private TypeUI ui = TypeUI.Helmet;

    [SerializeField] private Image healthBar = null;
    [SerializeField] private Image ammoBar = null;
    [SerializeField] private Image jetpackBar = null;
    [SerializeField] private Image healthBarBg = null;
    [SerializeField] private Image ammoBarBg = null;
    [SerializeField] private Image jetpackBarBg = null;


    [SerializeField] private Text healthText = null;
    [SerializeField] private Text ammoText = null;

    [SerializeField] private Camera mainCam = null;
    [SerializeField] private RectTransform crosshair = null;
    private Image crosshairImage = null;
    [Space]
    [SerializeField] private HelmetUI helmetUI = null;



    private Shop shop = null;


    private void Awake()
    {
        crosshairImage = crosshair.GetComponent<Image>();

        if(ui == TypeUI.Helmet)
        {
            healthBarBg.enabled = false;
            healthBar.enabled = false;

            ammoBarBg.enabled = false;
            ammoBar.enabled = false;

            jetpackBarBg.enabled = false;
            jetpackBar.enabled = false;

            helmetUI.gameObject.SetActive(true);
        }
        else if(ui == TypeUI.Simplistic)
        {
            healthBarBg.enabled = true;
            healthBar.enabled = true;

            ammoBarBg.enabled = true;
            ammoBar.enabled = true;

            jetpackBarBg.enabled = true;
            jetpackBar.enabled = true;

            helmetUI.gameObject.SetActive(false);
        }

        shop = FindObjectOfType<Shop>();
        shop.OnShopOpened += ShopOpenedCallback;
        shop.OnShopClosed += ShopClosedCallback;

    }

    private void OnDestroy()
    {
        shop.OnShopOpened -= ShopOpenedCallback;
        shop.OnShopClosed -= ShopClosedCallback;
    }


    private void ShopOpenedCallback()
    {
        crosshairImage.enabled = false;
    }

    private void ShopClosedCallback()
    {
        crosshairImage.enabled = true;
    }


    public void Health(int _current, int _max)
    {
        healthText.text = _current + " / " + _max;


        if (ui == TypeUI.Helmet)
        {
            helmetUI.DisplayHealth(_current, _max);
        }
        else if(ui == TypeUI.Simplistic)
        {
            healthBar.fillAmount = (float)_current / _max;
        }
    }

    public void Ammo(int _current, int _max)
    {
        ammoText.text = _current + " / " + _max;


        if (ui == TypeUI.Helmet)
        {
            helmetUI.DisplayAmmo(_current, _max);
        }
        else if (ui == TypeUI.Simplistic)
        {
            ammoBar.fillAmount = (float)_current / _max;
        }


    }

    public void Jetpack(float _current, float _max)
    {
        if (ui == TypeUI.Helmet)
        {
            helmetUI.DisplayJetpack(_current, _max);
        }
        else if (ui == TypeUI.Simplistic)
        {
            jetpackBar.fillAmount = _current / _max;
        }
    }


    public void Crosshair(Vector3 _pos, HitBroadcast _hb)
    {
        crosshair.position = mainCam.WorldToScreenPoint(_pos);

        if(_hb != null)
        {
            crosshairImage.color = Color.red;
        }
        else
        {
            crosshairImage.color = Color.white;
        }
    }
}
