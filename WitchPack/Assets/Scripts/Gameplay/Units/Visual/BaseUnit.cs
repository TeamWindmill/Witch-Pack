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
    [SerializeField, TabGroup("Combat")] private UnitAutoCaster _autoCaster;
    [SerializeField, TabGroup("Combat")] private BoxCollider2D boxCollider;
    [SerializeField, TabGroup("Combat")] private GroundCollider groundCollider;
    [SerializeField] private Transform _castPos;

    [SerializeField] private EnemyTargeter enemyTargeter;
    [SerializeField] private ShamanTargeter shamanTargeter;

    private UnitTargetHelper<Shaman> shamanTargetHelper;
    private UnitTargetHelper<Enemy> enemyTargetHelper;



    [SerializeField, TabGroup("Stats")] private UnitStats stats;
    [SerializeField, TabGroup("Movement")] private UnitMovement movement;
    [SerializeField, TabGroup("Visual")] private UnitVisualHandler unitVisual;


    [SerializeField, TabGroup("Visual")] private bool hasHPBar;
    [SerializeField, ShowIf(nameof(hasHPBar)), TabGroup("Visual")] private HP_Bar hpBar;


    private AutoAttackHandler autoAttackHandler;


    public HP_Bar HpBar => hpBar;
    public UnitVisualHandler UnitVisual => unitVisual;
    public Damageable Damageable { get => damageable; }
    public DamageDealer DamageDealer { get => damageDealer; }
    public Affector Affector { get => affector; }
    public Effectable Effectable { get => effectable; }
    public virtual StatSheet BaseStats { get { return null; } }
    public UnitStats Stats { get => stats; }
    public OffensiveAbility AutoAttack { get => autoAttack; }
    public AutoAttackHandler AutoAttackHandler { get => autoAttackHandler; }
    public UnitAutoCaster AutoCaster { get => _autoCaster; }
    public UnitMovement Movement { get => movement; }
    
    public Transform CastPos => _castPos;

    public EnemyTargeter EnemyTargeter { get => enemyTargeter; }
    public ShamanTargeter ShamanTargeter { get => shamanTargeter; }
    public UnitTargetHelper<Shaman> ShamanTargetHelper { get => shamanTargetHelper; }
    public UnitTargetHelper<Enemy> EnemyTargetHelper { get => enemyTargetHelper; }





    //movement comp
    //state machine -> heros and enemies essentially work the same only heroes can be told where to go, everything else is automatic 

    public virtual void Init(BaseUnitConfig givenConfig)
    {
        stats = new UnitStats(BaseStats);
        damageable = new Damageable(this);
        damageDealer = new DamageDealer(this, autoAttack);
        affector = new Affector(this);
        effectable = new Effectable(this);
        autoAttackHandler = new AutoAttackHandler(this, autoAttack);
        shamanTargetHelper = new UnitTargetHelper<Shaman>(ShamanTargeter, this);
        enemyTargetHelper = new UnitTargetHelper<Enemy>(EnemyTargeter, this);
        AutoCaster.SetUp(this);
        Movement.SetUp(this);
        groundCollider.Init(this);
        unitVisual.Init(this, givenConfig);
        ToggleCollider(true);
        if (hasHPBar)
        {
            hpBar.gameObject.SetActive(true);
            hpBar.Init(damageable.MaxHp, unitType);
            damageable.OnDamageCalc += hpBar.SetBarValue;
            damageable.OnHeal += hpBar.SetBarBasedOnOwner;
        }
        damageable.OnDamageCalc += LevelManager.Instance.PopupsManager.SpawnDamagePopup;
        effectable.OnAffected += LevelManager.Instance.PopupsManager.SpawnStatusEffectPopup;
        stats.OnHpRegenChange += damageable.SetRegenerationTimer;
        if(unitVisual.EffectHandler)
        { 
            effectable.OnAffectedGFX += unitVisual.EffectHandler.PlayEffect;
            effectable.OnEffectRemovedGFX += unitVisual.EffectHandler.DisableEffect;
        }
    }

    public void ToggleCollider(bool state)
    {
        boxCollider.enabled = state;
    }

    public void DisableAttacker()
    {
        _autoCaster.CanCast = false;

    }
    public void EnableAttacker()
    {
        _autoCaster.CanCast = true;
    }

    public void OnDeathAnimation()
    {
        Movement.ToggleMovement(false);
        ToggleCollider(false);
        damageable.ToggleHitable(false);
    }

    private void OnDestroy()
    {
        if (hasHPBar) damageable.OnDamageCalc -= hpBar.SetBarValue;

        stats.OnHpRegenChange -= damageable.SetRegenerationTimer;
    }

    private void OnValidate()
    {
        boxCollider ??= GetComponent<BoxCollider2D>();
    }

    protected virtual void OnDisable()
    {
        if(ReferenceEquals(LevelManager.Instance,null)) return;
        damageable.OnDamageCalc -= LevelManager.Instance.PopupsManager.SpawnDamagePopup;
        effectable.OnAffected -= LevelManager.Instance.PopupsManager.SpawnStatusEffectPopup;
        damageable.OnHeal -= hpBar.SetBarBasedOnOwner;
    }
}
