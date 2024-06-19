using System;

public class AbilityFactory
{
    public static Ability CreateAbility(AbilitySO config, BaseUnit owner)
    {
        
        switch (config)
        {
            //Passives
            case ConditionalStatPassiveSO ability:
                return new ConditionalStatPassive(ability, owner);
            
            case AffectedByUnitsStatPassiveSO ability:
                return new AffectedByUnitsStatPassive(ability, owner);
            
            case AttritionSO ability:
                return new Attrition(ability, owner);
            
            case StatPassiveSO ability:
                return new StatPassive(ability, owner);
            
            //Auto Attack
            case EnemyRangedAutoAttackSO ability:
                return new EnemyRangedAutoAttack(ability, owner);
            
            case EnemyMeleeAutoAttackSO ability:
                return new EnemyMeleeAutoAttack(ability, owner);
            
            case ShamanRangedAutoAttackSO ability:
                return new ShamanRangedAutoAttack(ability, owner);
            
            //Toor Abilities
            case ExperiencedHunterSO ability:
                return new ExperiencedHunter(ability, owner);
            
            case PiercingShotSO ability:
                return new PiercingShot(ability, owner);
            
            case RicochetSO ability:
                return new Ricochet(ability, owner);
            
            case MultiShotSO ability:
                return new MultiShot(ability, owner);
            
            //Nadia Abilities
            case HealingWeedsSO ability:
                return new HealingWeeds(ability, owner);
            
            case PoisonIvySO ability:
                return new PoisonIvy(ability, owner);
            
            case RootingVinesSO ability:
                return new RootingVines(ability, owner);
            
            case BlessingOfSwiftnessSO ability:
                return new BlessingOfSwiftness(ability, owner);
            
            case OverhealSO ability:
                return new Overheal(ability, owner);
            
            case HealSO ability:
                return new Heal(ability, owner);
            
            //Javan Abilities
            case HighImpactSO ability:
                return new HighImpactSmokeBomb(ability, owner);
            
            case SmokeBombSO ability:
                return new SmokeBomb(ability, owner);
            
            case CharmSO ability:
                return new Charm(ability, owner);
            
            case FireballSO ability:
                return new Fireball(ability, owner);
            
            //Lila Abilities
            case FortifySO ability:
                return new Fortify(ability, owner);
            
            case RockMonolithSO ability:
                return new RockMonolith(ability, owner);
            
        }

        throw new Exception($"{owner.UnitConfig.Name}'s Ability config not found in the ability factory");
    }
}