using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerDownHandler,IPointerEnterHandler,IPointerExitHandler,IPointerUpHandler
{

    private enum MainMenuButtonType {Play,Options,Exit }
    [SerializeField] private MainMenuButtonType buttonType;

    private delegate void MainMenuAction();
    private MainMenuAction buttonAction;


    private void Start()
    {
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
        }
    }

    private void ActivateButton()
    {
        buttonAction?.Invoke();
    }


    private void Play()
    {
        print("Load map");
    }

    private void Options()
    {
        print("Go to options");
    }

    private void Exit()
    {
        print("Exit game");
    }





    public void OnPointerDown(PointerEventData eventData)
    {
        // click animation
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        ActivateButton();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
       // hover animation
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // back to normal
    }

   
   
}
