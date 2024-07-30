using System;
using Gameplay.Units;
using Plugins.Demigiant.DOTween.Modules;
using UI.UISystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.MapUI.PartySelectionUI
{
    public class PackIcon : ClickableUIElement
    {
        public event Action<int> OnIconLeftClick;

        public ShamanSaveData ShamanSaveData { get; private set; }
        public int Index { get; private set; }
        public bool Locked { get; private set; }
        public bool Assigned { get; private set; }
        [SerializeField] private Image _splashRenderer;
        [SerializeField] private Image _bgRenderer;
        [SerializeField] private Image _alphaRenderer;
        [SerializeField] private Color _flashColor;
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Sprite _lockIcon;
        [SerializeField] private float _flashDuration;


        public void Init(int index)
        {
            Index = index;
            UnassignShaman();
            ToggleAlpha(false);
            Locked = false;
        }

        public override void Refresh()
        {
            if (!Assigned) Init(Index);
        }

        public void ToggleAlpha(bool state)
        {
            _alphaRenderer.gameObject.SetActive(state);
        }

        public void UnassignShaman()
        {
            ShamanSaveData = null;
            SetSplashAlpha(0);
            Assigned = false;
        }


        public void AssignShaman(ShamanSaveData shamanSaveData)
        {
            ShamanSaveData = shamanSaveData;
            _splashRenderer.sprite = shamanSaveData.Config.UnitIcon;
            SetSplashAlpha(1);
            Assigned = true;
        }

        public void FlashInRed()
        {
            _bgRenderer.DOColor(_flashColor, _flashDuration).onComplete += () => _bgRenderer.DOColor(_defaultColor, _flashDuration);
        }

        public void ToggleLockIcon(bool state)
        {
            Locked = state;
            if (state)
            {
                SetSplashAlpha(1);
                _splashRenderer.sprite = _lockIcon;
            }
            else
            {
                SetSplashAlpha(0);
            }
        }

        protected override void OnClick(PointerEventData eventData)
        {
            if(Locked) return;
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                OnIconLeftClick?.Invoke(Index);
            }
            base.OnClick(eventData);
        }

        private void SetSplashAlpha(float value)
        {
            var color = _splashRenderer.color;
            color.a = value;
            _splashRenderer.color = color;
        }
    }
}