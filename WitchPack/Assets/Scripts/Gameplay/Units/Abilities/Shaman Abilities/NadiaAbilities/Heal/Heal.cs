public class Heal : CastingAbility
{
    private HealSO _config;
    public Heal(HealSO config, BaseUnit owner) : base(config, owner)
    {
        _config = config;
    }

    public override bool CastAbility()
    {
        Shaman target = Owner.ShamanTargetHelper.GetTarget(_config.TargetData); 
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

            if(target.Damageable.CurrentHp == target.Damageable.MaxHp)
            {
                return false;
            }

            HealTarget(target, Owner);
            return true;
        }
        else // no other injured shamans
        {
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
        Shaman target = Owner.ShamanTargetHelper.GetTarget(_config.TargetData); 
        if (!ReferenceEquals(target, null)) // any shaman in range?
        {
            // Check if caster has lower hp (ratio) than lowest hp target
            float casterCurrentHpRatio = Owner.Damageable.CurrentHp / Owner.Stats[StatType.MaxHp].Value;
            float targetCurrentHpRatio = target.Damageable.CurrentHp / target.Stats[StatType.MaxHp].Value;
            if (casterCurrentHpRatio < targetCurrentHpRatio)
            {
                target = Owner as Shaman;
            }

            if(target.Damageable.CurrentHp == target.Damageable.MaxHp)
            {
                return false;
            }

            return true;
        }
        else // no other injured shamans
        {
            if(Owner.Damageable.CurrentHp < Owner.Stats[StatType.MaxHp].Value) // check if caster is injured
            {
                return true;
            }
            return false;
        }
    }
    
    
    protected virtual void HealTarget(Shaman target, BaseUnit caster)
    {
        target.Damageable.Heal(_config.HealAmount);
        target.ShamanVisualHandler.HealEffect.Play();
    }
}