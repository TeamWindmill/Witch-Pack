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
    
    public void Init(BaseAbility rootAbility,BaseAbility activeAbility = null, UnitCastingHandler castingHandler = null)
    {
        _rootAbility = rootAbility;
        if (ReferenceEquals(activeAbility,null))
        {
            _abilitySpriteRenderer.sprite = rootAbility.Icon;
            _frameSpriteRenderer.sprite = upgradeReadyFrameSprite;
        }
        else
        {
            _activeAbility = activeAbility;
            _abilitySpriteRenderer.sprite = activeAbility.Icon;
            _frameSpriteRenderer.sprite = defaultFrameSprite;
            if (castingHandler is not null)
            {
                _castingHandler = castingHandler;
                SetCooldownData(castingHandler);
            }
            else SetCooldownData();
        }
        Show();
    }

    public override void Hide()
    {
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
    private void SetCooldownData(UnitCastingHandler castingHandler = null)
    {
        if (ReferenceEquals(castingHandler, null))
        {
            _cooldownSpriteRenderer.fillAmount = 1;
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