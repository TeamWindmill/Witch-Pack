using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MetaUpgradeIcon<T> : ClickableUIElement //where T : 
{
    public event Action<T> OnUpgrade;

    [SerializeField] private MetaUpgradeIcon<T> childNode;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _amount;
    [SerializeField] private Image _lineImage;
    [SerializeField] private Image _frameImage;
    [SerializeField] private Image _alphaImage;
    [SerializeField] private bool _openAtStart;

    [Space] [BoxGroup("Sprites")] [SerializeField]
    private Sprite upgradeReadyFrameSprite;

    [BoxGroup("Sprites")] [SerializeField] private Sprite defaultFrameSprite;
    [BoxGroup("Sprites")] [SerializeField] private Sprite defaultLineSprite;
    [BoxGroup("Sprites")] [SerializeField] private Sprite upgradedLineSprite;
    public UpgradeState UpgradeState { get; private set; } = UpgradeState.Locked;

    public bool OpenAtStart => _openAtStart;

    private bool _hasSkillPoints;
    protected T _upgrade;


    public virtual void Init(MetaUpgradeConfig upgradeConfig, bool hasSkillPoints)
    {
        _hasSkillPoints = hasSkillPoints;
        _name.text = upgradeConfig.Name;
        _amount.text = upgradeConfig.ValueName;
        ChangeStateVisuals(UpgradeState);
        Show();
    }

    protected override void OnClick(PointerEventData eventData)
    {
        switch (UpgradeState)
        {
            case UpgradeState.Locked:
                break;
            case UpgradeState.Open:
                if (_hasSkillPoints)
                {
                    ChangeStateVisuals(UpgradeState.Upgraded);
                    OnUpgrade?.Invoke(_upgrade);
                    if (childNode != null) childNode.ChangeStateVisuals(UpgradeState.Open);
                }
                break;
            case UpgradeState.Upgraded:
                break;
        }

        base.OnClick(eventData);
    }

    public void ChangeStateVisuals(UpgradeState upgradeState)
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
            case UpgradeState.Upgraded:
                _alphaImage.gameObject.SetActive(false);
                _lineImage.sprite = upgradedLineSprite;
                _frameImage.sprite = defaultFrameSprite;
                break;
        }
    }
}