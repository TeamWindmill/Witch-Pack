using Gameplay.Units.Abilities.Shaman_Abilities.NadiaAbilities.Visuals;
using Gameplay.Units.Damage_System;
using Gameplay.Units.Stats;
using UnityEngine;

namespace Gameplay.Units.Visual
{
    public class EnemyEffectHandler : UnitEffectHandler
    {
    
        public PoisonIvyVisuals PoisonIvyVisuals => poisonIvyVisuals;

        [SerializeField] private RootingVinesVisuals[] rootingVinesVisuals;
        [SerializeField] private PoisonIvyVisuals poisonIvyVisuals;
        public override void PlayEffect(Effectable effectable, Affector affector, StatusEffect statusEffect)
        {
            switch (statusEffect.StatusEffectVisual)
            {
                case StatusEffectVisual.Root:
                case StatusEffectVisual.HealingRoot:
                case StatusEffectVisual.LongerRoot:
                case StatusEffectVisual.PoisonRoot:
                    foreach (var visual in rootingVinesVisuals)
                    {
                        visual.Init(statusEffect.Duration);
                    }
                    break;
            }
            base.PlayEffect(effectable, affector, statusEffect);
        
        }
    }
}