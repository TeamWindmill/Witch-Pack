using System.Collections.Generic;
using Gameplay.Units.Abilities.AbilitySystem.BaseConfigs.Passives;

namespace Gameplay.Units.Abilities.AbilitySystem.BaseAbilities.Passives
{
    public class AffectedByUnitsStatPassive : StatPassive
    {
        protected AffectedByUnitsStatPassiveSO _config;
        protected List<BaseUnit> AffectingUnits = new();
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

        private void ChangeStatByEnemy(Enemy.Enemy enemy, bool addition)
        {
            if (_config.AffectedByEnemiesWithStatusEffect)
            {
                if (enemy.Effectable.ContainsStatusEffect(_config.EnemyStatusEffect))
                {
                    ChangeStat(enemy,addition);
                }
            }
            else
            {
                ChangeStat(enemy,addition);
            }
        }


        protected void ChangeStatByShaman(Shaman shaman, bool addition)
        {
            if (_config.AffectedByShamansWithStatusEffect)
            {
                if (shaman.Effectable.ContainsStatusEffect(_config.ShamanStatusEffect))
                {
                    ChangeStat(shaman,addition);
                }
            }
            else
            {
                ChangeStat(shaman,addition);
            }
        }

        protected void ChangeStat(BaseUnit affectingUnit, bool addition)
        {
            foreach (var stat in PassiveAbilityStats)
            {
                if (addition)
                {
                    AffectingUnits.Add(affectingUnit);
                    Owner.Stats[stat.StatType].AddModifier(stat.Value);
                }
                else
                {
                    AffectingUnits.Remove(affectingUnit);
                    Owner.Stats[stat.StatType].RemoveModifier(stat.Value);
                }
            }
        }

        protected void RefreshAffectingUnits()
        {
            BaseUnit[] affectingUnits = AffectingUnits.ToArray();
            foreach (var unit in affectingUnits)
            {
                ChangeStat(unit,false);
            }
        
            var shamanTargets = Owner.ShamanTargeter.AvailableTargets;
            foreach (var shaman in shamanTargets)
            {
                ChangeStatByShaman(shaman,true);
            }
        
            var enemyTargets = Owner.EnemyTargeter.AvailableTargets;
            foreach (var enemy in enemyTargets)
            {
                ChangeStatByEnemy(enemy,true);
            }
        }
    }
}