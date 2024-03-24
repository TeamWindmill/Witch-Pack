using System;
using System.Collections.Generic;
using UnityEngine;


public class TimeControlUIHandler : UIElement
{
    public static TimeControlUIHandler Instance;
    public TimeButtonsUI CurrentTimeButton { get; private set; }

    [SerializeField] private List<TimeButtonsUI> _timeButtons;

    protected override void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else Instance = this;
        base.Awake();
    }

    public void ChangeTimeButton(TimeButtons button)
    {
        foreach (var timeButton in _timeButtons)
        {
            if (timeButton.ButtonType == button)
            {
                timeButton.SetState(true);
            }
        }
    }

    public override void Show()
    {
        foreach (var timeButtonsUI in _timeButtons)
        {
            timeButtonsUI.OnTurnOn += OnButtonPressed;
            if (timeButtonsUI.IsActive)
                CurrentTimeButton = timeButtonsUI;
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
        if (CurrentTimeButton == null)
        {
            CurrentTimeButton = timeButtonsUI;
            return;
        }

        CurrentTimeButton.SetState(false);
        CurrentTimeButton = timeButtonsUI;
    }
}

public enum TimeButtons
{
    Pause,
    Play,
    X2,
    X3,
}