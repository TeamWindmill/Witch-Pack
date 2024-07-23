using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AbilitySkillTreeDetails : UIElement<AbilitySO>
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private AbilityUpgradeUIButton baseAbilityUpgradeUIButton;
    [SerializeField] private Transform upgrades3Holder;
    [SerializeField] private Transform upgrades3BG;
    [SerializeField] private AbilityUpgradeUIButton[] abilityUpgrades3UI;
    [SerializeField] private Transform upgrades2Holder;
    [SerializeField] private Transform upgrades2BG;
    [SerializeField] private AbilityUpgradeUIButton[] abilityUpgrades2UI;
    
    private List<AbilitySO> _abilityUpgrades;

    public override void Init(AbilitySO data)
    {
        Hide();
        title.text = data.Name;
        _abilityUpgrades = data.GetUpgrades();
        if (_abilityUpgrades.Count == 3)
        {
            title.rectTransform.anchoredPosition = new Vector2(0,-30);
            upgrades3BG.gameObject.SetActive(true);
            upgrades3Holder.gameObject.SetActive(true);
        }
        else if (_abilityUpgrades.Count == 2)
        {
            title.rectTransform.anchoredPosition = new Vector2(0,-120);
            upgrades2BG.gameObject.SetActive(true);
            upgrades2Holder.gameObject.SetActive(true);
        }
        base.Init(data);
        Show();
    }

    public override void Hide()
    {
        upgrades3BG.gameObject.SetActive(false);
        upgrades3Holder.gameObject.SetActive(false);
        upgrades2BG.gameObject.SetActive(false);
        upgrades2Holder.gameObject.SetActive(false);
        base.Hide();
    }
}
