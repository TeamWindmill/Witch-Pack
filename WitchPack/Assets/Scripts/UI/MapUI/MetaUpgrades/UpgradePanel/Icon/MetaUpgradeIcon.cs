using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MetaUpgradeIcon<T> : ClickableUIElement
{
    public event Action<T> OnUpgrade;
    public Action<int,AbilitySO,AbilitySO[]> OnSelect;
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
    [SerializeField] private Image _selectedFrameImage;
    [SerializeField] private Image _lockedAlphaImage;
    [SerializeField] private bool _openAtStart;
    [SerializeField] private float _holdClickVisualsStartDelay = 0.1f;
    [Space] 
    [BoxGroup("Sprites")] [SerializeField] private Sprite upgradeReadyFrameSprite;
    [BoxGroup("Sprites")] [SerializeField] private Sprite defaultFrameSprite;
    [BoxGroup("Sprites")] [SerializeField] private Sprite defaultLineSprite;
    [BoxGroup("Sprites")] [SerializeField] private Sprite upgradedLineSprite;
    

    private int _availableSkillPoints;
    public T Upgrade { get; protected set; }
    private MetaUpgradeConfig _upgradeConfig;
    protected int _panelIndex;
    protected bool CanPurchaseUpgrade => _availableSkillPoints >= _upgradeConfig.SkillPointsCost;


    public virtual void Init(int index, MetaUpgradeConfig upgradeConfig, int availableSkillPoints)
    {
        _panelIndex = index;
        UpgradeState = UpgradeState.Locked;
        _upgradeConfig = upgradeConfig;
        _availableSkillPoints = availableSkillPoints;
        _cost.text = upgradeConfig.SkillPointsCost.ToString();
        _selectedFrameImage.gameObject.SetActive(false);
        ToggleAlpha(false);
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

    public void SelectIcon(bool state)
    {
        _selectedFrameImage.gameObject.SetActive(state);
    }

    protected override void OnHoldClick()
    {
        base.OnHoldClick();
        if (CanPurchaseUpgrade && UpgradeState == UpgradeState.Open)
        {
            OnUpgrade?.Invoke(Upgrade);
            ToggleAlpha(false);
            //ChangeState(UpgradeState.Upgraded);
        }
    }

    public void ChangeState(UpgradeState upgradeState)
    {
        UpgradeState = upgradeState;
        switch (upgradeState)
        {
            case UpgradeState.Locked:
                _lockedAlphaImage.gameObject.SetActive(true);
                _lineImage.sprite = defaultLineSprite;
                _frameImage.sprite = defaultFrameSprite;
                break;
            case UpgradeState.Open:
                if (CanPurchaseUpgrade)
                {
                    if(_upgradeConfig.NotWorking) return;
                    _lockedAlphaImage.gameObject.SetActive(false);
                    _lineImage.sprite = defaultLineSprite;
                    _frameImage.sprite = upgradeReadyFrameSprite;
                }
                else
                {
                    _lockedAlphaImage.gameObject.SetActive(true);
                    _lineImage.sprite = defaultLineSprite;
                    _frameImage.sprite = defaultFrameSprite;
                }

                break;
            case UpgradeState.Upgraded:
                _lockedAlphaImage.gameObject.SetActive(false);
                _lineImage.sprite = upgradedLineSprite;
                _frameImage.sprite = defaultFrameSprite;
                if (childNode != null) childNode.ChangeState(UpgradeState.Open);
                break;
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        if(UpgradeState != UpgradeState.Open) return;
        ToggleAlpha(true);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        if(UpgradeState != UpgradeState.Open) return;
        ToggleAlpha(false);
    }

    protected override void Update()
    {
        base.Update();
        if(UpgradeState != UpgradeState.Open) return;
        if(HoldClickTimer < _holdClickVisualsStartDelay) return;
        if (CanPurchaseUpgrade && PointerDown)
        {
            var ratio = Mathf.InverseLerp(_holdClickVisualsStartDelay,holdClickSpeed, HoldClickTimer);
            _alphaImage.fillAmount = ratio;
        }
    }

    private void ToggleAlpha(bool state)
    {
        _alphaImage.fillAmount = 0;
        _alphaImage.gameObject.SetActive(state);
    }
}