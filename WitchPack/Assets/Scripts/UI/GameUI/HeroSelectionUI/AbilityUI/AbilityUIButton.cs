using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityUIButton : ClickableUIElement
{
    public event Action<AbilityUIButton> OnAbilityClick;
    public BaseAbility RootAbility => _rootAbility;
    public BaseAbility ActiveAbility => _activeAbility;
    
    [SerializeField] private Image _abilitySpriteRenderer;
    [SerializeField] private Image _cooldownSpriteRenderer;
    [SerializeField] private Image _frameSpriteRenderer;
    [Space] 
    [SerializeField] private Sprite upgradeReadyFrameSprite;
    [SerializeField] private Sprite defaultFrameSprite;
    
    private UnitCastingHandler _castingHandler;
    private BaseAbility _rootAbility;
    private BaseAbility _activeAbility;
    private float _abilityCd;
    private float _abilityLastCast;



    private bool _activeCd;
    
    public void Init(BaseAbility rootAbility,BaseAbility activeAbility = null, UnitCastingHandler castingHandler = null, bool hasSkillPoints = false)
    {
        _rootAbility = rootAbility;
        _frameSpriteRenderer.sprite = hasSkillPoints ? upgradeReadyFrameSprite : defaultFrameSprite;
        if (ReferenceEquals(activeAbility,null))
        {
            _abilitySpriteRenderer.sprite = rootAbility.Icon;
            SetCooldownData(1);
        }
        else
        {
            _activeAbility = activeAbility;
            _abilitySpriteRenderer.sprite = activeAbility.Icon;
            if (castingHandler is not null)
            {
                _castingHandler = castingHandler;
                SetCooldownData(castHandler: castingHandler);
                _castingHandler.OnCast += UpdateOnCast;
            }
            else SetCooldownData(0);
        }
        Show();
    }

    public override void Hide()
    {
        if (_castingHandler is not null) _castingHandler.OnCast -= UpdateOnCast;
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
    private void SetCooldownData(float fillAmount = 0, UnitCastingHandler castHandler = null)
    {
        if (ReferenceEquals(castHandler, null))
        {
            _cooldownSpriteRenderer.fillAmount = fillAmount;
            _activeCd = false;
        }
        else
        {
            _abilityCd = castHandler.Ability.Cd;
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
    private void UpdateOnCast(UnitCastingHandler castingHandler)
    {
        _abilityLastCast = castingHandler.LastCast;
    }
}