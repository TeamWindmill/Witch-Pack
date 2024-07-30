using Gameplay.Units.Abilities.AbilitySystem.AbilityStats;
using Gameplay.Units.Abilities.AbilitySystem.BaseAbilities;
using Gameplay.Units.Abilities.AbilitySystem.BaseConfigs;
using Gameplay.Units.Abilities.Shaman_Abilities.LilaAbilities.RockMonolith.Configs;
using Gameplay.Units.Damage_System;
using Gameplay.Units.Enemy.EnemyAIBehavior.GroundEnemies.States;
using Managers;
using Sound;
using Tools.Time;

namespace Gameplay.Units.Abilities.Shaman_Abilities.LilaAbilities.RockMonolith
{
    public class RockMonolith : OffensiveAbility
    {
        public int DamageIncrement { get; protected set; }
        public RockMonolithSO RockMonolithConfig;
        protected Shaman.Shaman _shamanOwner;
        private AftershockMono _activeAftershock;
        public RockMonolith(OffensiveAbilitySO config, BaseUnit owner) : base(config, owner)
        {
            _shamanOwner = Owner as Shaman.Shaman;
            RockMonolithConfig = config as RockMonolithSO;
            abilityStats.Add(new AbilityStat(AbilityStatType.Duration,RockMonolithConfig.Duration));
            abilityStats.Add(new AbilityStat(AbilityStatType.FinalDamageModifier,1));
            //abilityStats.Add(new AbilityStat(AbilityStatType.Size,_config.TauntRadius));
        }

        public override bool CastAbility(out IDamagable target)
        {
            var targets = Owner.EnemyTargetHelper.GetAvailableTargets(TargetData);
        
            _shamanOwner.Effectable.AddEffects(StatusEffects,_shamanOwner.Affector);

            TimerManager.AddTimer(GetAbilityStatValue(AbilityStatType.Duration), OnTauntEnd,true);

            _shamanOwner.Damageable.OnHitGFX += IncrementDamage;

            if (targets.Count >= RockMonolithConfig.MinEnemiesForTaunt)
            {
                foreach (var enemy in targets)
                {
                    enemy.ShamanTargetHelper.ApplyTaunt(_shamanOwner, GetAbilityStatValue(AbilityStatType.Duration));
                    enemy.EnemyAI.SetState(typeof(Taunt));
                }

                target = targets[0];
                return true;
            }
        
            target = null;
            return false;
        }

        public override bool CheckCastAvailable()
        {
            Enemy.Enemy target = Owner.EnemyTargetHelper.GetTarget(TargetData);
            if (!ReferenceEquals(target, null)) //might want to change to multiple enemies near her
            {
                return true;
            }

            return false;
        }

        protected virtual void OnTauntEnd()
        {
            _activeAftershock = PoolManager.GetPooledObject<AftershockMono>();
            _activeAftershock.transform.position = Owner.transform.position;
            _activeAftershock.gameObject.SetActive(true);
            _activeAftershock.Init(_shamanOwner,this,false,0);
            _activeAftershock.OnActivation += OnAftershockActivation;
        }
    
        protected void IncrementDamage(bool isCrit)
        {
            SoundManager.PlayAudioClip(SoundEffectType.MonolithofRockShield);
            DamageIncrement++;
        }

        private void OnAftershockActivation()
        {
            _activeAftershock.OnActivation -= OnAftershockActivation;
            _activeAftershock = null;
            _shamanOwner.Damageable.OnHitGFX -= IncrementDamage;
            DamageIncrement = 0;
        }
    }
}