using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityUpgradeUIButton : ClickableUIElement
{
    public event Action<AbilityUpgradeUIButton> OnAbilityClick;
    public BaseAbility Ability => _ability;

    [SerializeField] private Image bg;
    [SerializeField] private Image lockedBg;
    [SerializeField] private Image frame;
    [SerializeField] private Image abilitySprite;
    [Space] 
    [SerializeField] private Sprite upgradeReadyFrameSprite;
    [SerializeField] private Sprite defaultFrameSprite;

    private BaseAbility _ability;
    private bool _hasSkillPoints;


    public void Init(BaseAbility ability, bool hasSkillPoints)
    {
        abilitySprite.sprite = ability.DefaultIcon;
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
                lockedBg.gameObject.SetActive(true);
                frame.sprite = defaultFrameSprite;
                break;
            case AbilityUpgradeState.Open:
                if (!_hasSkillPoints)
                {
                    lockedBg.gameObject.SetActive(true);
                    frame.sprite = defaultFrameSprite;
                }
                else
                {
                    lockedBg.gameObject.SetActive(false);
                    frame.sprite = upgradeReadyFrameSprite;
                }
                break;
            case AbilityUpgradeState.Upgraded:
                lockedBg.gameObject.SetActive(false);
                frame.sprite = defaultFrameSprite;
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