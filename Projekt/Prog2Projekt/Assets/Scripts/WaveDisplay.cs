using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveDisplay : MonoBehaviour
{
    [SerializeField] private Vector2 baseSize;
    [SerializeField] private Vector2 toSize;
    [SerializeField] private float transitionTime = 3f;
    private RectTransform rectTransform = null;
    private Text waveDisplayText = null;

    

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        waveDisplayText = GetComponent<Text>();
    }


    public void Display(int _waveNumber)
    {
        waveDisplayText.text = "Wave " + _waveNumber + " incoming...";
        waveDisplayText.enabled = true;
        LeanTween.size(rectTransform, toSize,transitionTime);
        //LeanTween.color(rectTransform, Color.white,transitionTime);
        Invoke(nameof(Hide), transitionTime);
    }

    private void Hide()
    {
        LeanTween.size(rectTransform, baseSize, transitionTime);
        LeanTween.color(rectTransform, Color.clear, transitionTime);
        Invoke(nameof(DisableText), transitionTime);
    }

    private void DisableText()
    {
        waveDisplayText.enabled = false;
    }
}
