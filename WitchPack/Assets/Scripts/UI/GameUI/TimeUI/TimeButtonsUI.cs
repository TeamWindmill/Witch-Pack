using System;
using GameTime;
using UI.UISystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.GameUI.TimeUI
{
    public class TimeButtonsUI : ClickableUIElement
    {
        public event Action<TimeButtonsUI> OnTurnOn;
    
        [SerializeField] private Image image;
        [SerializeField] private Color onColor;
        [SerializeField] private Color offColor;
        [SerializeField] private TimeButtons buttonType;
        [SerializeField] private float time;
        [SerializeField] private bool startState = false;

        private bool _isActive;

        public bool IsActive => _isActive;
        public TimeButtons ButtonType => buttonType;

        private void Start()
        {
            _isActive = startState;
            SetState(_isActive);
        }

        protected override void OnClick(PointerEventData eventData)
        {
            base.OnClick(eventData);
            if(_isActive) return;
            On();
        }

        public void SetState(bool state)
        {
            if (state) On();
            else Off();
        }

        private void On()
        {
            GAME_TIME.SetTimeStep(time);
            image.color = onColor;
            _isActive = true;
            OnTurnOn?.Invoke(this);
        }

        private void Off()
        {
            image.color = offColor;
            _isActive = false;
        }
    }
}