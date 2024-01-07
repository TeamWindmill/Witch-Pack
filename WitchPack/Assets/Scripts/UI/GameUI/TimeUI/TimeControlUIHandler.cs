﻿using System.Collections.Generic;
using UnityEngine;


public class TimeControlUIHandler : BaseUIElement
{
    [SerializeField] private List<TimeButtonsUI> _timeButtons;

    private TimeButtonsUI _currentButton;


    public override void Show()
    {
        foreach (var timeButtonsUI in _timeButtons)
        {
            timeButtonsUI.OnTurnOn += OnButtonPressed;
            if (timeButtonsUI.State == ButtonState.On)
                _currentButton = timeButtonsUI;
        }

        base.Show();
    }


    public override void Hide()
    {
        foreach (var timeButtonsUI in _timeButtons)
            timeButtonsUI.OnTurnOn -= OnButtonPressed;
        base.Hide();
    }

    private void OnButtonPressed(TimeButtonsUI timeButtonsUI)
    {
        if (_currentButton == null)
        {
            _currentButton = timeButtonsUI;
            return;
        }

        _currentButton.ChangeState(ButtonState.Off);
        _currentButton = timeButtonsUI;
    }
}