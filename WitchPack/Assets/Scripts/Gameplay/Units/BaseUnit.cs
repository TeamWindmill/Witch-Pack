using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    [SerializeField, TabGroup("Combat")] private Damageable damageable;
    [SerializeField, TabGroup("Combat")] private DamageDealer damageDealer;
    [SerializeField, TabGroup("Combat")] private Affector affector;
    [SerializeField, TabGroup("Combat")] private Effectable effectable;
    [SerializeField, TabGroup("Combat")] private List<BaseAbility> knownAbilities = new List<BaseAbility>();
    [SerializeField, TabGroup("Combat")] private OffensiveAbility autoAttack;
    [SerializeField, TabGroup("Stats")] private StatSheet baseStats;
    [SerializeField, TabGroup("Stats")] private UnitStats stats;
    private List<UnitCastingHandler> castingHandlers = new List<UnitCastingHandler>();
    private AutoAttackHandler autoAttackHandler;


    public Damageable Damageable { get => damageable; }
    public DamageDealer DamageDealer { get => damageDealer; }
    public Affector Affector { get => affector; }
    public Effectable Effectable { get => effectable; }
    public StatSheet BaseStats { get => baseStats; }
    public UnitStats Stats { get => stats; }
    public List<UnitCastingHandler> CastingHandlers { get => castingHandlers; }
    public List<BaseAbility> KnownAbilities { get => knownAbilities; }
    public OffensiveAbility AutoAttack { get => autoAttack; }
    public AutoAttackHandler AutoAttackHandler { get => autoAttackHandler; }

    //movement comp
    //state machine -> heros and enemies essentially work the same only heroes can be told where to go, everything else is automatic 

    protected virtual void Awake()
    {
        damageable = new Damageable(this);
        damageDealer = new DamageDealer(this, autoAttack);
        affector = new Affector(this);
        effectable = new Effectable(this);
        stats = new UnitStats(this);
        autoAttackHandler = new AutoAttackHandler(this, autoAttack);
        IntializeCastingHandlers();
    }

    //testing 
    private void Update()
    {
        foreach (var item in CastingHandlers)
        {
            item.CastAbility();
        }
        if (!ReferenceEquals(autoAttack, null))
        {
            AutoAttackHandler.Attack();

        }
    }


    private void IntializeCastingHandlers()
    {
        foreach (var item in KnownAbilities)
        {
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

}
