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
    [SerializeField] private Image enemyHealthbarBg = null;
    [SerializeField] private Image enemyHealthbar = null;
   // [SerializeField] private Color enemyFullHealthColor = Color.green;
   // [SerializeField] private Color enemyLowHealthColor = Color.red;
    private Image crosshairImage = null;
    [Space]
    [SerializeField] private HelmetUI helmetUI = null;
    [Space]
    [SerializeField] private Transform damageIndicatorSpawn = null;
    [SerializeField] private GameObject damageIndicatorPrefab = null;
    [Space]
    [SerializeField] private Text moneyText = null;



    private Shop shop = null;

    private float enemyHealthPercent_ = 0;


    private void Awake()
    {
        crosshairImage = crosshair.GetComponent<Image>();
        enemyHealthbarBg.gameObject.SetActive(false);

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

        EnemyResources.OnEnemyHoverGlobal += OnEnemyHoverGlobalCallback;

    }

    private void OnDestroy()
    {
        shop.OnShopOpened -= ShopOpenedCallback;
        shop.OnShopClosed -= ShopClosedCallback;

        EnemyResources.OnEnemyHoverGlobal -= OnEnemyHoverGlobalCallback;
    }


    private void ShopOpenedCallback()
    {
        crosshairImage.enabled = false;
    }

    private void ShopClosedCallback()
    {
        crosshairImage.enabled = true;
    }

    private void OnEnemyHoverGlobalCallback(int _health, int _baseHealth)
    {
        enemyHealthPercent_ = (float)_health / _baseHealth;

        enemyHealthbar.fillAmount = enemyHealthPercent_;
      //  enemyHealthbar.color = (enemyHealthPercent_ * enemyFullHealthColor) + ((1 - enemyHealthPercent_) * enemyLowHealthColor);
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


    public void DamageIndicator(Vector3 _pos)
    {
       DamageIndicator _di = Instantiate(damageIndicatorPrefab, damageIndicatorSpawn.position, Quaternion.identity, damageIndicatorSpawn).GetComponent<DamageIndicator>();
        _di.SetHitOriginPosition(_pos);
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

        if(_hb != null) // we found an enemy, enabling enemy healthbar gameobject, fill amount will be et from global enemy hover event
        {
            crosshairImage.color = Color.red;

            if (!enemyHealthbarBg.gameObject.activeInHierarchy)
            {
                enemyHealthbarBg.gameObject.SetActive(true);
            }

            _hb.OnHover();
        }
        else
        {
            crosshairImage.color = Color.white;

            if (enemyHealthbarBg.gameObject.activeInHierarchy)
            {
                enemyHealthbarBg.gameObject.SetActive(false);
            }
        }
    }

    public void Money(int _amount)
    {
        moneyText.text = _amount.ToString();
    }
}
