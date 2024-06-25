using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Enumerable = System.Linq.Enumerable;

public class Shaman : BaseUnit
{
    #region public

    public override Stats BaseStats => ShamanConfig.BaseStats;
    public ShamanConfig ShamanConfig { get; private set; }

    public List<Ability> KnownAbilities { get; } = new();
    public List<Ability> RootAbilities { get; } = new();
    public bool MouseOverShaman => clicker.IsHover;
    public EnergyHandler EnergyHandler => energyHandler;
    public ShamanVisualHandler ShamanVisualHandler => shamanVisualHandler;
    public Dictionary<PowerStructure,int> ActivePowerStructures { get; } = new();

    public ClickHelper Clicker => clicker;

    #endregion

    #region serialized

    [SerializeField, TabGroup("Visual")] private ShamanAnimator shamanAnimator;
    [SerializeField, TabGroup("Visual")] private ShamanVisualHandler shamanVisualHandler;
    [SerializeField] private ClickHelper clicker;
    [SerializeField] private Indicatable indicatable;
    [SerializeField] private ParticleSystem levelUpEffect;
    [SerializeField] private EnergyHandler energyHandler;

    #endregion

    #region private


    #endregion

    private void OnValidate()
    {
        shamanAnimator ??= GetComponentInChildren<ShamanAnimator>();
    }

