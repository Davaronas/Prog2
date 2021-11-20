using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject main = null;
    [SerializeField] private GameObject options = null;

    [SerializeField] private Dropdown HUD_dropdown = null;
    [SerializeField] private Text bestWaveText = null;




    // Start is called before the first frame update
    void Start()
    {


        HUD_dropdown.ClearOptions();
        List<Dropdown.OptionData> _options = new List<Dropdown.OptionData>();
        Dropdown.OptionData _0 = new Dropdown.OptionData("Simple");
        Dropdown.OptionData _1 = new Dropdown.OptionData("Helmet");
        _options.Add(_0);
        _options.Add(_1);
        HUD_dropdown.AddOptions(_options);

        HUD_dropdown.value = PlayerPrefs.GetInt("HUD", 1);

        bestWaveText.text = "Best: " + PlayerPrefs.GetInt("RecordWave", 0) + " waves";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RemoteCall_ChangeHudDropdown()
    {
        PlayerPrefs.SetInt("HUD", HUD_dropdown.value);
    }



    public void ShowOptionsMenu()
    {
        main.SetActive(false);
        options.SetActive(true);
    }

    public void ShowMainMenu()
    {
        main.SetActive(true);
        options.SetActive(false);
    }
}
