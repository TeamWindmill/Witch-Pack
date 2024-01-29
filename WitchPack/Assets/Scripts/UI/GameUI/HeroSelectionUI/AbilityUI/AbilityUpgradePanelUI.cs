using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AbilityUpgradePanelUI : UIElement, IInit<AbilityUI,Shaman>
{
    [SerializeField] private TextMeshProUGUI titleTMP;
    [SerializeField] private AbilityUpgradeUI baseAbilityUpgradeUI;
    [SerializeField] private Transform upgrades3Holder;
    [SerializeField] private AbilityUpgradeUI[] abilityUpgrades3UI;
    [SerializeField] private Transform upgrades2Holder;
    [SerializeField] private AbilityUpgradeUI[] abilityUpgrades2UI;

    private Vector3 _baseAbilityUIPos;
    private AbilityUI _abilityUI;
    private BaseAbility _baseAbility;
    private BaseAbility[] _abilityUpgrades;
    private Shaman _shaman;

    private void Start()
    {
        baseAbilityUpgradeUI.OnAbilityClick += UpgradeShamanAbility;
    }

    public void Init(AbilityUI abilityUI, Shaman shaman)
    {
        _shaman = shaman;
        upgrades3Holder.gameObject.SetActive(false);
        upgrades2Holder.gameObject.SetActive(false);
        _abilityUI = abilityUI;
        _baseAbility = abilityUI.BaseAbility;
        _baseAbilityUIPos = abilityUI.RectTransform.position;
        _abilityUpgrades = abilityUI.BaseAbility.Upgrades;
        Show();
    }
    public override void Show()
    {
        var position = rectTransform.position;
        rectTransform.position = new Vector3(_abilityUI.RectTransform.position.x + (_abilityUI.RectTransform.rect.width / 2),position.y,position.z);
        baseAbilityUpgradeUI.Init(_baseAbility);
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

    private void UpgradeShamanAbility(BaseAbility ability)
    {
        _shaman.UpgradeAbility(_baseAbility,ability);
    }
}
