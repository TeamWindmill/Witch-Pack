using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AbilityUpgradePanelUI : UIElement
{
    [SerializeField] private TextMeshProUGUI titleTMP;
    [SerializeField] private AbilityUpgradeUIButton baseAbilityUpgradeUIButton;
    [SerializeField] private Transform upgrades3Holder;
    [SerializeField] private AbilityUpgradeUIButton[] abilityUpgrades3UI;
    [SerializeField] private Transform upgrades2Holder;
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
        upgrades3Holder.gameObject.SetActive(false);
        upgrades2Holder.gameObject.SetActive(false);
        _abilityUIButton = abilityUIButton;
        _rootAbility = abilityUIButton.RootAbility;
        _abilityUpgrades = _rootAbility.GetUpgrades();
        titleTMP.text = _rootAbility.Name;
        Show();
    }
    public override void Show()
    {
        var position = rectTransform.position;
        rectTransform.position = new Vector3(_abilityUIButton.RectTransform.position.x + (_abilityUIButton.RectTransform.rect.width / 2),position.y,position.z);
        baseAbilityUpgradeUIButton.OnAbilityClick += UpgradeShamanAbility;
        baseAbilityUpgradeUIButton.Init(_rootAbility);
        if (_abilityUpgrades.Count == 3)
        {
            upgrades3Holder.gameObject.SetActive(true);
            for (int i = 0; i < abilityUpgrades3UI.Length; i++)
            {
                abilityUpgrades3UI[i].Init(_abilityUpgrades[i]);
                abilityUpgrades3UI[i].OnAbilityClick += UpgradeShamanAbility;
            }
        }
        else if(_abilityUpgrades.Count == 2)
        {
            upgrades2Holder.gameObject.SetActive(true);
            for (int i = 0; i < abilityUpgrades2UI.Length; i++)
            {
                abilityUpgrades2UI[i].Init(_abilityUpgrades[i]);
                abilityUpgrades2UI[i].OnAbilityClick += UpgradeShamanAbility;
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
        baseAbilityUpgradeUIButton.OnAbilityClick -= UpgradeShamanAbility;
        if (upgrades2Holder.gameObject.activeSelf)
        {
            foreach (var ability in abilityUpgrades2UI)
            {
                ability.OnAbilityClick -= UpgradeShamanAbility;
            }
        }
        else if (upgrades3Holder.gameObject.activeSelf)
        {
            foreach (var ability in abilityUpgrades3UI)
            {
                ability.OnAbilityClick -= UpgradeShamanAbility;
            }
        }
        base.Hide();
    }

    private void Update()
    {
        if (!gameObject.activeSelf || isMouseOver) return;
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0)) Hide();
    }

    private void UpgradeShamanAbility(AbilityUpgradeUIButton abilityUpgradeButton)
    {
        var ability = abilityUpgradeButton.Ability;
        if (!ReferenceEquals(_abilityUIButton.ActiveAbility,null))
        {
            _shaman.UpgradeAbility(_abilityUIButton.ActiveAbility,ability);
            if (_abilityUIButton.ActiveAbility.Upgrades.Length > 0)
            {
                foreach (var upgrade in _abilityUIButton.ActiveAbility.Upgrades)
                {
                    if(upgrade == ability) continue;
                    upgrade.ChangeUpgradeState(AbilityUpgradeState.Locked);
                }
            }
        }
        else
        {
            _shaman.LearnAbility(ability);
        }
        var caster = _shaman.GetCasterFromAbility(ability);
        _abilityUIButton.Init(_rootAbility,ability,caster);
        Show();
    }
}
