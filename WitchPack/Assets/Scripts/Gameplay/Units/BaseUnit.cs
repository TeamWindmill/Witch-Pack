using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class BaseUnit : BaseEntity , IDamagable
{
    #region Public

    public bool Initialized { get; protected set; }
    public bool IsDead => damageable.CurrentHp <= 0;
    public BaseUnitConfig UnitConfig => _unitConfig;
    public HP_Bar HpBar => hpBar;
    public Damageable Damageable => damageable;
    public DamageDealer DamageDealer => damageDealer;
    public Affector Affector => affector;
    public Effectable Effectable => effectable;
    public virtual Stats BaseStats => null;
    public UnitStats Stats => stats;
    public AutoAttackCaster AutoAttackCaster => autoAttackCaster;
    public UnitAutoCaster AutoCaster => _autoCaster;
    public UnitMovement Movement => movement;
    public List<AbilityCaster> CastingHandlers => castingHandlers;
    public Transform CastPos => _castPos;
    public EnemyTargeter EnemyTargeter => enemyTargeter;
    public ShamanTargeter ShamanTargeter => shamanTargeter;
    public UnitTargetHelper<Shaman> ShamanTargetHelper => shamanTargetHelper;
    public UnitTargetHelper<Enemy> EnemyTargetHelper => enemyTargetHelper;
    public List<ITimer> UnitTimers => unitTimers;

    #endregion

    #region Serialized
    
    [SerializeField] private UnitType unitType;
    [TabGroup("Combat")] private Damageable damageable;
    [TabGroup("Combat")] private DamageDealer damageDealer;
    [TabGroup("Combat")] private Affector affector;
    [TabGroup("Combat")] private Effectable effectable;
    [SerializeField, TabGroup("Combat")] private UnitAutoCaster _autoCaster;
    [SerializeField, TabGroup("Combat")] private BoxCollider2D boxCollider;
    [SerializeField, TabGroup("Combat")] private GroundCollider groundCollider;
    [SerializeField, TabGroup("Stats")] private UnitStats stats;
    [SerializeField, TabGroup("Movement")] private UnitMovement movement;
    [SerializeField, TabGroup("Visual")] private bool hasHPBar;
    [SerializeField, ShowIf(nameof(hasHPBar)), TabGroup("Visual")] private HP_Bar hpBar;
    [SerializeField, TabGroup("Combat")] private Transform _castPos;
    [SerializeField, TabGroup("Targeter")] private EnemyTargeter enemyTargeter;
    [SerializeField, TabGroup("Targeter")] private ShamanTargeter shamanTargeter;
    
    #endregion

    #region Private

    protected List<AbilityCaster> castingHandlers = new();
    private UnitTargetHelper<Shaman> shamanTargetHelper;
    private UnitTargetHelper<Enemy> enemyTargetHelper;
    private AutoAttackCaster autoAttackCaster;
    private OffensiveAbilitySO _autoAttack;
    private List<ITimer> unitTimers;
    private BaseUnitConfig _unitConfig;

    #endregion
    
    
    public virtual void Init(BaseUnitConfig givenConfig)
    {
        _unitConfig = givenConfig;
        _autoAttack = givenConfig.AutoAttack;
        stats = new UnitStats(BaseStats);
        damageDealer = new DamageDealer(this, givenConfig.AutoAttack);
        affector = new Affector(this);
        effectable = new Effectable(this);
        autoAttackCaster = new AutoAttackCaster(this, AbilityFactory.CreateAbility(givenConfig.AutoAttack,this) as OffensiveAbility);
        damageable = new Damageable(this);
        shamanTargetHelper = new UnitTargetHelper<Shaman>(ShamanTargeter, this);
        enemyTargetHelper = new UnitTargetHelper<Enemy>(EnemyTargeter, this);
        unitTimers = new List<ITimer>();
        Movement.SetUp(this);
        groundCollider.Init(this);
        
        ToggleCollider(true);
        damageable.SetRegenerationTimer();
        if (hasHPBar)
        {
            hpBar.gameObject.SetActive(true);
            hpBar.Init(damageable.MaxHp, unitType);
            damageable.OnHealthChange += hpBar.SetBarValue;
        }

        damageable.OnTakeDamage += LevelManager.Instance.PopupsManager.SpawnDamagePopup;
        damageable.OnHeal += LevelManager.Instance.PopupsManager.SpawnHealPopup;
        effectable.OnAffected += LevelManager.Instance.PopupsManager.SpawnStatusEffectPopup;
        Stats.OnStatChanged += EnemyTargeter.AddRadius;
        Stats.OnStatChanged += movement.OnSpeedChange;

    }
    protected virtual void OnDisable() //unsubscribe to events
    {
        if (ReferenceEquals(LevelManager.Instance, null)) return;
        if (ReferenceEquals(damageable, null)) return;
        if (ReferenceEquals(effectable, null)) return;
        damageable.OnTakeDamage -= LevelManager.Instance.PopupsManager.SpawnDamagePopup;
        damageable.OnHeal -= LevelManager.Instance.PopupsManager.SpawnHealPopup;
        effectable.OnAffected -= LevelManager.Instance.PopupsManager.SpawnStatusEffectPopup;
        if (hasHPBar) damageable.OnHealthChange -= hpBar.SetBarValue;
        Stats.OnStatChanged -= EnemyTargeter.AddRadius;
        Stats.OnStatChanged -= movement.OnSpeedChange;
    }
    public void AddStatUpgrades(StatValueUpgradeConfig[] statUpgrades)
    {
        foreach (var statUpgrade in statUpgrades)
        {
            Stats[statUpgrade.StatType].AddStatValue(statUpgrade.Factor,statUpgrade.StatValue);
        }
    }

    public void ToggleCollider(bool state)
    {
        boxCollider.enabled = state;
    }

    public void OnDeathAnimation()
    {
        Movement.ToggleMovement(false);
        ToggleCollider(false);
        damageable.ToggleHitable(false);
        _autoCaster.DisableCaster();
    }

    public void ClearUnitTimers()
    {
        foreach (ITimer iTimer in UnitTimers)
        {
            iTimer.RemoveThisTimer();
        }

        UnitTimers.Clear();
    }

    public BaseEntity GameObject => this;

    private void OnValidate()
    {
        boxCollider ??= GetComponent<BoxCollider2D>();
    }
    
}