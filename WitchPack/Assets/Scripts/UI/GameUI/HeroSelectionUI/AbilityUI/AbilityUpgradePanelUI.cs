using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AbilityUpgradePanelUI : UIElement, IInit<AbilityUI>
{
    [SerializeField] private TextMeshProUGUI titleTMP;
    [SerializeField] private AbilityUpgradeUI baseAbilityUpgradeUI;
    [SerializeField] private AbilityUpgradeUI[] abilityUpgradesUI;

    private Vector3 _baseAbilityUIPos;
    private AbilityUI _abilityUI;

    public void Init(AbilityUI abilityUI)
    {
        _abilityUI = abilityUI;
        _baseAbilityUIPos = abilityUI.RectTransform.position;
        Show();
    }
    public override void Show()
    {
        var position = rectTransform.position;
        rectTransform.position = new Vector3(_abilityUI.RectTransform.position.x + (_abilityUI.RectTransform.rect.width / 2),position.y,position.z);
        if (abilityUpgradesUI.Length == 3)
        {
            
        }
        else if(abilityUpgradesUI.Length == 2)
        {
            
        }
        base.Show();
    }

    private void Update()
    {
        if (!gameObject.activeSelf || isMouseOver) return;
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0)) Hide();
    }
}
