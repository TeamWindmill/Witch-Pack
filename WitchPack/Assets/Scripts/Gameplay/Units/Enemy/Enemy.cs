using PathCreation;
using Sirenix.OdinInspector;
using Systems.StateMachine;
using UnityEngine;

public class Enemy : BaseUnit
{
    [SerializeField, TabGroup("Visual")] private EnemyVisualHandler enemyVisualHandler;
    [SerializeField, TabGroup("Combat")] private EnemyAI enemyAI;

    public EnemyConfig EnemyConfig { get => enemyConfig; }
    public int CoreDamage => _coreDamage;
    public int EnergyPoints => _energyPoints;
    public override Stats BaseStats => enemyConfig.BaseStats;
    public EnemyMovement EnemyMovement => _enemyMovement;
    public EnemyAI EnemyAI => enemyAI;
    public EnemyVisualHandler EnemyVisualHandler => enemyVisualHandler;
    public PathCreator Path => _path;

    [SerializeField, TabGroup("Visual")] private EnemyAnimator enemyAnimator;
    private int _coreDamage;
    private int _energyPoints;
    //testing 
    public int Id => gameObject.GetHashCode();

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
        _enemyMovement = new EnemyMovement(this);
        enemyAnimator.Init(this);
        enemyVisualHandler.Init(this, givenConfig);
        IntializeAbilities();
        enemyAI.Init(this);
        Movement.ToggleMovement(false);
        #region Events
        //remember to unsubscribe in OnDisable!!!
        enemyVisualHandler.OnSpriteFlip += enemyAnimator.FlipAnimations;
        Effectable.OnAffectedVFX += enemyVisualHandler.EffectHandler.PlayEffect;
        Effectable.OnEffectRemovedVFX += enemyVisualHandler.EffectHandler.DisableEffect;
        Damageable.OnHitGFX += GetHitSFX;
        Damageable.OnDeathGFX += DeathSFX;
        AutoAttackCaster.OnAttack += AttackSFX;
        

        #endregion

        Initialized = true;
    }

    private void IntializeAbilities()
    {
        foreach (var ability in enemyConfig.Abilities)
        {
            if (ability is not Passive)
            {
                ability.OnSetCaster(this);
                castingHandlers.Add(new AbilityCaster(this, ability as CastingAbility));
            }
            else
            {
                (ability as Passive).SubscribePassive(this);
            }
        }

        AutoCaster.Init(this, false);
    }
    protected override void OnDisable() //enemy death
    {
        base.OnDisable();
        if(!Initialized) return;
        if (ReferenceEquals(LevelManager.Instance, null)) return;
        enemyAI.OnDisable();
        Damageable.OnHitGFX -= GetHitSFX;
        Damageable.OnDeathGFX -= DeathSFX;
        if (AutoAttackCaster != null) AutoAttackCaster.OnAttack -= AttackSFX;
        Effectable.OnAffectedVFX -= enemyVisualHandler.EffectHandler.PlayEffect;
        Effectable.OnEffectRemovedVFX -= enemyVisualHandler.EffectHandler.DisableEffect;
        enemyVisualHandler.OnSpriteFlip -= enemyAnimator.FlipAnimations;

        Initialized = false;
    }

    #region SFX

    private void GetHitSFX(bool isCrit)
    {
        SoundManager.Instance.PlayAudioClip(isCrit ? SoundEffectType.EnemyGetHitCrit : SoundEffectType.EnemyGetHit);
        enemyVisualHandler.HitEffect.Play();
    }
    public void AbilityCastSFX(CastingAbility ability) => SoundManager.Instance.PlayAudioClip(ability.SoundEffectType);
    private void DeathSFX() => SoundManager.Instance.PlayAudioClip(SoundEffectType.EnemyDeath);
    private void AttackSFX() => SoundManager.Instance.PlayAudioClip(SoundEffectType.EnemyAttack);
    
    #endregion
    

    
}
