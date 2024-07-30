using Gameplay.Units.Damage_System;
using Gameplay.Units.Shaman;
using Managers;
using UI.UISystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.GameUI.PartyUI
{
    public class ShamanUIHandler : ClickableUIElement
    {
        [SerializeField] private Image _fill;
        [SerializeField] private Slider _healthBar;
        [SerializeField] private Image _splash;
        [SerializeField] private Image _upgradeFrame;
        [SerializeField] private Image _upgradeArrow;
        [SerializeField, Range(0, 1)] private float spriteDeathAlpha;
        [Space] 
        [SerializeField] private Image _redInjuryImage;
        [SerializeField] private Sprite _deadUnitIcon;

        private Shaman _shaman;


        public void Init(Shaman shaman)
        {
            _shaman = shaman;
            _splash.sprite = _shaman.ShamanConfig.UnitIcon;
            _healthBar.value = 1;
            Color upgradeColor = _upgradeFrame.color;
            _redInjuryImage.fillAmount = 0;
            if (shaman.EnergyHandler.HasSkillPoints)
            {
                upgradeColor.a = 100;
                _upgradeArrow.gameObject.SetActive(true);
            }
            else
            {
                upgradeColor.a = 0;
                _upgradeArrow.gameObject.SetActive(false);
            }
            _upgradeFrame.color = upgradeColor;
            shaman.EnergyHandler.OnShamanUpgrade += OnShamanUpgrade;
            shaman.EnergyHandler.OnShamanLevelUp += OnShamanLevelUp;
            shaman.Damageable.OnHealthChange += OnCurrentChangeHealth;
            shaman.Damageable.OnDeath += ShamanDeathUI;
            OnClickEvent += GoToShaman;
            OnClickEvent += ShowShamanInfo;
            Show();
        }

        private void OnCurrentChangeHealth(int hp,int maxHp)
        {
            float hpRatio = _shaman.Damageable.CurrentHp / (float)_shaman.Damageable.MaxHp;
            _redInjuryImage.fillAmount = 1 - hpRatio;
            _healthBar.value = hpRatio;
            _fill.color = Color.Lerp(Color.red, Color.green, hpRatio);
        }

        public override void Hide()
        {
            _shaman.Damageable.OnHealthChange -= OnCurrentChangeHealth;
            _shaman.Damageable.OnDeath -= ShamanDeathUI;
            OnClickEvent -= GoToShaman;
            OnClickEvent -= ShowShamanInfo;
            base.Hide();
        }

        private void ShamanDeathUI(Damageable arg1, DamageDealer arg2)
        {
            Color upgradeColor = _upgradeFrame.color;
            upgradeColor.a = 0;
            _upgradeFrame.color = upgradeColor;
            _splash.sprite = _deadUnitIcon;
            _redInjuryImage.fillAmount = 0;
        }

        private void OnShamanLevelUp(int obj)
        { 
            if(_shaman.IsDead) return;
            Color upgradeColor = _upgradeFrame.color;
            upgradeColor.a = 100;
            _upgradeFrame.color = upgradeColor;
            _upgradeArrow.gameObject.SetActive(true);
        }

        private void OnShamanUpgrade(bool hasSkillPoints)
        {
            if(_shaman.IsDead) return;
            Color upgradeColor = _upgradeFrame.color;
            if (hasSkillPoints)
            {
                upgradeColor.a = 100;
                _upgradeArrow.gameObject.SetActive(true);
            }
            else
            {
                upgradeColor.a = 0;
                _upgradeArrow.gameObject.SetActive(false);
            }
            _upgradeFrame.color = upgradeColor;
        }

        private void GoToShaman(PointerEventData pointerData)
        {
            if(_shaman.IsDead) return;
            GameManager.CameraHandler.SetCameraPosition(_shaman.transform.position);
        }
        private void ShowShamanInfo(PointerEventData pointerData)
        {
            if(_shaman.IsDead) return;
            _shaman.SetSelectedShaman(pointerData.button);
        }
    }
}