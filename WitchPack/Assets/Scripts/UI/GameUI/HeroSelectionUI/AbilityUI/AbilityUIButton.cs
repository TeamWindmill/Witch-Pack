using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityUIButton : ClickableUIElement
{
    public event Action<AbilityUIButton> OnAbilityClick;
    public Ability RootAbility => rootAbility;
    public Ability ActiveAbility => activeAbility;
    public AbilityCaster Caster => _caster;
    
    [SerializeField] private Image _abilitySpriteRenderer;
    [SerializeField] private Image _cooldownSpriteRenderer;
    [SerializeField] private Image _frameSpriteRenderer;
    [Space] 
    [SerializeField] private Sprite upgradeReadyFrameSprite;
    [SerializeField] private Sprite defaultFrameSprite;
    
    private AbilityCaster _caster;
    private Ability rootAbility;
    private Ability activeAbility;
    private float _abilityCd;
    private float _abilityLastCast;



    private bool _activeCd;
    
    public void Init(Ability rootAbility,Ability activeAbility = null, AbilityCaster caster = null, bool hasSkillPoints = false)
    {
        
        this.rootAbility = rootAbility;
        if (ReferenceEquals(activeAbility,null))
        {
            _windowInfo.Name = rootAbility.BaseConfig.Name;
            _windowInfo.Discription = rootAbility.BaseConfig.Discription;
            _frameSpriteRenderer.sprite = hasSkillPoints ? upgradeReadyFrameSprite : defaultFrameSprite;
            _abilitySpriteRenderer.sprite = hasSkillPoints ? rootAbility.BaseConfig.UpgradeIcon : rootAbility.BaseConfig.DisabledIcon;
            this.activeAbility = null;
            SetCooldownData(0);
        }
        else
        {
            this.activeAbility = activeAbility;
            _windowInfo.Name = activeAbility.BaseConfig.Name;
            _windowInfo.Discription = activeAbility.BaseConfig.Discription;
            if (this.activeAbility.Upgrades.Count > 0)
            {
                _frameSpriteRenderer.sprite = hasSkillPoints ? upgradeReadyFrameSprite : defaultFrameSprite;
                _abilitySpriteRenderer.sprite = hasSkillPoints ? activeAbility.BaseConfig.UpgradeIcon : activeAbility.BaseConfig.DefaultIcon;
            }
            else
            {
                _frameSpriteRenderer.sprite = defaultFrameSprite;
                _abilitySpriteRenderer.sprite = activeAbility.BaseConfig.DefaultIcon;
            }
            
            if (caster is not null)
            {
                _caster = caster;
                SetCooldownData(castHandler: caster);
                _caster.OnCast += UpdateOnCast;
            }
            else SetCooldownData(0);
        }
        Show();
    }

    public override void Hide()
    {
        if (_caster is not null) _caster.OnCast -= UpdateOnCast;
        OnAbilityClick = null;
        SetCooldownData();
        base.Hide();
    }

    protected override void Update()
    {
        UpdateCooldownFillAmount();
    }

    protected override void OnClick(PointerEventData eventData)
    {
        OnAbilityClick?.Invoke(this);
    }

    //set null for disabling the cooldown
    private void SetCooldownData(float fillAmount = 0, AbilityCaster castHandler = null)
    {
        if (ReferenceEquals(castHandler, null))
        {
            _cooldownSpriteRenderer.fillAmount = fillAmount;
            _activeCd = false;
        }
        else
        {
            _abilityCd = castHandler.Ability.CastingConfig.Cd;
            _abilityLastCast = castHandler.LastCast;
            _activeCd = true;
        }
    }

    private void UpdateCooldownFillAmount()
    {
        if (!_activeCd) return;
        
        float ratio = 0;
        if (_abilityLastCast > 0)
        {
            ratio = (GAME_TIME.GameTime - _abilityLastCast) / _abilityCd;
            if (ratio >= 1) ratio = 0;
        }
        _cooldownSpriteRenderer.fillAmount = ratio;
    }
    private void UpdateOnCast(AbilityCaster caster)
    {
        _abilityLastCast = caster.LastCast;
    }
}