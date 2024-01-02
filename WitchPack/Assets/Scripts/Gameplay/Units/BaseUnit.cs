using Sirenix.OdinInspector;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    [SerializeField, TabGroup("Combat")] private Damageable damageable;
    [SerializeField, TabGroup("Combat")] private DamageDealer damageDealer;
    [SerializeField, TabGroup("Combat")] private Affector affector;
    [SerializeField, TabGroup("Combat")] private Effectable effectable;
    [SerializeField, TabGroup("Stats")] private StatSheet baseStats;
    [SerializeField, TabGroup("Stats")] private UnitStats stats;


    public Damageable Damageable { get => damageable; }
    public DamageDealer DamageDealer { get => damageDealer; }
    public Affector Affector { get => affector; }
    public Effectable Effectable { get => effectable; }
    public StatSheet BaseStats { get => baseStats; }
    public UnitStats Stats { get => stats; }

    //movement comp
    //state machine -> heros and enemies essentially work the same only heroes can be told where to go, everything else is automatic 

    private void Awake()
    {
        damageable = new Damageable(this);
        damageDealer = new DamageDealer(this);
        affector = new Affector(this);
        effectable = new Effectable(this);
    }
}
