using UnityEngine;

public class AftershockMono : MonoBehaviour
{
    [SerializeField] private EnemyTargeter _targeter;
    [SerializeField] private float _lifetime;
    
    
    public void Activate(BaseUnit Owner, RockMonolith ability, int damageIncrement,bool chainReaction)
    {
        foreach (var enemy in _targeter.AvailableTargets)
        {
            var damageHandler = new DamageHandler(ability.GetAbilityStatValue(AbilityStatType.Damage) + ability.RockMonolithConfig.DamageIncreasePerHit * damageIncrement);
            enemy.Damageable.TakeDamage(Owner.DamageDealer,damageHandler,ability,false);
        }

        TimerManager.AddTimer(_lifetime, Disable);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}