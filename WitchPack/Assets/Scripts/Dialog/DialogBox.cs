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
    [SerializeField] private GameObject _nameTextBox;

    private DialogSequence _dialogSequence;
    private bool _mouseClickListen;
    private int _currentDialogBoxIndex = 0;

    private Color _transparentImageColor = new Color(1,1,1,0);
    private Color _opaqueImageColor = new Color(1,1,1,1);


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
        _textField.text = boxConfig.DialogText;

        if(boxConfig.IsCharacterDialog)
        {
            _nameTextBox.gameObject.SetActive(true);
            _nameText.text = boxConfig.Speaker.Name;
            _characterSplash.sprite = boxConfig.Speaker.UnitSprite;
            _characterSplash.color = _opaqueImageColor;
        }        
        else
        {
            _nameTextBox.gameObject.SetActive(false);
            _characterSplash.color = _transparentImageColor;
        }
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
