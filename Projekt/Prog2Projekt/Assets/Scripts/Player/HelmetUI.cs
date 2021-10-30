using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetUI : MonoBehaviour
{
    [SerializeField] private Renderer[] healthDisplays;
    [SerializeField] private Renderer[] ammoDisplays;
    [SerializeField] private Renderer[] jetpackDisplays;
    [SerializeField] private Renderer Dash1Display;
    [SerializeField] private Renderer Dash2Display;
    [Space]
    [SerializeField] private Material healthActiveMaterial;
    [SerializeField] private Material ammoActiveMaterial;
    [SerializeField] private Material jetpackActiveMaterial;
    [SerializeField] private Material inactiveMaterial;

    int healthAmount_ = 0;
    int ammoAmount_ = 0;
    int jetpackAmount_ = 0;


    public void DisplayHealth(int _currentHealth,int _baseHealth)
    {
        healthAmount_ = Mathf.CeilToInt((float)_currentHealth / _baseHealth * healthDisplays.Length);

        for (int i = 0; i < healthDisplays.Length; i++)
        {
            if (i < healthAmount_)
            {
                healthDisplays[i].material = healthActiveMaterial;
            }
            else
            {
                healthDisplays[i].material = inactiveMaterial;
            }
        }
    }

    public void DisplayAmmo(int _currentAmmo, int _baseAmmo)
    {
         ammoAmount_ = Mathf.CeilToInt((float)_currentAmmo / _baseAmmo * ammoDisplays.Length);

        for (int i = 0; i < ammoDisplays.Length; i++)
        {
            if (i < ammoAmount_)
            {
                ammoDisplays[i].material = ammoActiveMaterial;
            }
            else
            {
                ammoDisplays[i].material = inactiveMaterial;
            }
        }
    }

    public void DisplayJetpack(float _currentJetpack, float _baseJetpack)
    {
        jetpackAmount_ = Mathf.CeilToInt(_currentJetpack / _baseJetpack * jetpackDisplays.Length);

        for (int i = 0; i < jetpackDisplays.Length; i++)
        {
            if (i < jetpackAmount_)
            {
                jetpackDisplays[i].material = jetpackActiveMaterial;
            }
            else
            {
                jetpackDisplays[i].material = inactiveMaterial;
            }
        }
    }

    public void DisplayDashes(bool _dash1, bool _dash2)
    {
        if(_dash1)
        {
            Dash1Display.material = jetpackActiveMaterial;
        }
        else
        {
            Dash1Display.material = inactiveMaterial;
        }

        if(_dash2)
        {
            Dash2Display.material = jetpackActiveMaterial;
        }
        else
        {
            Dash2Display.material = inactiveMaterial;
        }
    }
}
