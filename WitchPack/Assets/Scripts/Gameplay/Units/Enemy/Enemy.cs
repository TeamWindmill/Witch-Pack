using Sirenix.OdinInspector;
using UnityEngine;

public class Enemy : BaseUnit
{
    public EnemyConfig EnemyConfig { get => enemyConfig; }
    public int CoreDamage => _coreDamage;
    public int EnergyPoints => _energyPoints;
    public override StatSheet BaseStats => enemyConfig.BaseStats;
    public EnemyMovement EnemyMovement => _enemyMovement;

    [SerializeField, TabGroup("Visual")] private EnemyAnimator enemyAnimator;
    private int _coreDamage;
    private int _energyPoints;
    //testing 
    public int Id => gameObject.GetHashCode();

    private EnemyAgro _enemyAgro;
    private EnemyMovement _enemyMovement;
    private EnemyConfig enemyConfig;
    private int pointIndex;

    private void OnValidate()
    {
        enemyAnimator ??= GetComponentInChildren<EnemyAnimator>();
    }
    public override void Init(BaseUnitConfig givenConfig)
    {
        pointIndex = 0;
        enemyConfig = givenConfig as EnemyConfig;
        base.Init(enemyConfig);
        _coreDamage = enemyConfig.CoreDamage;
        _energyPoints = enemyConfig.EnergyPoints;
        Targeter.SetRadius(Stats.BonusRange);
        _enemyAgro = new EnemyAgro(this);
        _enemyMovement = new EnemyMovement(this);
        enemyAnimator.Init(this);
    }

    private void Update()
    {
        _enemyMovement.FollowPath();
    }

    private void SetNextDest()
    {
        //pointIndex++;
        // if (givenPath.Waypoints.Count <= pointIndex)//if reached the end of the path target nexus 
        // {
        //     gameObject.SetActive(false);
        // }
        // else
        // {
        //     Debug.Log("set dest");
        //     Movement.SetDest(givenPath.Waypoints[pointIndex].position);
        // }

    }

    protected override void OnDisable()
    {
        Movement.OnDestinationReached -= SetNextDest;
        _enemyAgro?.OnDisable();
        base.OnDisable();
    }

    
}
