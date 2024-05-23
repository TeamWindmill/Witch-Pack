using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityMetaUpgradeIcon : ClickableUIElement
{
    [SerializeField] private AbilityMetaUpgradeIcon childNode;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _amount;
    [SerializeField] private Image _lineImage;
    [SerializeField] private Image _frameImage;
    [Space] 
    [BoxGroup("Sprites")][SerializeField] private Sprite upgradeReadyFrameSprite;
    [BoxGroup("Sprites")][SerializeField] private Sprite defaultFrameSprite;
    [BoxGroup("Sprites")][SerializeField] private Sprite defaultLineSprite;
    [BoxGroup("Sprites")][SerializeField] private Sprite upgradedLineSprite;
    public AbilityUpgradeState UpgradeState { get; private set; } = AbilityUpgradeState.Locked;

    public void Init(AbilityStatUpgrade abilityUpgrade)
    {
        _name.text = abilityUpgrade.Name;
        char factor = abilityUpgrade.Factor == Factor.Add ? '+' : '-';
        _amount.text = factor + abilityUpgrade.AbilityStatValue.ToString();
        Show();
    }

    protected override void OnClick(PointerEventData eventData)
    {
        switch (UpgradeState)
        {
            case AbilityUpgradeState.Locked:
                break;
            case AbilityUpgradeState.Open:
                UpgradeState = AbilityUpgradeState.Upgraded;
                childNode.UpgradeState = AbilityUpgradeState.Open;
                
                break;
            case AbilityUpgradeState.Upgraded:
                break;
        }
        base.OnClick(eventData);
    }

    private void ChangeState(AbilityUpgradeState upgradeState)
    {
        switch (upgradeState)
        {
            case AbilityUpgradeState.Locked:
                break;
            case AbilityUpgradeState.Open:
                break;
            case AbilityUpgradeState.Upgraded:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(upgradeState), upgradeState, null);
        }
    }
}
