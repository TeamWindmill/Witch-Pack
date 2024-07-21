using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityUpgradeUIButton : ClickableUIElement
{
    public event Action<AbilityUpgradeUIButton> OnAbilityClick;
    public BaseAbility Ability => _ability;

    [SerializeField] private Image bg;
    //[SerializeField] private Image lockedBg;
    [SerializeField] private Image frame;
    [SerializeField] private Image abilitySprite;
    [Space] 
    [SerializeField] private Sprite upgradeReadyFrameSprite;
    [SerializeField] private Sprite defaultFrameSprite;

    private BaseAbility _ability;
    private bool _hasSkillPoints;


    public void Init(BaseAbility ability, bool hasSkillPoints)
    {
        _ability = ability;
        _hasSkillPoints = hasSkillPoints;
        _windowInfo.Name = ability.Name;
        _windowInfo.Discription = ability.Discription;
        Show();
    }

    public override void Show()
    {
        switch (_ability.AbilityUpgradeState)
        {
            case AbilityUpgradeState.Locked:
                frame.sprite = defaultFrameSprite;
                abilitySprite.sprite = _ability.DisabledIcon;
                break;
            case AbilityUpgradeState.Open:
                if (!_hasSkillPoints)
                {
                    frame.sprite = defaultFrameSprite;
                    abilitySprite.sprite = _ability.DisabledIcon;
                }
                else
                {
                    frame.sprite = upgradeReadyFrameSprite;
                    abilitySprite.sprite = _ability.UpgradeIcon;
                }
                break;
            case AbilityUpgradeState.Upgraded:
                frame.sprite = defaultFrameSprite;
                abilitySprite.sprite = _ability.DefaultIcon;
                break;
        }

        base.Show();
    }

    public override void Hide()
    {
        if (OnAbilityClick is not null)
        {
            OnAbilityClick = null;
            //Debug.Log($"{_ability.name} Hidden");
        }
        base.Hide();
    }

    protected override void OnClick(PointerEventData eventData)
    {
        base.OnClick(eventData);
        switch (_ability.AbilityUpgradeState)
        {
            case AbilityUpgradeState.Locked:
                SoundManager.Instance.PlayAudioClip(SoundEffectType.MenuClick);
                return;
            case AbilityUpgradeState.Open:
                if(!_hasSkillPoints) return;
                _ability.UpgradeAbility();
                OnAbilityClick?.Invoke(this);
                SoundManager.Instance.PlayAudioClip(SoundEffectType.UpgradeAbility);
                return;
            case AbilityUpgradeState.Upgraded:
                SoundManager.Instance.PlayAudioClip(SoundEffectType.MenuClick);
                return;
        }
    }
}

public enum AbilityUpgradeState
{
    Locked,
    Open,
    Upgraded
}