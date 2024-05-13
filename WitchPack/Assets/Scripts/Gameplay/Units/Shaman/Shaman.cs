using System;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shaman : BaseUnit
{
    #region public

    public override Stats BaseStats => shamanConfig.BaseStats;
    public ShamanConfig ShamanConfig => shamanConfig;
    public List<BaseAbilitySO> KnownAbilities => knownAbilities;
    public bool MouseOverShaman => clicker.IsHover;
    public List<BaseAbilitySO> RootAbilities => rootAbilities;
    public EnergyHandler EnergyHandler => energyHandler;
    public ShamanVisualHandler ShamanVisualHandler => shamanVisualHandler;
    public Dictionary<PowerStructure,int> ActivePowerStructures => _activePowerStructures;
    public ClickHelper Clicker => clicker;

    #endregion

    #region serialized

    [SerializeField, TabGroup("Visual")] private ShamanAnimator shamanAnimator;
    [SerializeField, TabGroup("Visual")] private ShamanVisualHandler shamanVisualHandler;
    [SerializeField] private ClickHelper clicker;
    [SerializeField] private Indicatable indicatable;
    [SerializeField] private ParticleSystem levelUpEffect;

    #endregion

    #region private

    private Dictionary<PowerStructure,int> _activePowerStructures = new();
    private ShamanConfig shamanConfig;
    private List<BaseAbilitySO> rootAbilities = new List<BaseAbilitySO>();
    private List<BaseAbilitySO> knownAbilities = new List<BaseAbilitySO>();
    [SerializeField] private EnergyHandler energyHandler;

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
        indicatable.Init(shamanConfig.UnitIndicatorIcon, action: FocusCameraOnShaman, clickable: true,
            indicatorPointerSprite: IndicatorPointerSpriteType.Cyan);
        Indicator newIndicator = LevelManager.Instance.IndicatorManager.CreateIndicator(indicatable);
        newIndicator.gameObject.SetActive(false);
        shamanVisualHandler.Init(this, baseUnitConfig);
        AutoCaster.Init(this, true);

        #region Events

        // no need to unsubscribe because shaman gets destroyed between levels
        shamanVisualHandler.OnSpriteFlip += shamanAnimator.FlipAnimations;
        Movement.OnDestinationSet += AutoCaster.DisableCaster;
        Movement.OnDestinationReached += AutoCaster.EnableCaster;
        clicker.OnClick += SetSelectedShaman;
        clicker.OnEnterHover += ShamanHoveredEntered;
        clicker.OnExitHover += ShamanHoveredExit;
        DamageDealer.OnKill += energyHandler.OnEnemyKill;
        DamageDealer.OnAssist += energyHandler.OnEnemyAssist;
        energyHandler.OnShamanLevelUp += OnLevelUpGFX;
        Damageable.OnHitGFX += OnHitSFX;
        Damageable.OnDeathGFX += DeathSFX;
        Damageable.OnDeathGFX += SetOffIndicator;
        AutoAttackCaster.OnAttack += AttackSFX;
        Effectable.OnAffectedVFX += ShamanVisualHandler.EffectHandler.PlayEffect;
        Effectable.OnEffectRemovedVFX += ShamanVisualHandler.EffectHandler.DisableEffect;
        AutoCaster.CastTimeStartVFX += ShamanVisualHandler.EffectHandler.PlayEffect;
        AutoCaster.CastTimeEndVFX += ShamanVisualHandler.EffectHandler.DisableEffect;
        AutoCaster.CastTimeStart += ShamanCastSFX;

        #endregion

        Initialized = true;
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
                castingHandlers.Add(new AbilityCaster(this, ability as CastingAbilitySO));
            }
            else
            {
                (ability as Passive).SubscribePassive(this);
            }
        }

        AutoCaster.Init(this, true);
    }

    public void LearnAbility(BaseAbilitySO abilitySo)
    {
        knownAbilities.Add(abilitySo);
        if (abilitySo is not Passive passive)
        {
            abilitySo.OnSetCaster(this);
            var caster = new AbilityCaster(this, abilitySo as CastingAbilitySO);
            castingHandlers.Add(caster);
            AutoCaster.ReplaceAbility(caster);
        }
        else
        {
            passive.SubscribePassive(this);
        }
    }

    public void RemoveAbility(BaseAbilitySO abilitySo)
    {
        //if (ability is not Passive) //might cause a problem with some passives
        {
            knownAbilities.Remove(abilitySo);
            castingHandlers.Remove(GetCasterFromAbility(abilitySo));
        }
    }

    public void UpgradeAbility(BaseAbilitySO abilitySo, BaseAbilitySO upgrade)
    {
        RemoveAbility(abilitySo);
        LearnAbility(upgrade);
    }


    public AbilityCaster GetCasterFromAbility(BaseAbilitySO givenAbiltiy)
    {
        for (int i = 0; i < castingHandlers.Count; i++)
        {
            if (ReferenceEquals(castingHandlers[i].AbilitySo, givenAbiltiy))
            {
                return castingHandlers[i];
            }
        }

        //Debug.LogError("Attempted to retrive a non existing caster");
        return null;
    }

    public BaseAbilitySO GetActiveAbilityFromRoot(BaseAbilitySO rootAbilitySo)
    {
        if (KnownAbilities.Contains(rootAbilitySo)) return rootAbilitySo;

        var upgrades = rootAbilitySo.GetUpgrades();
        foreach (var upgrade in upgrades)
        {
            if (KnownAbilities.Contains(upgrade)) return upgrade;
        }

        return null;
    }

    public void SetSelectedShaman(PointerEventData.InputButton button)
    {
        LevelManager.Instance.SelectionHandler.OnShamanClick(button, this);
    }

    private void ShamanHoveredEntered()
    {
        shamanVisualHandler.ShowShamanRange();
        foreach (var powerStructure in _activePowerStructures)
        {
            powerStructure.Key.ProximityRingsManager.ToggleRingSprite(powerStructure.Value,true);
            powerStructure.Key.OnShamanHoverEnter(this,powerStructure.Value);
        }
    }

    private void ShamanHoveredExit()
    {
        shamanVisualHandler.HideShamanRange();
        foreach (var powerStructure in LevelManager.Instance.CurrentLevel.PowerStructures)
        {
            powerStructure.ProximityRingsManager.ToggleAllSprites(false);
            powerStructure.HideUI();
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

    private void OnHitSFX(bool isCrit)
    {
        switch (shamanConfig.Sex)
        {
            case Sex.Male:
                SoundManager.Instance.PlayAudioClip(SoundEffectType.ShamanGetHitMale);
                break;
            case Sex.Female:
                SoundManager.Instance.PlayAudioClip(SoundEffectType.ShamanGetHitFemale);
                break;
        }
        
        shamanVisualHandler.HitEffect.Play();
    }

    private void DeathSFX()
    {
        switch (shamanConfig.Sex)
        {
            case Sex.Male:
                SoundManager.Instance.PlayAudioClip(SoundEffectType.ShamanDeathMale);
                break;
            case Sex.Female:
                SoundManager.Instance.PlayAudioClip(SoundEffectType.ShamanDeathFemale);
                break;
        }
    }

    private void AttackSFX() => SoundManager.Instance.PlayAudioClip(SoundEffectType.BasicAttack);

    public void ShamanAbilityCastSFX(CastingAbilitySO abilitySo) =>
        SoundManager.Instance.PlayAudioClip(abilitySo.SoundEffectType);

    public void ShamanCastSFX(CastingAbilitySO abilitySo) =>
        SoundManager.Instance.PlayAudioClip(SoundEffectType.ShamanCast);

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