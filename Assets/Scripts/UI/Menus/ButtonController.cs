using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ButtonType
{
    StartGame,
    EndGame
}

[RequireComponent(typeof(Button))]
public class ButtonController : MonoBehaviour
{
    public ButtonType buttonType;

    CanvasManager _canvasManager;
    Button _menuButton;
    
    private void Start()
    {
        _menuButton = GetComponent<Button>();
        _menuButton.onClick.AddListener(OnButtonClicked);
        _canvasManager = CanvasManager.GetInstance();
    }

    void OnButtonClicked()
    {
        switch (buttonType)
        {
            case ButtonType.EndGame:
                _canvasManager.SwitchCanvas(CanvasType.Stats);
                break;
            default:
               break;
        }
    }
}