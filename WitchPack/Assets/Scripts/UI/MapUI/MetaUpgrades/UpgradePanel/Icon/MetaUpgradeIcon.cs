using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MetaUpgradeIcon<T> : ClickableUIElement
{
    public event Action<T> OnUpgrade;
    public UpgradeState UpgradeState { get; private set; }
    public bool OpenAtStart => _openAtStart;
    public MetaUpgradeConfig UpgradeConfig => _upgradeConfig;
    
    [SerializeField] private MetaUpgradeIcon<T> childNode;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _amount;
    [SerializeField] private TextMeshProUGUI _cost;
    [SerializeField] private Image _lineImage;
    [SerializeField] private Image _frameImage;
    [SerializeField] private Image _alphaImage;
    [SerializeField] private bool _openAtStart;

    [Space] [BoxGroup("Sprites")] [SerializeField]
    private Sprite upgradeReadyFrameSprite;

    [BoxGroup("Sprites")] [SerializeField] private Sprite defaultFrameSprite;
    [BoxGroup("Sprites")] [SerializeField] private Sprite defaultLineSprite;
    [BoxGroup("Sprites")] [SerializeField] private Sprite upgradedLineSprite;
    

    private int _availableSkillPoints;
    protected T Upgrade;
    private MetaUpgradeConfig _upgradeConfig;


    public virtual void Init(MetaUpgradeConfig upgradeConfig, int availableSkillPoints)
    {
        UpgradeState = UpgradeState.Locked;
        _upgradeConfig = upgradeConfig;
        _availableSkillPoints = availableSkillPoints;
        _cost.text = upgradeConfig.SkillPointsCost.ToString();
        if (!upgradeConfig.NotWorking)
        {
            _name.text = upgradeConfig.Name;
            _amount.text = upgradeConfig.ValueName;
        }
        else
        {
            _name.text = "WIP";
            _amount.text = "";
        }
        
        ChangeState(UpgradeState);
        Show();
    }

    protected override void OnClick(PointerEventData eventData)
    {
        switch (UpgradeState)
        {
            case UpgradeState.Locked:
                break;
            case UpgradeState.Open:
                if (_availableSkillPoints >= _upgradeConfig.SkillPointsCost)
                {
                    ChangeState(UpgradeState.Upgraded);
                    OnUpgrade?.Invoke(Upgrade);
                }
                break;
            case UpgradeState.Upgraded:
                break;
        }
        base.OnClick(eventData);
    }

    public void ChangeState(UpgradeState upgradeState)
    {
        UpgradeState = upgradeState;
        switch (upgradeState)
        {
            case UpgradeState.Locked:
                _alphaImage.gameObject.SetActive(true);
                _lineImage.sprite = defaultLineSprite;
                _frameImage.sprite = defaultFrameSprite;
                break;
            case UpgradeState.Open:
                if (_availableSkillPoints >= _upgradeConfig.SkillPointsCost)
                {
                    if(_upgradeConfig.NotWorking) return;
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
            case UpgradeState.Upgraded:
                _alphaImage.gameObject.SetActive(false);
                _lineImage.sprite = upgradedLineSprite;
                _frameImage.sprite = defaultFrameSprite;
                if (childNode != null) childNode.ChangeState(UpgradeState.Open);
                break;
        }
    }
}