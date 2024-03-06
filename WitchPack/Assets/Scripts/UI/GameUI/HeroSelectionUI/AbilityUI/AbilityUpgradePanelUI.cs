using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AbilityUpgradePanelUI : UIElement
{
    public event Action OnAbilityUpgrade;
    [SerializeField] private TextMeshProUGUI titleTMP;
    [SerializeField] private AbilityUpgradeUIButton baseAbilityUpgradeUIButton;
    [SerializeField] private Transform upgrades3Holder;
    [SerializeField] private Transform upgrades3BG;
    [SerializeField] private AbilityUpgradeUIButton[] abilityUpgrades3UI;
    [SerializeField] private Transform upgrades2Holder;
    [SerializeField] private Transform upgrades2BG;
    [SerializeField] private AbilityUpgradeUIButton[] abilityUpgrades2UI;

    private AbilityUIButton _abilityUIButton;
    private BaseAbility _rootAbility;
    private BaseAbility _activeAbility;
    private List<BaseAbility> _abilityUpgrades;
    private Shaman _shaman;

    public void SetShaman(Shaman shaman)
    {
        _shaman = shaman;
    }

    public void Init(AbilityUIButton abilityUIButton)
    {
        _abilityUIButton = abilityUIButton;
        _rootAbility = abilityUIButton.RootAbility;
        _abilityUpgrades = _rootAbility.GetUpgrades();
        titleTMP.text = abilityUIButton.RootAbility is Passive ? "Passive" : _rootAbility.Name;
        baseAbilityUpgradeUIButton.OnAbilityClick += UpgradeShamanAbility;
        if (_abilityUpgrades.Count == 3)
        {
            foreach (var upgrade in abilityUpgrades3UI)
            {
                upgrade.OnAbilityClick += UpgradeShamanAbility;
            }
        }
        else if (_abilityUpgrades.Count == 2)
        {
            foreach (var upgrade in abilityUpgrades2UI)
            {
                upgrade.OnAbilityClick += UpgradeShamanAbility;
            }
        }

        Show();
    }

    public override void Show()
    {
        var position = rectTransform.position;
        rectTransform.position = new Vector3(_abilityUIButton.RectTransform.position.x + (_abilityUIButton.RectTransform.rect.width / 2), position.y, position.z);

        var shamanHasSkillPoints = _shaman.EnergyHandler.AvailableSkillPoints > 0;
        baseAbilityUpgradeUIButton.Init(_rootAbility,shamanHasSkillPoints);
        if (_abilityUpgrades.Count == 3)
        {
            titleTMP.rectTransform.anchoredPosition = new Vector2(0,-30);
            upgrades3BG.gameObject.SetActive(true);
            upgrades3Holder.gameObject.SetActive(true);
            for (int i = 0; i < abilityUpgrades3UI.Length; i++)
            {
                abilityUpgrades3UI[i].Init(_abilityUpgrades[i],shamanHasSkillPoints);
            }
        }
        else if (_abilityUpgrades.Count == 2)
        {
            titleTMP.rectTransform.anchoredPosition = new Vector2(0,-120);
            upgrades2BG.gameObject.SetActive(true);
            upgrades2Holder.gameObject.SetActive(true);
            for (int i = 0; i < abilityUpgrades2UI.Length; i++)
            {
                abilityUpgrades2UI[i].Init(_abilityUpgrades[i],shamanHasSkillPoints);
            }
        }
        else
        {
            Debug.LogError("invalid number of upgrades");
            return;
        }

        base.Show();
    }

    public override void Hide()
    {
        baseAbilityUpgradeUIButton.Hide();
        foreach (var ability in abilityUpgrades2UI)
        {
            ability.Hide();
        }
        upgrades2Holder.gameObject.SetActive(false);
        upgrades2BG.gameObject.SetActive(false);
        foreach (var ability in abilityUpgrades3UI)
        {
            ability.Hide();
        }
        upgrades3Holder.gameObject.SetActive(false);
        upgrades3BG.gameObject.SetActive(false);
        InformationWindow.Instance.Hide();
        base.Hide();
        if(_shaman is not null && _abilityUIButton is not null) AbilitiesHandlerUI.UpdateButton(_shaman,_abilityUIButton);
    }

    private void Update()
    {
        if (!gameObject.activeSelf || isMouseOver) return;
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0)) Hide();
    }

    private void UpgradeShamanAbility(AbilityUpgradeUIButton abilityUpgradeButton)
    {
        var ability = abilityUpgradeButton.Ability;
        if (!ReferenceEquals(_abilityUIButton.ActiveAbility, null))
        {
            //Debug.Log($"upgraded {ability.name}");
            _shaman.UpgradeAbility(_abilityUIButton.ActiveAbility, ability);
            if (_abilityUIButton.ActiveAbility.Upgrades.Length > 0)
            {
                foreach (var upgrade in _abilityUIButton.ActiveAbility.Upgrades)
                {
                    if (upgrade == ability) continue;
                    upgrade.ChangeUpgradeState(AbilityUpgradeState.Locked);
                }
            }
        }
        else
        {
            _shaman.LearnAbility(ability);
        }

        _shaman.EnergyHandler.TryUseSkillPoint();
        var caster = _shaman.GetCasterFromAbility(ability);
        OnAbilityUpgrade?.Invoke();
        _abilityUIButton.Init(_rootAbility, ability, caster);
        Show();
    }
}