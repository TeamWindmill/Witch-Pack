public class Heal : CastingAbility
{
    private HealSO _config;
    public Heal(HealSO config, BaseUnit owner) : base(config, owner)
    {
        _config = config;
        abilityStats.Add(new AbilityStat(AbilityStatType.Heal,config.HealAmount));
    }

    public override bool CastAbility()
    {
        Shaman target = Owner.ShamanTargetHelper.GetTarget(TargetData); 
        if (!ReferenceEquals(target, null)) // any shaman in range?
        {
            // Check if caster has lower hp (ratio) than lowest hp target
            float maxHp = Owner.Stats[StatType.MaxHp].Value;
            float casterCurrentHpRatio = Owner.Damageable.CurrentHp / maxHp;
            maxHp = target.Stats[StatType.MaxHp].Value;
            float targetCurrentHpRatio = target.Damageable.CurrentHp / maxHp;
            if (casterCurrentHpRatio < targetCurrentHpRatio)
            {
                target = Owner as Shaman;
            }

            if (!HasAbilityBehavior(AbilityBehavior.HealOnFullHealth))
            {
                if(target.Damageable.CurrentHp == target.Damageable.MaxHp)
                {
                    return false;
                }
            }

            HealTarget(target, Owner);
            return true;
        }
        else // no other injured shamans
        {
            if (HasAbilityBehavior(AbilityBehavior.HealOnFullHealth))
            {
                HealTarget(Owner as Shaman, Owner);
                return true;
            }
            
            if(Owner.Damageable.CurrentHp < Owner.Stats[StatType.MaxHp].Value) // check if caster is injured
            {
                HealTarget(Owner as Shaman, Owner);
                return true;
            } 
            return false;
            
        }
    }

    public override bool CheckCastAvailable()
    {
        Shaman target = Owner.ShamanTargetHelper.GetTarget(TargetData); 
        if (!ReferenceEquals(target, null)) // any shaman in range?
        {
            // Check if caster has lower hp (ratio) than lowest hp target
            float casterCurrentHpRatio = Owner.Damageable.CurrentHp / Owner.Stats[StatType.MaxHp].Value;
            float targetCurrentHpRatio = target.Damageable.CurrentHp / target.Stats[StatType.MaxHp].Value;
            if (casterCurrentHpRatio < targetCurrentHpRatio)
            {
                target = Owner as Shaman;
            }
            
            if (HasAbilityBehavior(AbilityBehavior.HealOnFullHealth)) return true;

            if(target.Damageable.CurrentHp == target.Damageable.MaxHp)
            {
                return false;
            }

            return true;
        }
        else // no other injured shamans
        {
            if (HasAbilityBehavior(AbilityBehavior.HealOnFullHealth)) return true;
            if(Owner.Damageable.CurrentHp < Owner.Stats[StatType.MaxHp].Value) // check if caster is injured
            {
                return true;
            }
            return false;
        }
    }
    
    
    protected virtual void HealTarget(Shaman target, BaseUnit caster)
    {
        target.Damageable.Heal((int)GetAbilityStatValue(AbilityStatType.Heal));
        target.ShamanVisualHandler.HealEffect.Play();
    }
}