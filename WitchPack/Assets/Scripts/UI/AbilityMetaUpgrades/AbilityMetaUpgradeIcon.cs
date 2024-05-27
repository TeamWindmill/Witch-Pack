using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityMetaUpgradeIcon : ClickableUIElement
{
    public event Action<AbilityUpgrade> OnUpgrade;
    
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
    private AbilityStatUpgradeConfig _abilityStatUpgradeConfig;
    private AbilityUpgrade _abilityUpgrade;


    public void Init(AbilityStatUpgradeConfig abilityUpgradeConfig,AbilitySO[] abilitiesToUpgrade, bool hasSkillPoints)
    {
        
        _abilityStatUpgradeConfig = abilityUpgradeConfig;
        _hasSkillPoints = hasSkillPoints;
        _name.text = abilityUpgradeConfig.Name;
        char factor = abilityUpgradeConfig.Factor == Factor.Add ? '+' : '-';
        _amount.text = factor + abilityUpgradeConfig.AbilityStatValue.ToString();


        _abilityUpgrade = new AbilityUpgrade(abilityUpgradeConfig,abilitiesToUpgrade);
        ChangeStateVisuals(UpgradeState);
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
                    ChangeStateVisuals(AbilityUpgradeState.Upgraded);
                    OnUpgrade?.Invoke(_abilityUpgrade);
                    if(childNode != null) childNode.ChangeStateVisuals(AbilityUpgradeState.Open);
                }
                break;
            case AbilityUpgradeState.Upgraded:
                break;
        }
        base.OnClick(eventData);
    }

    public void ChangeStateVisuals(AbilityUpgradeState upgradeState)
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
