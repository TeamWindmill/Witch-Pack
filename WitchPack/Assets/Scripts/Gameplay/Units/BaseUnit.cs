using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    [SerializeField] private UnitType unitType;
    [SerializeField, TabGroup("Combat")] private Damageable damageable;
    [SerializeField, TabGroup("Combat")] private DamageDealer damageDealer;
    [SerializeField, TabGroup("Combat")] private Affector affector;
    [SerializeField, TabGroup("Combat")] private Effectable effectable;
    [SerializeField, TabGroup("Combat")] private OffensiveAbility autoAttack;
    [SerializeField, TabGroup("Combat")] private UnitAutoAttacker autoAttacker;
    [SerializeField, TabGroup("Stats")] private UnitStats stats;
    [SerializeField, TabGroup("Movement")] private UnitMovement movement;
    [SerializeField, TabGroup("Visual")] private UnitVisualHandler unitVisual;


    [SerializeField, TabGroup("Visual")] private bool hasHPBar;
    [SerializeField,ShowIf(nameof(hasHPBar)), TabGroup("Visual")] private HP_Bar hpBar;

    private AutoAttackHandler autoAttackHandler;


    public UnitVisualHandler UnitVisual => unitVisual;
    public Damageable Damageable { get => damageable; }
    public DamageDealer DamageDealer { get => damageDealer; }
    public Affector Affector { get => affector; }
    public Effectable Effectable { get => effectable; }
    public virtual StatSheet BaseStats { get { return null; } }
    public UnitStats Stats { get => stats; }
    public OffensiveAbility AutoAttack { get => autoAttack; }
    public AutoAttackHandler AutoAttackHandler { get => autoAttackHandler; }
    public UnitAutoAttacker AutoAttacker { get => autoAttacker; }
    public UnitMovement Movement { get => movement; }

    //movement comp
    //state machine -> heros and enemies essentially work the same only heroes can be told where to go, everything else is automatic 

    public virtual void Init(BaseUnitConfig givenConfig)
    {
        stats = new UnitStats(this);
        damageable = new Damageable(this);
        damageDealer = new DamageDealer(this, autoAttack);
        affector = new Affector(this);
        effectable = new Effectable(this);
        autoAttackHandler = new AutoAttackHandler(this, autoAttack);
        Movement.SetSpeed(Stats.MovementSpeed);
        Stats.OnStatChanged += Movement.AddSpeed;
        AutoAttacker.SetUp(this);
        Movement.SetUp(this);
        unitVisual.Init(this, givenConfig);
        if (hasHPBar)
        {
            hpBar.Init(damageable.MaxHp,unitType);
            damageable.OnDamageCalc += hpBar.SetBarValue;
        }
        
    }


    protected void DisableAttacker(Vector3 pos)
    {
        autoAttacker.CanAttack = false;

    }
    protected void EnableAttacker(Vector3 pos)
    {
        autoAttacker.CanAttack = true;
    }

    private void OnDestroy()
    {
        if (hasHPBar) damageable.OnDamageCalc -= hpBar.SetBarValue;
        Stats.OnStatChanged -= Movement.AddSpeed;
    }
}
