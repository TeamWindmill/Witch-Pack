public class AffectedByUnitsStatPassive : StatPassive
{
    private AffectedByUnitsStatPassiveSO _config;
    public AffectedByUnitsStatPassive(AffectedByUnitsStatPassiveSO config, BaseUnit owner) : base(config, owner)
    {
        _config = config;
    }
    
    public override void SubscribePassive()
    {
        
        if (_config.AffectedByEnemies) Owner.EnemyTargeter.OnTargetAdded += enemy => ChangeStatByEnemy(enemy, true);
        if (_config.AffectedByEnemies) Owner.EnemyTargeter.OnTargetLost += enemy => ChangeStatByEnemy(enemy, false);
        if (_config.AffectedByShamans) Owner.ShamanTargeter.OnTargetAdded += shaman => ChangeStatByShaman(shaman, true);
        if (_config.AffectedByShamans) Owner.ShamanTargeter.OnTargetLost += shaman => ChangeStatByShaman(shaman, false);
    }

    private void ChangeStatByEnemy(Enemy enemy, bool addition)
    {
        if (_config.AffectedByEnemiesWithStatusEffect)
        {
            if (enemy.Effectable.ContainsStatusEffect(_config.EnemyStatusEffect))
            {
                ChangeStat(addition);
            }
        }
        else
        {
            ChangeStat(addition);
        }
    }


    private void ChangeStatByShaman(Shaman shaman, bool addition)
    {
        if (_config.AffectedByShamansWithStatusEffect)
        {
            if (shaman.Effectable.ContainsStatusEffect(_config.ShamanStatusEffect))
            {
                ChangeStat(addition);
            }
        }
        else
        {
            ChangeStat(addition);
        }
    }

    private void ChangeStat(bool addition)
    {
        foreach (var stat in PassiveAbilityStats)
        {
            if (addition) Owner.Stats.AddModifierToStat(stat.StatType, stat.Value);
            else
            {
                if(Owner.Stats.GetBaseStatValue(stat.StatType) >= Owner.Stats.GetStatValue(stat.StatType)) return;
                Owner.Stats.AddModifierToStat(stat.StatType, -stat.Value);
            }
        }
    }

}