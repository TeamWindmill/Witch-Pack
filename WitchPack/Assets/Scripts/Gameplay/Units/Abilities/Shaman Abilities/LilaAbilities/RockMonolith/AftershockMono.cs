using Systems.Pool_System;
using Tools.Targeter;
using UnityEngine;

public class AftershockMono : MonoBehaviour , IPoolable
{
    [SerializeField] private LayerMask _targeterLayer;
    [SerializeField] private float _lifetime;
    private BaseUnit _owner;
    private RockMonolith _ability;
    private Aftershock _aftershockAbility;
    private int _chainIndex;
    private bool _chainReaction;
    private ITimer _lifetimeTimer;
    private const float RANGE_TO_OVERLAP_CIRCLE_RATIO = 0.4f;
    public void Init(BaseUnit owner, RockMonolith ability, bool chainReaction, int chainDamageReductionIndex)
    {
        _owner = owner;
        if (ability is Aftershock aftershock) _aftershockAbility = aftershock;
        _ability = ability;
        _chainIndex = chainDamageReductionIndex;
        _chainReaction = chainReaction;
        SetSize(owner.Stats[StatType.BaseRange].Value);
        SoundManager.PlayAudioClip(SoundEffectType.MonolithofRockExplosion);
        
        foreach (var enemy in TargetingHelper<Enemy>.GetAvailableTargets(transform.position,_owner.Stats[StatType.BaseRange].Value * RANGE_TO_OVERLAP_CIRCLE_RATIO,_targeterLayer))
        {
            if(enemy.Effectable.ContainsStatusEffect(StatusEffectVisual.Charm)) return;
            var damageHandler = new DamageHandler(CalculateDamage());
            if (_chainReaction) damageHandler.OnKill += OnEnemyDeath;
            enemy.Damageable.TakeDamage(_owner.DamageDealer, damageHandler, _ability, false);
        }

        _lifetimeTimer = TimerManager.AddTimer(_lifetime, Disable);
    }
    private void SetSize(float range)
    {
        transform.localScale = new Vector3(range,range,range);
    }

    private float CalculateDamage()
    {
        var damage = _ability.GetAbilityStatValue(AbilityStatType.Damage) + _ability.RockMonolithConfig.DamageIncreasePerHit * _ability.DamageIncrement;
        return damage * _ability.GetAbilityStatValue(AbilityStatType.FinalDamageModifier);
    }

    private void OnEnemyDeath(Damageable damageable, DamageHandler damageHandler)
    {
        damageHandler.OnKill -= OnEnemyDeath;
        var aftershockMono = PoolManager.GetPooledObject<AftershockMono>();
        var enemy = damageable.Owner as Enemy;
        aftershockMono.transform.position = enemy.transform.position;
        aftershockMono.gameObject.SetActive(true);
        aftershockMono.Init(_owner, _ability, true, _chainIndex + 1);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _lifetimeTimer?.RemoveThisTimer();
    }

    public GameObject PoolableGameObject => gameObject;
}