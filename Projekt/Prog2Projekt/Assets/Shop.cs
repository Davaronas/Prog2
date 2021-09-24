using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel = null;

    public Action OnShopOpened;
    public Action OnShopClosed;


   [HideInInspector]  public bool isOpened = false;


    private void Start()
    {
        shopPanel.SetActive(false);
    }

    public void OpenShop() // round ends
    {
        shopPanel.SetActive(true);
        OnShopOpened?.Invoke();
        isOpened = true;
    }

    public void CloseShop() // round starts
    {
        shopPanel.SetActive(false);
        OnShopClosed?.Invoke();
        isOpened = false;
    }


}
