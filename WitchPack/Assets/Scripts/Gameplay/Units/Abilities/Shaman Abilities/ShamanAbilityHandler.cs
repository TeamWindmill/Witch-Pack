using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Units.Abilities.AbilitySystem;
using Gameplay.Units.Abilities.AbilitySystem.BaseAbilities;
using Gameplay.Units.Abilities.AbilitySystem.BaseAbilities.Passives;
using Gameplay.Units.Abilities.AbilitySystem.BaseConfigs;
using Gameplay.Units.Abilities.AbilitySystem.Casting;
using Gameplay.Units;
using UI.GameUI.HeroSelectionUI.AbilityUI;
using UI.MapUI.MetaUpgrades.UpgradePanel.Configs;

namespace Gameplay.Units.Abilities.Shaman_Abilities
{
    public class ShamanAbilityHandler : AbilityHandler
    {
        public event Action<Ability> OnAbilityLearned;
        public List<Ability> KnownAbilities { get; } = new();
        public List<Ability> RootAbilities { get; } = new();

        private readonly Shaman _shamanOwner;
    
        public ShamanAbilityHandler(BaseUnit owner) : base(owner)
        {
            _shamanOwner = owner as Shaman;
            AutoAttackCaster.OnAttack += _shamanOwner.AttackSFX;
        
        }

        #region Abilities

        public void IntializeAbilities()
        {
            foreach (var abilitySo in _shamanOwner.ShamanConfig.RootAbilities)
            {
                var ability = AbilityFactory.CreateAbility(abilitySo, _shamanOwner);
                RootAbilities.Add(ability);
                foreach (var upgrade in ability.GetUpgrades())
                {
                    upgrade.ChangeUpgradeState(UpgradeState.Locked);
                }

                ability.ChangeUpgradeState(UpgradeState.Open);
            }

            foreach (var abilitySo in _shamanOwner.ShamanConfig.KnownAbilities)
            {
                foreach (var ability in Enumerable.Where(RootAbilities, rootAbility => rootAbility.BaseConfig == abilitySo))
                {
                    ability.UpgradeAbility();
                    KnownAbilities.Add(ability);
                    if (ability is PassiveAbility passive)
                    {
                        passive.SubscribePassive();
                    }
                    else if (ability is CastingAbility castingAbility)
                    {
                        var abilityCaster = new AbilityCaster(Owner, castingAbility);
                        CastingHandlers.Add(abilityCaster);
                        OnCasterAdded?.Invoke(abilityCaster);
                    }
                }
            }

            Owner.AutoCaster.Init(Owner, true);
        }

        public void LearnAbility(Ability ability)
        {
            KnownAbilities.Add(ability);
            OnAbilityLearned?.Invoke(ability);
            if (ability is PassiveAbility passive)
            {
                passive.SubscribePassive();
            }
            else if (ability is CastingAbility castingAbility)
            {
                //ability.BaseConfig.OnSetCaster(this);
                var caster = new AbilityCaster(Owner, castingAbility);
                CastingHandlers.Add(caster);
                Owner.AutoCaster.ReplaceAbility(caster);
                OnCasterAdded?.Invoke(caster);
            }
        }

        public void RemoveAbility(Ability ability)
        {
            if (ability is not PassiveAbility) //unsubscribe passives currently doesnt work
            {
                KnownAbilities.Remove(ability);
                CastingHandlers.Remove(GetCasterFromAbility(ability));
            }
        }

        public void UpgradeAbility(Ability ability, Ability upgrade)
        {
            RemoveAbility(ability);
            LearnAbility(upgrade);
        }


        public AbilityCaster GetCasterFromAbility(Ability givenAbility)
        {
            for (int i = 0; i < CastingHandlers.Count; i++)
            {
                if (ReferenceEquals(CastingHandlers[i].Ability, givenAbility))
                {
                    return CastingHandlers[i];
                }
            }

            return null;
        }

        public Ability GetActiveAbilityFromRoot(Ability rootAbility)
        {
            if (KnownAbilities.Contains(rootAbility)) return rootAbility;

            var upgrades = rootAbility.GetUpgrades();
            foreach (var upgrade in upgrades)
            {
                if (KnownAbilities.Contains(upgrade)) return upgrade;
            }

            return null;
        }


        public Ability GetAbilityFromConfig(AbilitySO config)
        {
            foreach (var ability in RootAbilities)
            {
                if (ability.BaseConfig == config) return ability;

                foreach (var upgrade in ability.GetUpgrades())
                {
                    if (upgrade.BaseConfig == config) return upgrade;
                }
            }

            return null;
        }

        #endregion
    
        #region MetaUpgrades

        public void AddMetaUpgrades(ShamanSaveData saveData)
        {
            AddAbilityMetaUpgrades(saveData.AbilityUpgrades);
            AddStatMetaUpgrades(saveData.StatUpgrades);
        }

        private void AddAbilityMetaUpgrades(List<AbilityUpgradeConfig> abilityUpgrades)
        {
            foreach (var abilityUpgrade in abilityUpgrades)
            {
                foreach (var abilitySO in abilityUpgrade.AbilitiesToUpgrade)
                {
                    var ability = GetAbilityFromConfig(abilitySO);
                    ability.AddStatUpgrade(abilityUpgrade);
                    if (abilityUpgrade.AbilitiesBehaviors.Length > 0) ability.AddAbilityBehavior(abilityUpgrade);
                }
            }
        }

        private void AddStatMetaUpgrades(List<StatMetaUpgradeConfig> statUpgrades)
        {
            foreach (var statUpgrade in statUpgrades)
            {
                if (statUpgrade.AbilitiesBehaviors.Length > 0)
                {
                    foreach (var abilitySO in statUpgrade.AbilitiesToUpgrade)
                    {
                        var ability = GetAbilityFromConfig(abilitySO);
                        ability.AddAbilityBehavior(statUpgrade);
                    }
                }

                if (statUpgrade.UpgradeAbility)
                {
                    foreach (var abilitySO in statUpgrade.AbilitiesToUpgrade)
                    {
                        var ability = GetAbilityFromConfig(abilitySO);
                        ability.AddStatUpgrade(statUpgrade);
                    }
                }
                else if (statUpgrade.UpgradePassiveAbility)
                {
                    foreach (var abilitySO in statUpgrade.AbilitiesToUpgrade)
                    {
                        var ability = GetAbilityFromConfig(abilitySO);
                        if (ability is not StatPassive statPassive) return;
                        statPassive.AddPassiveStatUpgrade(statUpgrade);
                    }
                }
                else
                {
                    foreach (var statConfig in statUpgrade.Stats)
                    {
                        _shamanOwner.Stats.AddValueToStat(statConfig.StatType, statConfig.Factor, statConfig.StatValue);
                    }
                }
            }
        }

        #endregion
    }
}