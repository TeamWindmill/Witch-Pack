using PathCreation;
using Sirenix.OdinInspector;
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
    private EnemyMovement _enemyMovement;
    private EnemyConfig enemyConfig;
    private PathCreator _path;


    private void OnValidate()
    {
        enemyAnimator ??= GetComponentInChildren<EnemyAnimator>();
    }
    public override void Init(BaseUnitConfig givenConfig)
    {
        enemyConfig = givenConfig as EnemyConfig;
        base.Init(enemyConfig);
        Damageable.Init();
        transform.localScale = new Vector3(enemyConfig.Size,enemyConfig.Size,enemyConfig.Size);
        _path = enemyConfig.Path;
        _coreDamage = enemyConfig.CoreDamage;
        _energyPoints = enemyConfig.EnergyPoints;
        ShamanTargeter.SetRadius(Stats[StatType.BaseRange].Value);
        EnemyTargeter.SetRadius(Stats[StatType.BaseRange].Value);
        _enemyMovement = new EnemyMovement(this);
        enemyAnimator.Init(this);
        enemyVisualHandler.Init(this, givenConfig);
        IntializeAbilities();
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
    
    private void IntializeAbilities()
    {
        foreach (var abilitySo in enemyConfig.Abilities)
        {
            var ability = AbilityFactory.CreateAbility(abilitySo, this);
            if (ability is PassiveAbility passive)
            {
                passive.SubscribePassive();
            }
            else if (ability is CastingAbility castingAbility)
            {
                //abilitySo.OnSetCaster(this);
                castingHandlers.Add(new AbilityCaster(this, castingAbility));
            }
        }

        AutoCaster.Init(this, false);
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
