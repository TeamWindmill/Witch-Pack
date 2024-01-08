using Sirenix.OdinInspector;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    [SerializeField, TabGroup("Combat")] private Damageable damageable;
    [SerializeField, TabGroup("Combat")] private DamageDealer damageDealer;
    [SerializeField, TabGroup("Combat")] private Affector affector;
    [SerializeField, TabGroup("Combat")] private Effectable effectable;
    [SerializeField, TabGroup("Combat")] private OffensiveAbility autoAttack;
    [SerializeField, TabGroup("Combat")] private UnitAutoAttacker autoAttacker;
    [SerializeField, TabGroup("Stats")] private UnitStats stats;
    [SerializeField, TabGroup("Movement")] private UnitMovement movement;
    [SerializeField, TabGroup("Visual")] private UnitVisualHandler unitVisual;

    private AutoAttackHandler autoAttackHandler;


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
        damageable = new Damageable(this);
        damageDealer = new DamageDealer(this, autoAttack);
        affector = new Affector(this);
        effectable = new Effectable(this);
        stats = new UnitStats(this);
        autoAttackHandler = new AutoAttackHandler(this, autoAttack);
        Movement.SetSpeed(Stats.MovementSpeed);
        Stats.OnStatChanged += Movement.AddSpeed;
        AutoAttacker.SetUp(this);
        Movement.SetUp(this);
        unitVisual.Init(this, givenConfig);
    }


    protected void DisableAttacker(Vector3 pos)
    {
        autoAttacker.CanAttack = false;

    }
    protected void EnableAttacker(Vector3 pos)
    {
        autoAttacker.CanAttack = true;
    }

   

}
