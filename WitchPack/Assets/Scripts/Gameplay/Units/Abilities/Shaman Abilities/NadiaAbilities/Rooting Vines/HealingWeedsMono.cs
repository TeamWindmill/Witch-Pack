using Gameplay.Units.Abilities.AbilitySystem.BaseAbilities;
using Gameplay.Units.Damage_System;
using Sound;
using Tools.Time;

namespace Gameplay.Units.Abilities.Shaman_Abilities.NadiaAbilities.Rooting_Vines
{
    public class HealingWeedsMono : RootingVinesMono
    {
        private HealingWeeds _healingWeedsAbility;
        public override void Init(BaseUnit owner, CastingAbility ability, float lastingTime, float aoeRange)
        {
            base.Init(owner, ability, lastingTime, aoeRange);
            _healingWeedsAbility = ability as HealingWeeds;
        }

        protected override void OnRoot(Enemy.Enemy enemy)
        {
            base.OnRoot(enemy);
            enemy.Damageable.OnDeath += HerbalWeeds;
            TimerData<Enemy.Enemy> timerData = new TimerData<Enemy.Enemy>(tickTime: 1, data: enemy, tickAmount: _healingWeedsAbility.StatusEffects[0].Duration.Value, usingGameTime: true);
            Timer<Enemy.Enemy> timer = new Timer<Enemy.Enemy>(timerData);
            timer.OnTimerEnd += RemoveHerbalWeeds;
            TimerManager.AddTimer(timer);
            enemy.UnitTimers.Add(timer);
        }

        private void RemoveHerbalWeeds(Timer<Enemy.Enemy> timer)
        {
            timer.Data.Damageable.OnDeath -= HerbalWeeds;
        }

        private void HerbalWeeds(Damageable damageable, DamageDealer damageDealer)
        {
            damageDealer.Owner.Effectable.AddEffects(_healingWeedsAbility.HealStatusEffects, damageable.Owner.Affector);
            if (damageDealer.Owner is Shaman shaman)
            {
                shaman.ShamanVisualHandler.HealingWeedsEffect.Play();
            }
            SoundManager.PlayAudioClip(SoundEffectType.HealingWeeds);
        }

    }
}
