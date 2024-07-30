using Gameplay.Units.Abilities.AbilitySystem.AbilityStats;
using Gameplay.Units.Abilities.AbilitySystem.BaseAbilities;
using Gameplay.Units.Abilities.Shaman_Abilities.ToorAbilities.PiercingShot.Configs;
using Gameplay.Units.Damage_System;
using Gameplay.Units.Stats;
using Managers;
using Sound;

namespace Gameplay.Units.Abilities.Shaman_Abilities.ToorAbilities.PiercingShot
{
    public class ExperiencedHunter : PiercingShot
    {
        private ExperiencedHunterSO _config;
    
        public ExperiencedHunter(ExperiencedHunterSO config, BaseUnit owner) : base(config, owner)
        {
            _config = config;
            AddAbilityStatUpgrade(AbilityStatType.ExtraPenetrationPerKill,Factor.Add,_config.ExtraPenPerKill);
            AddAbilityStatUpgrade(AbilityStatType.KillToIncreasePenetration,Factor.Add,_config.NumberOfKillsRequiredToIncreasePierce);
            if(owner is null) return;
            ExperiencedHunterCounter eventCounter = new ExperiencedHunterCounter(owner, this, ref owner.DamageDealer.OnKill);
            eventCounter.OnCountIncrement += IncreasePen;
        }
    
        private void IncreasePen(AbilityEventCounter counter, Damageable target, DamageDealer dealer, DamageHandler dmg, Ability ability)
        {
            ProjectilePenetration += (int)GetAbilityStatValue(AbilityStatType.ExtraPenetrationPerKill);
            // Play Sound
            SoundManager.PlayAudioClip(SoundEffectType.ExperiencedHunterLevelUp);
            // Popup
            LevelManager.Instance.PopupsManager.SpawnGeneralPopup("PIERCE UP+", _config.TextPopupColor, dealer.Owner.transform.position, yOffset: true);
        }
    }
}