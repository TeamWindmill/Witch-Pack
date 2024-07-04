public class ExperiencedHunter : PiercingShot
{
    private ExperiencedHunterSO _config;
    
    public ExperiencedHunter(ExperiencedHunterSO config, BaseUnit owner) : base(config, owner)
    {
        _config = config;
        abilityStats.Add(new AbilityStat(AbilityStatType.KillToIncreasePenetration,_config.NumberOfKillsRequiredToIncreasePierce));
        abilityStats.Add(new AbilityStat(AbilityStatType.ExtraPenetrationPerKill,config.ExtraPenPerKill));
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