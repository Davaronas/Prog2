using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour, IPointerDownHandler,IPointerEnterHandler,IPointerExitHandler,IPointerUpHandler
{

    private enum MainMenuButtonType {Play,Options,Exit, BackToMain }
    [SerializeField] private MainMenuButtonType buttonType;

    private delegate void MainMenuAction();
    private MainMenuAction buttonAction;


    private MainMenu mainMenu;
    private RectTransform rectTransform;

    [Space]
    [SerializeField] private float transitionSizeMultiplier = 1.08f;
    [SerializeField] private float transitionSpeed = 0.05f;
    [SerializeField] private Color clickColor = Color.grey;
    [SerializeField] private float colorChangeSpeed = 10f;
    private Vector2 originalSize = Vector3.zero;
    private Vector2 transitionedSize = Vector3.zero;

    private void Start()
    {
        mainMenu = FindObjectOfType<MainMenu>();
        rectTransform = GetComponent<RectTransform>();
        originalSize = rectTransform.sizeDelta;
        transitionedSize = originalSize * transitionSizeMultiplier;

        switch(buttonType)
        {
            case MainMenuButtonType.Play:
                buttonAction += Play;
                break;

            case MainMenuButtonType.Options:
                buttonAction += Options;
                break;

            case MainMenuButtonType.Exit:
                buttonAction += Exit;
                break;

            case MainMenuButtonType.BackToMain:
                buttonAction += BackToMain;
                break;
        }
    }

    private void ActivateButton()
    {
        buttonAction?.Invoke();
    }


    private void Play()
    {
        SceneManager.LoadScene(2);
    }

    private void Options()
    {
        mainMenu.ShowOptionsMenu();
    }

    private void Exit()
    {
        Application.Quit();
    }

    private void BackToMain()
    {
        mainMenu.ShowMainMenu();
    }





    public void OnPointerDown(PointerEventData eventData)
    {
        LeanTween.color(rectTransform, clickColor,colorChangeSpeed);
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        ActivateButton();
        LeanTween.color(rectTransform, Color.white, colorChangeSpeed);

        rectTransform.sizeDelta = originalSize;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.size(rectTransform, transitionedSize, transitionSpeed);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.size(rectTransform, originalSize, transitionSpeed);
        LeanTween.color(rectTransform, Color.white, colorChangeSpeed);
    }

   
   
}
