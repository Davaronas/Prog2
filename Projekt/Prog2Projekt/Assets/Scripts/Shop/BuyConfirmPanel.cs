using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyConfirmPanel : MonoBehaviour
{

    [SerializeField] private Text areYouSureText = null;
    [SerializeField] private Text itemNameText = null;


    private InventoryWeapon selectedInvetoryWeapon = null;
    private Perk selectedPerk = null;

    private readonly string areYouSureString = "Are you sure you want to buy this for ";


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetItem(InventoryWeapon _iw, string _name, string _cost)
    {
        selectedInvetoryWeapon = _iw;
        selectedPerk = null;

        areYouSureText.text = areYouSureString + _cost + " ?";
        itemNameText.text = _name;

        gameObject.SetActive(true);
        

    }

    public void SetItem(Perk _perk, string _name, string _cost)
    {
        selectedPerk = _perk;
        selectedInvetoryWeapon = null;

        areYouSureText.text = areYouSureString + _cost + " ?";
        itemNameText.text = _name;

        gameObject.SetActive(true);
    }


    public void RemoteCall_Confirm()
    {
        if(selectedPerk != null)
        {
            selectedPerk.BuyPerk();
        }
        else if(selectedInvetoryWeapon != null)
        {
            selectedInvetoryWeapon.BuyWeapon();
        }

        gameObject.SetActive(false);
    }

    public void RemoteCall_CloseBuyConfirmPanel()
    {
        gameObject.SetActive(false);
    }
}
