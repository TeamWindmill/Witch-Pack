using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityUIButton : ClickableUIElement , IInit<UnitCastingHandler>
{
    public event Action<AbilityUIButton> OnAbilityClick;
    public BaseAbility BaseAbility => _baseAbility;
    
    [SerializeField] private Image _abilitySpriteRenderer;
    [SerializeField] private Image _cooldownSpriteRenderer;

    private UnitCastingHandler _abilityCaster;
    private BaseAbility _baseAbility;

    private float _abilityCd;
    private float _abilityLastCast;
    private bool _activeCd;
    
    public void Init(UnitCastingHandler abilityCaster)
    {
        _abilityCaster = abilityCaster;
        _baseAbility = abilityCaster.Ability;
        _abilitySpriteRenderer.sprite = abilityCaster.Ability.Icon;
        SetCooldownData(abilityCaster);
        Show();
    }

    public override void Hide()
    {
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
    private void SetCooldownData(UnitCastingHandler castingHandler = null)
    {
        if (ReferenceEquals(castingHandler, null))
        {
            _activeCd = false;
        }
        else
        {
            _abilityCd = castingHandler.Ability.Cd;
            _abilityLastCast = castingHandler.LastCast;
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
            if (ratio < 0) ratio = 0;
        }
        _cooldownSpriteRenderer.fillAmount = ratio;
    }
}