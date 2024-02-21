using Sirenix.OdinInspector;
using UnityEngine;

public class Enemy : BaseUnit
{
    public EnemyConfig EnemyConfig { get => enemyConfig; }
    public int CoreDamage => _coreDamage;
    public int EnergyPoints => _energyPoints;
    public override StatSheet BaseStats => enemyConfig.BaseStats;
    public EnemyMovement EnemyMovement => _enemyMovement;
    public EnemyAgro EnemyAgro => _enemyAgro;

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
        ShamanTargeter.SetRadius(Stats.BonusRange);
        EnemyTargeter.SetRadius(Stats.BonusRange);
        _enemyAgro = new EnemyAgro(this);
        _enemyMovement = new EnemyMovement(this);
        enemyAnimator.Init(this);

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
        _enemyAgro?.OnDisable();
        base.OnDisable();
        Damageable.OnHitGFX -= GetHitSFX;
        Damageable.OnDeathGFX -= DeathSFX;
        AutoAttackHandler.OnAttack -= AttackSFX;
        Movement.OnDestinationSet -= AutoCaster.DisableCaster;
        Movement.OnDestinationReached -= AutoCaster.EnableCaster;
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
