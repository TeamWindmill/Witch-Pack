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
    [SerializeField] private Image _alphaImage;
    [SerializeField] private bool _openAtStart;
    [Space] 
    [BoxGroup("Sprites")][SerializeField] private Sprite upgradeReadyFrameSprite;
    [BoxGroup("Sprites")][SerializeField] private Sprite defaultFrameSprite;
    [BoxGroup("Sprites")][SerializeField] private Sprite defaultLineSprite;
    [BoxGroup("Sprites")][SerializeField] private Sprite upgradedLineSprite;
    public AbilityUpgradeState UpgradeState { get; private set; } = AbilityUpgradeState.Locked;

    public bool OpenAtStart => _openAtStart;

    private bool _hasSkillPoints;

    public void Init(AbilityStatUpgrade abilityUpgrade, bool hasSkillPoints)
    {
        _hasSkillPoints = hasSkillPoints;
        _name.text = abilityUpgrade.Name;
        char factor = abilityUpgrade.Factor == Factor.Add ? '+' : '-';
        _amount.text = factor + abilityUpgrade.AbilityStatValue.ToString();
        ChangeState(UpgradeState);
        Show();
    }

    protected override void OnClick(PointerEventData eventData)
    {
        switch (UpgradeState)
        {
            case AbilityUpgradeState.Locked:
                break;
            case AbilityUpgradeState.Open:
                if (_hasSkillPoints)
                {
                    ChangeState(AbilityUpgradeState.Upgraded);
                    if(childNode != null) childNode.ChangeState(AbilityUpgradeState.Open);
                }
                break;
            case AbilityUpgradeState.Upgraded:
                break;
        }
        base.OnClick(eventData);
    }

    public void ChangeState(AbilityUpgradeState upgradeState)
    {
        UpgradeState = upgradeState;
        switch (upgradeState)
        {
            case AbilityUpgradeState.Locked:
                _alphaImage.gameObject.SetActive(true);
                _lineImage.sprite = defaultLineSprite;
                _frameImage.sprite = defaultFrameSprite;
                break;
            case AbilityUpgradeState.Open:
                if (_hasSkillPoints)
                {
                    _alphaImage.gameObject.SetActive(false);
                    _lineImage.sprite = defaultLineSprite;
                    _frameImage.sprite = upgradeReadyFrameSprite;
                }
                else
                {
                    _alphaImage.gameObject.SetActive(true);
                    _lineImage.sprite = defaultLineSprite;
                    _frameImage.sprite = defaultLineSprite;
                }
                break;
            case AbilityUpgradeState.Upgraded:
                _alphaImage.gameObject.SetActive(false);
                _lineImage.sprite = upgradedLineSprite;
                _frameImage.sprite = defaultFrameSprite;
                break;
        }
    }
}
