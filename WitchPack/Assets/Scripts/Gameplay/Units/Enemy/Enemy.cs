using PathCreation;
using Sirenix.OdinInspector;
using UnityEngine;

public class Enemy : BaseUnit
{
    [SerializeField, TabGroup("Visual")] private EnemyVisualHandler enemyVisualHandler;
    [SerializeField, TabGroup("Combat")] private EnemyAI enemyAI;

    public override Stats BaseStats => EnemyConfig.BaseStats;
    public EnemyConfig EnemyConfig { get; private set; }
    public EnemyAbilityHandler EnemyAbilityHandler { get; private set; }
    public int CoreDamage { get; private set; }
    public int EnergyPoints { get; private set; }
    public EnemyMovement EnemyMovement { get; private set; }
    public PathCreator Path { get; private set; }
    public EnemyAI EnemyAI => enemyAI;
    public EnemyVisualHandler EnemyVisualHandler => enemyVisualHandler;

    [SerializeField, TabGroup("Visual")] private EnemyAnimator enemyAnimator;


    private void OnValidate()
    {
        enemyAnimator ??= GetComponentInChildren<EnemyAnimator>();
    }
    public override void Init(BaseUnitConfig givenConfig)
    {
        EnemyConfig = givenConfig as EnemyConfig;
        base.Init(EnemyConfig);
        Damageable.Init();
        transform.localScale = new Vector3(EnemyConfig.Size,EnemyConfig.Size,EnemyConfig.Size);
        Path = EnemyConfig.Path;
        CoreDamage = EnemyConfig.CoreDamage;
        EnergyPoints = EnemyConfig.EnergyPoints;
        
        EnemyAbilityHandler = new EnemyAbilityHandler(this);
        AbilityHandler = EnemyAbilityHandler;
        EnemyAbilityHandler.IntializeAbilities();
        
        ShamanTargeter.SetRadius(Stats[StatType.BaseRange].Value);
        EnemyTargeter.SetRadius(Stats[StatType.BaseRange].Value);
        EnemyMovement = new EnemyMovement(this);
        enemyAnimator.Init(this);
        enemyVisualHandler.Init(this, givenConfig);
        enemyAI.Init(this);
        Movement.ToggleMovement(false);
        #region Events
        //remember to unsubscribe in OnDisable!!!
        enemyVisualHandler.OnSpriteFlip += enemyAnimator.FlipAnimations;
        Effectable.OnAffected += enemyVisualHandler.EffectHandler.PlayEffect;
        Effectable.OnEffectRemoved += enemyVisualHandler.EffectHandler.DisableEffect;
        Damageable.OnHitGFX += GetHitSFX;
        Damageable.OnDeathGFX += DeathSFX;
        //AutoAttackCaster.OnAttack += AttackSFX;
        

        #endregion

        Initialized = true;
    }

    
    protected override void OnDisable() //enemy death
    {
        base.OnDisable();
        if(!Initialized) return;
        if (ReferenceEquals(LevelManager.Instance, null)) return;
        enemyAI.Disable();
        Damageable.Disable();
        Damageable.OnHitGFX -= GetHitSFX;
        Damageable.OnDeathGFX -= DeathSFX;
        //if (AutoAttackCaster != null) AutoAttackCaster.OnAttack -= AttackSFX;
        Effectable.OnAffected -= enemyVisualHandler.EffectHandler.PlayEffect;
        Effectable.OnEffectRemoved -= enemyVisualHandler.EffectHandler.DisableEffect;
        enemyVisualHandler.OnSpriteFlip -= enemyAnimator.FlipAnimations;

        Initialized = false;
    }
    
    

    #region SFX

    private void GetHitSFX(bool isCrit)
    {
        SoundManager.PlayAudioClip(isCrit ? SoundEffectType.EnemyGetHitCrit : SoundEffectType.EnemyGetHit);
        enemyVisualHandler.HitEffect.Play();
    }
    public void AbilityCastSFX(CastingAbilitySO abilitySo) => SoundManager.PlayAudioClip(abilitySo.SoundEffectType);
    private void DeathSFX() => SoundManager.PlayAudioClip(SoundEffectType.EnemyDeath);
    //private void AttackSFX() => SoundManager.Instance.PlayAudioClip(SoundEffectType.EnemyAttack);
    
    #endregion
    
}