    public void Init(ShamanSaveData saveData)
    {
        ShamanConfig = saveData.Config;
        base.Init(ShamanConfig);
        energyHandler = new EnergyHandler(this);
        EnemyTargeter.SetRadius(Stats[StatType.BaseRange].Value);
        IntializeAbilities();
        AddMetaUpgrades(saveData);
        shamanAnimator.Init(this);
        indicatable.Init(ShamanConfig.UnitIndicatorIcon, action: FocusCameraOnShaman, clickable: true,
            indicatorPointerSprite: IndicatorPointerSpriteType.Cyan);
        Indicator newIndicator = LevelManager.Instance.IndicatorManager.CreateIndicator(indicatable);
        newIndicator.gameObject.SetActive(false);
        shamanVisualHandler.Init(this, saveData.Config);
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

    #region MetaUpgrades

    private void AddMetaUpgrades(ShamanSaveData saveData)
    {
        AddAbilityMetaUpgrades(saveData);
        AddStatMetaUpgrades(saveData);
    }
    private void AddAbilityMetaUpgrades(ShamanSaveData saveData)
    {
        foreach (var abilityUpgrade in saveData.AbilityUpgrades)
        {
            foreach (var abilitySO in abilityUpgrade.AbilitiesToUpgrade)
            {
                var ability = GetAbilityFromConfig(abilitySO);
                ability.AddStatUpgrade(abilityUpgrade);
                if(abilityUpgrade.AbilitiesBehaviors.Length > 0) ability.AddAbilityBehavior(abilityUpgrade);
            }
        }
    }

    private void AddStatMetaUpgrades(ShamanSaveData saveData)
    {
        foreach (var statUpgrade in saveData.StatUpgrades)
        {
            if (statUpgrade.AbilitiesBehaviors.Length > 0)
            {
                foreach (var abilitySO in statUpgrade.AbilitiesToUpgrade)
                {
                    var ability = GetAbilityFromConfig(abilitySO);
                    ability.AddAbilityBehavior(statUpgrade);
                }
            }
            if (statUpgrade.UpgradeAbility)
            {
                foreach (var abilitySO in statUpgrade.AbilitiesToUpgrade)
                {
                    var ability = GetAbilityFromConfig(abilitySO);
                    ability.AddStatUpgrade(statUpgrade);
                }
            }
            else if (statUpgrade.UpgradePassiveAbility)
            {
                foreach (var abilitySO in statUpgrade.AbilitiesToUpgrade)
                {
                    var ability = GetAbilityFromConfig(abilitySO);
                    if(ability is not StatPassive statPassive) return;
                    statPassive.AddPassiveStatUpgrade(statUpgrade);
                }
            }
            else
            {
                foreach (var statConfig in statUpgrade.Stats)
                {
                    Stats.AddValueToStat(statConfig.StatType,statConfig.Factor,statConfig.StatValue);
                }
            }
        }
    }

    #endregion

    #region Abilities

    private void IntializeAbilities()
    {
        foreach (var abilitySo in ShamanConfig.RootAbilities)
        {
            var ability = AbilityFactory.CreateAbility(abilitySo, this);
            RootAbilities.Add(ability);
            foreach (var upgrade in ability.GetUpgrades())
            {
                upgrade.ChangeUpgradeState(UpgradeState.Locked);
            }

            ability.ChangeUpgradeState(UpgradeState.Open);
        }

        foreach (var abilitySo in ShamanConfig.KnownAbilities)
        {
            foreach (var ability in Enumerable.Where(RootAbilities, rootAbility => rootAbility.BaseConfig == abilitySo))
            {
                ability.UpgradeAbility();
                KnownAbilities.Add(ability);
                if (ability is PassiveAbility passive)
                {
                    passive.SubscribePassive();
                }
                else if (ability is CastingAbility castingAbility)
                {
                    //abilitySo.OnSetCaster(this);
                    castingHandlers.Add(new AbilityCaster(this, castingAbility));
                }
            }
        }

        AutoCaster.Init(this, true);
    }

    public void LearnAbility(Ability ability)
    {
        KnownAbilities.Add(ability);
        if (ability is PassiveAbility passive)
        {
            passive.SubscribePassive();
        }
        else if (ability is CastingAbility castingAbility)
        {
            //ability.BaseConfig.OnSetCaster(this);
            var caster = new AbilityCaster(this, castingAbility);
            castingHandlers.Add(caster);
            AutoCaster.ReplaceAbility(caster);
        }
    }

    public void RemoveAbility(Ability ability)
    {
        if (ability is not PassiveAbility) //unsubscribe passives currently doesnt work
        {
            KnownAbilities.Remove(ability);
            castingHandlers.Remove(GetCasterFromAbility(ability));
        }
    }

    public void UpgradeAbility(Ability ability, Ability upgrade)
    {
        RemoveAbility(ability);
        LearnAbility(upgrade);
    }


    public AbilityCaster GetCasterFromAbility(Ability givenAbility)
    {
        for (int i = 0; i < castingHandlers.Count; i++)
        {
            if (ReferenceEquals(castingHandlers[i].Ability, givenAbility))
            {
                return castingHandlers[i];
            }
        }

        return null;
    }

    public Ability GetActiveAbilityFromRoot(Ability rootAbility)
    {
        if (KnownAbilities.Contains(rootAbility)) return rootAbility;

        var upgrades = rootAbility.GetUpgrades();
        foreach (var upgrade in upgrades)
        {
            if (KnownAbilities.Contains(upgrade)) return upgrade;
        }

        return null;
    }

    public Ability GetAbilityFromConfig(AbilitySO config)
    {
        foreach (var ability in RootAbilities)
        {
            if (ability.BaseConfig == config) return ability;
            
            foreach (var upgrade in ability.GetUpgrades())
            {
                if (upgrade.BaseConfig == config) return upgrade;
            }
        }

        return null;
    }
    
    #endregion

    #region Selection

    public void SetSelectedShaman(PointerEventData.InputButton button)
    {
        LevelManager.Instance.SelectionHandler.OnShamanClick(button, this);
    }

    private void ShamanHoveredEntered()
    {
        shamanVisualHandler.ShowShamanRange();
        foreach (var powerStructure in ActivePowerStructures)
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

    #endregion

    #region SFX

    private void OnLevelUpGFX(int obj)
    {
        levelUpEffect.Play();
        SoundManager.Instance.PlayAudioClip(SoundEffectType.ShamanLevelUp);
    }

    private void OnHitSFX(bool isCrit)
    {
        switch (ShamanConfig.Sex)
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
        switch (ShamanConfig.Sex)
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

    public void ShamanCastSFX(CastingAbility ability) =>
        SoundManager.Instance.PlayAudioClip(SoundEffectType.ShamanCast);

    #endregion

    #region Indicator

    private void SetOffIndicator()
    {
        indicatable.ToggleIndicatableRendering(false);
    }

    private void FocusCameraOnShaman()
    {
        GameManager.Instance.CameraHandler.SetCameraPosition(transform.position, false);
    }

    #endregion

    
}