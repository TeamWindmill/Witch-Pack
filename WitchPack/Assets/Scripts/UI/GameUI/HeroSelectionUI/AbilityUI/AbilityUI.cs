using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityUI : ClickableUIElement , IInit<UnitCastingHandler>
{
    public event Action<AbilityUI> OnAbilityClick;
    [SerializeField] private Image _abilitySpriteRenderer;
    [SerializeField] private Image _cooldownSpriteRenderer;

    private UnitCastingHandler _abilityCaster;
    private BaseAbility _baseAbility;
    //public bool IsActive => _isActive;
   // private bool _isActive = false;

    // protected override void Start()
    // {
    //     UIManager.Instance.AddUIElement(this,uiGroup);
    //     
    //     _abilitySpriteRenderer.enabled = false;
    //     _cooldownSpriteRenderer.enabled = false;
    // }
    public void Init(UnitCastingHandler abilityCaster)
    {
        _abilityCaster = abilityCaster;
        _baseAbility = abilityCaster.Ability;
        _abilitySpriteRenderer.sprite = abilityCaster.Ability.Icon;
        Show();
    }
    public override void Show()
    {
        gameObject.SetActive(true);
     //   _isActive = true;
    }
   
    public override void Hide()
    {
        base.Hide();
//_isActive = false;
    }

    private void Update()
    {
      //  if (!_isActive) return;
       // UpdateCooldownFillAmount(_abilityCaster);
    }

    protected override void OnClick(PointerEventData eventData)
    {
        OnAbilityClick?.Invoke(this);
    }

    private void UpdateCooldownFillAmount(UnitCastingHandler abilityCaster)
    {
        float ratio = 0;
        if (abilityCaster.LastCast > 0)
        {
            ratio = (GAME_TIME.GameTime - abilityCaster.LastCast) / abilityCaster.Ability.Cd;
            if (ratio < 0) ratio = 0;
        }
        _cooldownSpriteRenderer.fillAmount = ratio;
    }
}