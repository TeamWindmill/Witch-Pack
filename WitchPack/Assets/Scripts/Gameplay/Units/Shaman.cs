using System;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class Shaman : BaseUnit
{
    [SerializeField, TabGroup("Combat")] private EnemyTargeter enemyTargeter;
    [SerializeField, TabGroup("Visual")] private ShamanAnimator shamanAnimator;
    [SerializeField] private ClickHelper clicker;
    private ShamanConfig shamanConfig;
    private List<BaseAbility> knownAbilities = new List<BaseAbility>();
    private List<UnitCastingHandler> castingHandlers = new List<UnitCastingHandler>();


    public override StatSheet BaseStats => shamanConfig.BaseStats;

    private void OnValidate()
    {
        shamanAnimator ??= GetComponentInChildren<ShamanAnimator>();
    }

    public override void Init(BaseUnitConfig baseUnitConfig)
    {
        shamanConfig = baseUnitConfig as ShamanConfig;
        base.Init(shamanConfig);
        enemyTargeter.SetRadius(Stats.BonusRange);
        Stats.OnStatChanged += enemyTargeter.AddRadius;
        IntializeCastingHandlers();
        Movement.OnDestenationSet += DisableAttacker;
        Movement.OnDestenationReached += EnableAttacker;
        shamanAnimator.Init(this);
        clicker.OnClick += SetSelectedShaman;
    }


    private void IntializeCastingHandlers()
    {
        foreach (var item in ShamanConfig.KnownAbilities)
        {
            knownAbilities.Add(item);
            castingHandlers.Add(new UnitCastingHandler(this, item));
        }
    }

    public void LearnAbility(BaseAbility ability)
    {
        knownAbilities.Add(ability);
        castingHandlers.Add(new UnitCastingHandler(this, ability));
    }

    public void RemoveAbility(BaseAbility ability)
    {
        knownAbilities.Remove(ability);
        foreach (var item in castingHandlers)
        {
            if (ReferenceEquals(item.Ability, ability))
            {
                castingHandlers.Remove(item);
                break;
            }
        }
    }

    private void SetSelectedShaman()
    {
        LevelManager.Instance.SelectionManager.SetSelectedShaman(this);
    }

    public EnemyTargeter EnemyTargeter { get => enemyTargeter; }
    public ShamanConfig ShamanConfig { get => shamanConfig; }
    public List<BaseAbility> KnownAbilities { get => knownAbilities; }
    public List<UnitCastingHandler> CastingHandlers { get => castingHandlers; }
}
