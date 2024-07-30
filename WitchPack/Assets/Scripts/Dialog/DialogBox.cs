using System;
using TMPro;
using Tools.Time;
using UI.UISystem;
using UnityEngine;
using UnityEngine.UI;

namespace Dialog
{
    public class DialogBox : UIElement
    {
        public static DialogBox Instance { get; private set; }

        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _textField;
        [SerializeField] private Image _characterSplash;
        [SerializeField] private GameObject _nameTextBox;
        [SerializeField] private float _clickListenDelay;

        private DialogSequence _dialogSequence;
        private Action _dialogEndTrigger;
        private bool _mouseClickListen;
        private int _currentDialogBoxIndex = 0;

        private Color _transparentImageColor = new Color(1,1,1,0);
        private Color _opaqueImageColor = new Color(1,1,1,1);
        protected override void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(Instance);
            }
            else Instance = this;
            base.Awake();
        }
        public void SetDialogSequence(DialogSequence dialogSequence, Action dialogEndTrigger = null)
        {
            _currentDialogBoxIndex = 0;
            _dialogSequence = dialogSequence;
            _dialogEndTrigger = dialogEndTrigger;
        }

        public override void Show()
        {
            SetDialogBox(_dialogSequence[_currentDialogBoxIndex]);
            TimerManager.AddTimer(_clickListenDelay, () => _mouseClickListen = true);
            base.Show();
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
                _dialogEndTrigger?.Invoke();
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
}
