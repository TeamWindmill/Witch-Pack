using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : UIElement
{
    [SerializeField] private DialogSequence config; //temp for testing
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _textField;
    [SerializeField] private Image _characterSplash;

    private DialogSequence _dialogSequence;
    private bool _mouseClickListen;
    private int _currentDialogBoxIndex = 0;

    private void Start() //temp for testing
    {
        Init(config);
    }

    public void Init(DialogSequence dialogSequence)
    {
        _dialogSequence = dialogSequence;
        SetDialogBox(dialogSequence[_currentDialogBoxIndex]);
        _mouseClickListen = true;
    }

    private void SetDialogBox(DialogBoxConfig boxConfig)
    {
        _nameText.text = boxConfig.Speaker.Name;
        _characterSplash.sprite = boxConfig.Speaker.UnitSprite;
        _textField.text = boxConfig.DialogText;
    }
    private void MoveToNextDialogBox()
    {
        _currentDialogBoxIndex++;
        if (_currentDialogBoxIndex < _dialogSequence.Sequence.Length)
        {
            SetDialogBox(_dialogSequence[_currentDialogBoxIndex]);
        }
        else
        {
            _mouseClickListen = false;
            Hide();
        }
    }
    protected override void Update()
    {
        if (_mouseClickListen)
        {
            if (Input.GetMouseButtonDown(0))
            {
                MoveToNextDialogBox();
            }
        }
    }

    
}
