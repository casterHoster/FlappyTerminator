using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : Window
{
    public Action PlayButtonClicked;

    public override void Close()
    {
        WindowGroup.alpha = 0f;
        ActionButton.interactable = false;
    }

    public override void Open()
    {
        WindowGroup.alpha = 1f;
        ActionButton.interactable= true;
    }

    protected override void OnButtonClick()
    {
        throw new System.NotImplementedException();
    }
}
