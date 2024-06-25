using System;
using UnityEngine;

public class AftershockMono : MonoBehaviour
{
    public event Action OnActivation;
    [SerializeField] private GroundColliderTargeter _targeter;
    [SerializeField] private float _lifetime;

    private BaseUnit _owner;
    private RockMonolith _ability;
    private Aftershock _aftershockAbility;
    private int _chainIndex;
    private bool _chainReaction;
    private float _range;

    public void Init(BaseUnit owner, RockMonolith ability, bool chainReaction,int chainDamageReductionIndex)
    {
        _owner = owner;
        if (ability is Aftershock aftershock) _aftershockAbility = aftershock;
        _ability = ability;
        _chainIndex = chainDamageReductionIndex;
        _chainReaction = chainReaction;
        SetRange(owner.Stats[StatType.BaseRange].Value);
        TimerManager.AddTimer(0.1f,Activate); //gives time for the targeter to add all the targets

    }

    private void SetRange(float range)
    {
        transform.localScale = new Vector3(range,range,range);
    }

    public void Activate()
    {
        foreach (var collider in _targeter.AvailableTargets)
        {
            if (collider.Unit is Enemy enemy)
            {
                var damageHandler = new DamageHandler(CalculateDamage());
                if(_chainReaction) damageHandler.OnKill += OnEnemyDeath;
                enemy.Damageable.TakeDamage(_owner.DamageDealer,damageHandler,_ability,false);
            }
        }

        OnActivation?.Invoke();
        TimerManager.AddTimer(_lifetime, Disable);
    }

    private float CalculateDamage()
    {
        return _ability.GetAbilityStatValue(AbilityStatType.Damage) + _ability.RockMonolithConfig.DamageIncreasePerHit * _ability.DamageIncrement;
        //var damageReductionInPercent = _chainIndex * _aftershockAbility.AftershockConfig.DamageReductionPerBounceInPercent;
        //float damageReduction = 1 - ((float)damageReductionInPercent / 100);
    }

    private void OnEnemyDeath(Damageable damageable, DamageHandler damageHandler)
    {
        var aftershockMono = LevelManager.Instance.PoolManager.AftershockPool.GetPooledObject();
        var enemy = damageable.Owner as Enemy;
        aftershockMono.transform.position = enemy.transform.position;
        aftershockMono.gameObject.SetActive(true);
        aftershockMono.Init(_owner,_ability,true,_chainIndex + 1);
        damageHandler.OnKill -= OnEnemyDeath;
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}