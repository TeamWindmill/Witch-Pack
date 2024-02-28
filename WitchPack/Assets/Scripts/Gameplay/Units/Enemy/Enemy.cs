using PathCreation;
using Sirenix.OdinInspector;
using Systems.StateMachine;
using UnityEngine;

public class Enemy : BaseUnit
{
    [SerializeField, TabGroup("Visual")] private EnemyVisualHandler unitVisual;
    [SerializeField, TabGroup("Combat")] private BaseStateMachine stateMachine;

    public EnemyConfig EnemyConfig { get => enemyConfig; }
    public int CoreDamage => _coreDamage;
    public int EnergyPoints => _energyPoints;
    public override StatSheet BaseStats => enemyConfig.BaseStats;
    public EnemyMovement EnemyMovement => _enemyMovement;
    public EnemyAI EnemyAI => _enemyAI;
    public EnemyVisualHandler UnitVisual => unitVisual;
    public PathCreator Path => _path;

    [SerializeField, TabGroup("Visual")] private EnemyAnimator enemyAnimator;
    private int _coreDamage;
    private int _energyPoints;
    //testing 
    public int Id => gameObject.GetHashCode();

    private EnemyAI _enemyAI;
    private EnemyMovement _enemyMovement;
    private EnemyConfig enemyConfig;
    private PathCreator _path;
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
        _path = enemyConfig.Path;
        _coreDamage = enemyConfig.CoreDamage;
        _energyPoints = enemyConfig.EnergyPoints;
        ShamanTargeter.SetRadius(Stats.BonusRange);
        EnemyTargeter.SetRadius(Stats.BonusRange);
        stateMachine.Init(this);
        _enemyMovement = new EnemyMovement(this);
        enemyAnimator.Init(this);
        unitVisual.Init(this, givenConfig);
        Effectable.OnAffectedGFX += unitVisual.EffectHandler.PlayEffect;
        Effectable.OnEffectRemovedGFX += unitVisual.EffectHandler.DisableEffect;

        #region Events
        //remember to unsubscribe in OnDisable!!!

        Damageable.OnHitGFX += GetHitSFX;
        Damageable.OnDeathGFX += DeathSFX;
        AutoAttackHandler.OnAttack += AttackSFX;
        Movement.OnDestinationSet += AutoCaster.DisableCaster;
        Movement.OnDestinationReached += AutoCaster.EnableCaster;

        #endregion
        
    }
    protected override void OnDisable()
    {
        _enemyAI?.OnDisable();
        base.OnDisable();
        Damageable.OnHitGFX -= GetHitSFX;
        Damageable.OnDeathGFX -= DeathSFX;
        if (AutoAttackHandler != null) AutoAttackHandler.OnAttack -= AttackSFX;
        Movement.OnDestinationSet -= AutoCaster.DisableCaster;
        Movement.OnDestinationReached -= AutoCaster.EnableCaster;
        Effectable.OnAffectedGFX -= unitVisual.EffectHandler.PlayEffect;
        Effectable.OnEffectRemovedGFX -= unitVisual.EffectHandler.DisableEffect;
    }
    private void Update()
    {
        _enemyMovement.FollowPath();
    }

    #region SFX
    private void GetHitSFX(bool isCrit) => SoundManager.Instance.PlayAudioClip(isCrit ? SoundEffectType.EnemyGetHitCrit : SoundEffectType.EnemyGetHit);
    private void DeathSFX() => SoundManager.Instance.PlayAudioClip(SoundEffectType.EnemyDeath);
    private void AttackSFX() => SoundManager.Instance.PlayAudioClip(SoundEffectType.EnemyAttack);
    
    #endregion
    

    
}
