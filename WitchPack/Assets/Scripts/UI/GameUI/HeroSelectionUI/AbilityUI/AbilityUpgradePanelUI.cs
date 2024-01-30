using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AbilityUpgradePanelUI : UIElement, IInit<AbilityUIButton,Shaman>
{
    [SerializeField] private TextMeshProUGUI titleTMP;
    [SerializeField] private AbilityUpgradeUIButton baseAbilityUpgradeUIButton;
    [SerializeField] private Transform upgrades3Holder;
    [SerializeField] private AbilityUpgradeUIButton[] abilityUpgrades3UI;
    [SerializeField] private Transform upgrades2Holder;
    [SerializeField] private AbilityUpgradeUIButton[] abilityUpgrades2UI;

    private AbilityUIButton _abilityUIButton;
    private BaseAbility _baseAbility;
    private BaseAbility[] _abilityUpgrades;

    public void Init(AbilityUIButton abilityUIButton)
    {
        upgrades3Holder.gameObject.SetActive(false);
        upgrades2Holder.gameObject.SetActive(false);
        _abilityUIButton = abilityUIButton;
        _baseAbility = abilityUIButton.CastingHandler;
        _abilityUpgrades = abilityUIButton.CastingHandler.Upgrades;
        titleTMP.text = _baseAbility.Name;
        baseAbilityUpgradeUIButton.OnAbilityClick += UpgradeShamanAbility;
        Show();
    }
    public override void Show()
    {
        var position = rectTransform.position;
        rectTransform.position = new Vector3(_abilityUIButton.RectTransform.position.x + (_abilityUIButton.RectTransform.rect.width / 2),position.y,position.z);
        
        baseAbilityUpgradeUIButton.Init(_baseAbility);
        if (_abilityUpgrades.Length == 3)
        {
            upgrades3Holder.gameObject.SetActive(true);
            for (int i = 0; i < abilityUpgrades3UI.Length; i++)
            {
                abilityUpgrades3UI[i].Init(_abilityUpgrades[i]);
            }
        }
        else if(_abilityUpgrades.Length == 2)
        {
            upgrades2Holder.gameObject.SetActive(true);
            for (int i = 0; i < abilityUpgrades2UI.Length; i++)
            {
                abilityUpgrades2UI[i].Init(_abilityUpgrades[i]);
            }
        }
        else
        {
            Debug.LogError("invalid number of upgrades");
            return;
        }
        base.Show();
    }

    private void Update()
    {
        if (!gameObject.activeSelf || isMouseOver) return;
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0)) Hide();
    }

    private void UpgradeShamanAbility(AbilityUpgradeUIButton ability)
    {
        //_shaman.UpgradeAbility(_baseAbility,ability);
    }
}
