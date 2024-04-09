using System.Collections.Generic;
using Sirenix.OdinInspector;
using Tools.Helpers;
using UnityEngine;

public class BaseUnit : InitializedMono<BaseUnitConfig>
{
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

    protected List<AbilityCaster> castingHandlers = new List<AbilityCaster>();
    private UnitTargetHelper<Shaman> shamanTargetHelper;
    private UnitTargetHelper<Enemy> enemyTargetHelper;
    private AutoAttackHandler autoAttackHandler;
    private OffensiveAbility _autoAttack;
    private List<ITimer> unitTimers;

    #endregion
    
    #region Public
    public bool IsDead => damageable.CurrentHp <= 0;
    public HP_Bar HpBar => hpBar;
    public Damageable Damageable => damageable;
    public DamageDealer DamageDealer => damageDealer;
    public Affector Affector => affector;
    public Effectable Effectable => effectable;
    public virtual Stats BaseStats => null;
    public UnitStats Stats => stats;
    public AutoAttackHandler AutoAttackHandler => autoAttackHandler;
    public OffensiveAbility AutoAttack => _autoAttack;
    public UnitAutoCaster AutoCaster => _autoCaster;
    public UnitMovement Movement => movement;
    public List<AbilityCaster> CastingHandlers => castingHandlers;
    public Transform CastPos => _castPos;
    public EnemyTargeter EnemyTargeter => enemyTargeter;
    public ShamanTargeter ShamanTargeter => shamanTargeter;
    public UnitTargetHelper<Shaman> ShamanTargetHelper => shamanTargetHelper;
    public UnitTargetHelper<Enemy> EnemyTargetHelper => enemyTargetHelper;

    public List<ITimer> UnitTimers { get => unitTimers; }
    #endregion

    public virtual void Init(BaseUnitConfig givenConfig)
    {
        _autoAttack = givenConfig.AutoAttack;
        stats = new UnitStats(BaseStats);
        damageable = new Damageable(this);
        damageDealer = new DamageDealer(this, givenConfig.AutoAttack);
        affector = new Affector(this);
        effectable = new Effectable(this);
        autoAttackHandler = new AutoAttackHandler(this, givenConfig.AutoAttack);
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
            damageable.OnDamageCalc += hpBar.SetBarValue;
            damageable.OnHeal += hpBar.SetBarBasedOnOwner;
        }

        damageable.OnDamageCalc += LevelManager.Instance.PopupsManager.SpawnDamagePopup;
        damageable.OnHeal += LevelManager.Instance.PopupsManager.SpawnHealPopup;
        effectable.OnAffected += LevelManager.Instance.PopupsManager.SpawnStatusEffectPopup;
        Stats.OnStatChanged += EnemyTargeter.AddRadius;
        Stats.OnStatChanged += movement.OnSpeedChange;

    }

    protected void BaseInit(BaseUnitConfig givenConfig) => base.Init(givenConfig);

    protected virtual void OnDisable() //unsubscribe to events
    {
        if (ReferenceEquals(LevelManager.Instance, null)) return;
        if (ReferenceEquals(damageable, null)) return;
        if (ReferenceEquals(effectable, null)) return;
        damageable.OnDamageCalc -= LevelManager.Instance.PopupsManager.SpawnDamagePopup;
        damageable.OnHeal -= LevelManager.Instance.PopupsManager.SpawnHealPopup;
        effectable.OnAffected -= LevelManager.Instance.PopupsManager.SpawnStatusEffectPopup;
        if (hasHPBar)
        {
            damageable.OnDamageCalc -= hpBar.SetBarValue;
            damageable.OnHeal -= hpBar.SetBarBasedOnOwner;
        }
        Stats.OnStatChanged -= EnemyTargeter.AddRadius;
        Stats.OnStatChanged -= movement.OnSpeedChange;
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

    public void ClearUnitTImers()
    {
        foreach (ITimer iTimer in UnitTimers)
        {
            iTimer.RemoveThisTimer();
        }

        UnitTimers.Clear();
    }

    private void OnValidate()
    {
        boxCollider ??= GetComponent<BoxCollider2D>();
    }
}