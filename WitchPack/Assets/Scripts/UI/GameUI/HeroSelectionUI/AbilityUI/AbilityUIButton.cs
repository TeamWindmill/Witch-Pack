using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityUIButton : ClickableUIElement
{
    public event Action<AbilityUIButton> OnAbilityClick;
    public BaseAbilitySO RootAbilitySo => rootAbilitySo;
    public BaseAbilitySO ActiveAbilitySo => activeAbilitySo;
    public AbilityCaster Caster => _caster;
    
    [SerializeField] private Image _abilitySpriteRenderer;
    [SerializeField] private Image _cooldownSpriteRenderer;
    [SerializeField] private Image _frameSpriteRenderer;
    [Space] 
    [SerializeField] private Sprite upgradeReadyFrameSprite;
    [SerializeField] private Sprite defaultFrameSprite;
    
    private AbilityCaster _caster;
    private BaseAbilitySO rootAbilitySo;
    private BaseAbilitySO activeAbilitySo;
    private float _abilityCd;
    private float _abilityLastCast;



    private bool _activeCd;
    
    public void Init(BaseAbilitySO rootAbilitySo,BaseAbilitySO activeAbilitySo = null, AbilityCaster caster = null, bool hasSkillPoints = false)
    {
        
        this.rootAbilitySo = rootAbilitySo;
        if (ReferenceEquals(activeAbilitySo,null))
        {
            _windowInfo.Name = rootAbilitySo.Name;
            _windowInfo.Discription = rootAbilitySo.Discription;
            _frameSpriteRenderer.sprite = hasSkillPoints ? upgradeReadyFrameSprite : defaultFrameSprite;
            _abilitySpriteRenderer.sprite = hasSkillPoints ? rootAbilitySo.UpgradeIcon : rootAbilitySo.DisabledIcon;
            this.activeAbilitySo = null;
            SetCooldownData(0);
        }
        else
        {
            this.activeAbilitySo = activeAbilitySo;
            _windowInfo.Name = activeAbilitySo.Name;
            _windowInfo.Discription = activeAbilitySo.Discription;
            if (this.activeAbilitySo.Upgrades.Length > 0)
            {
                _frameSpriteRenderer.sprite = hasSkillPoints ? upgradeReadyFrameSprite : defaultFrameSprite;
                _abilitySpriteRenderer.sprite = hasSkillPoints ? activeAbilitySo.UpgradeIcon : activeAbilitySo.DefaultIcon;
            }
            else
            {
                _frameSpriteRenderer.sprite = defaultFrameSprite;
                _abilitySpriteRenderer.sprite = activeAbilitySo.DefaultIcon;
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

    private void Update()
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
            _abilityCd = castHandler.AbilitySo.Cd;
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