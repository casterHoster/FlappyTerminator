using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameScreen : Window
{
    public Action RestartButtonClicked;

    private void Awake()
    {
        WindowGroup.alpha = 0f;
    }

    public override void Close()
    {
        WindowGroup.alpha = 0f;
        ActionButton.interactable = false;
    }

    public override void Open()
    {
        WindowGroup.alpha = 1f;
        ActionButton.interactable = true;
    }

    protected override void OnButtonClick()
    {
        RestartButtonClicked?.Invoke();
    }
}
