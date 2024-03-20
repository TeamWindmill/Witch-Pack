using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shaman : BaseUnit
{
    #region public

    public override StatSheet BaseStats => shamanConfig.BaseStats;
    public ShamanConfig ShamanConfig => shamanConfig;
    public List<BaseAbility> KnownAbilities => knownAbilities;
    public bool MouseOverShaman => clicker.IsHover;
    public List<BaseAbility> RootAbilities => rootAbilities;
    public EnergyHandler EnergyHandler => energyHandler;
    public ShamanVisualHandler ShamanVisualHandler => shamanVisualHandler;
    public bool IsSelected {get => _isSelected; set => _isSelected = value; }
    #endregion

    #region serialized

    [SerializeField, TabGroup("Visual")] private ShamanAnimator shamanAnimator;
    [SerializeField, TabGroup("Visual")] private ShamanVisualHandler shamanVisualHandler;
    [SerializeField] private ClickHelper clicker;
    [SerializeField] private Indicatable indicatable;
    [SerializeField] private ParticleSystem levelUpEffect;

    #endregion

    #region private

    private ShamanConfig shamanConfig;
    private List<BaseAbility> rootAbilities = new List<BaseAbility>();
    private List<BaseAbility> knownAbilities = new List<BaseAbility>();
    [SerializeField] private EnergyHandler energyHandler;
    private bool _isSelected;
    #endregion

    private void OnValidate()
    {
        shamanAnimator ??= GetComponentInChildren<ShamanAnimator>();
    }

    public override void Init(BaseUnitConfig baseUnitConfig)
    {
        shamanConfig = baseUnitConfig as ShamanConfig;
        base.Init(shamanConfig);
        energyHandler = new EnergyHandler(this);
        EnemyTargeter.SetRadius(Stats.BonusRange);
        IntializeAbilities();
        shamanAnimator.Init(this);
        indicatable.Init(shamanConfig.UnitIndicatorIcon, action: FocusCameraOnShaman, clickable: true, indicatorPointerSprite: IndicatorPointerSpriteType.Cyan);
        Indicator newIndicator = LevelManager.Instance.IndicatorManager.CreateIndicator(indicatable);
        newIndicator.gameObject.SetActive(false);
        shamanVisualHandler.Init(this, baseUnitConfig);
        AutoCaster.Init(this, true);

        #region Events

        // no need to unsubscribe because shaman gets destroyed between levels
        shamanVisualHandler.OnSpriteFlip += shamanAnimator.FlipAnimations;
        Movement.OnDestinationSet += AutoCaster.DisableCaster;
        Movement.OnDestinationReached += AutoCaster.EnableCaster;
        if (LevelManager.Instance.SelectionHandler.GetOnMouseDownShaman())
        {
            clicker.OnMouseDown += SetSelectedShaman;
        }
        else
        { 
            clicker.OnClick += SetSelectedShaman;
        }
        clicker.OnEnterHover += ShamanHoveredEntered;
        clicker.OnExitHover += ShamanHoveredExit;
        DamageDealer.OnKill += energyHandler.OnEnemyKill;
        DamageDealer.OnAssist += energyHandler.OnEnemyAssist;
        energyHandler.OnShamanLevelUp += OnLevelUpGFX;
        Damageable.OnHitGFX += OnHitSFX;
        Damageable.OnDeathGFX += DeathSFX;
        Damageable.OnDeathGFX += SetOffIndicator;
        AutoAttackHandler.OnAttack += AttackSFX;
        Effectable.OnAffectedVFX += ShamanVisualHandler.EffectHandler.PlayEffect;
        Effectable.OnEffectRemovedVFX += ShamanVisualHandler.EffectHandler.DisableEffect;
        AutoCaster.CastTimeStartVFX += ShamanVisualHandler.EffectHandler.PlayEffect;
        AutoCaster.CastTimeEndVFX += ShamanVisualHandler.EffectHandler.DisableEffect;
        AutoCaster.CastTimeStart += ShamanCastSFX;

        #endregion

        BaseInit(baseUnitConfig);
    }

    private void IntializeAbilities()
    {
        foreach (var rootAbility in shamanConfig.RootAbilities)
        {
            rootAbilities.Add(rootAbility);
            foreach (var upgrade in rootAbility.GetUpgrades())
            {
                upgrade.ChangeUpgradeState(AbilityUpgradeState.Locked);
            }

            rootAbility.ChangeUpgradeState(AbilityUpgradeState.Open);
        }

        foreach (var ability in ShamanConfig.KnownAbilities)
        {
            ability.UpgradeAbility();
            knownAbilities.Add(ability);
            if (ability is not Passive)
            {
                ability.OnSetCaster(this);
                castingHandlers.Add(new AbilityCaster(this, ability as CastingAbility));
            }
            else
            {
                (ability as Passive).SubscribePassive(this);
            }
        }

        AutoCaster.Init(this, true);
    }

    public void LearnAbility(BaseAbility ability)
    {
        knownAbilities.Add(ability);
        if (ability is not Passive passive)
        {
            ability.OnSetCaster(this);
            castingHandlers.Add(new AbilityCaster(this, ability as CastingAbility));
        }
        else
        {
            passive.SubscribePassive(this);
        }

        AutoCaster.Init(this, true);
    }

    public void RemoveAbility(BaseAbility ability)
    {
        //if (ability is not Passive) //might cause a problem with some passives
        {
            knownAbilities.Remove(ability);
            castingHandlers.Remove(GetCasterFromAbility(ability));
        }
    }

    public void UpgradeAbility(BaseAbility ability, BaseAbility upgrade)
    {
        RemoveAbility(ability);
        LearnAbility(upgrade);
    }


    public AbilityCaster GetCasterFromAbility(BaseAbility givenAbiltiy)
    {
        for (int i = 0; i < castingHandlers.Count; i++)
        {
            if (ReferenceEquals(castingHandlers[i].Ability, givenAbiltiy))
            {
                return castingHandlers[i];
            }
        }

        //Debug.LogError("Attempted to retrive a non existing caster");
        return null;
    }

    public BaseAbility GetActiveAbilityFromRoot(BaseAbility rootAbility)
    {
        if (KnownAbilities.Contains(rootAbility)) return rootAbility;

        var upgrades = rootAbility.GetUpgrades();
        foreach (var upgrade in upgrades)
        {
            if (KnownAbilities.Contains(upgrade)) return upgrade;
        }

        return null;
    }

    public void SetSelectedShaman(PointerEventData.InputButton button)
    {
        LevelManager.Instance.SelectionHandler.OnShamanClick(button,this);
    }
    public void ShamanHoveredEntered()
    {
        if (!_isSelected)
        {
            shamanVisualHandler.ShowShamanRange();
        }
    }
    public void ShamanHoveredExit()
    {
        if (!_isSelected)
        { 
            shamanVisualHandler.HideShamanRange();
        }
    }

    public void ToggleClicker(bool state)
    {
        clicker.enabled = state;
    }


    #region SFX

    private void OnLevelUpGFX(int obj)
    {
        levelUpEffect.Play();
        SoundManager.Instance.PlayAudioClip(SoundEffectType.ShamanLevelUp);
    }

    private void OnHitSFX(bool isCrit) => SoundManager.Instance.PlayAudioClip(shamanConfig.IsMale ? SoundEffectType.ShamanGetHitMale : SoundEffectType.ShamanGetHitFemale);
    private void DeathSFX() => SoundManager.Instance.PlayAudioClip(shamanConfig.IsMale ? SoundEffectType.ShamanDeathMale : SoundEffectType.ShamanDeathFemale);
    private void AttackSFX() => SoundManager.Instance.PlayAudioClip(SoundEffectType.BasicAttack);
    public void ShamanAbilityCastSFX(CastingAbility ability) => SoundManager.Instance.PlayAudioClip(ability.SoundEffectType);
    public void ShamanCastSFX(CastingAbility ability) => SoundManager.Instance.PlayAudioClip(SoundEffectType.ShamanCast);

    #endregion

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    private void SetOffIndicator()
    {
        indicatable.ToggleIndicatableRendering(false);
    }

    private void FocusCameraOnShaman()
    {
        GameManager.Instance.CameraHandler.SetCameraPosition(transform.position, false);
    }
}