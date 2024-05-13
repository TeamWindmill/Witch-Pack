public class ExperiencedHunter : PiercingShot
{
    private ExperiencedHunterSO _config;
    public ExperiencedHunter(ExperiencedHunterSO config, BaseUnit owner) : base(config, owner)
    {
        _config = config;
        ExperiencedHunterCounter eventCounter = new ExperiencedHunterCounter(owner, _config, ref owner.DamageDealer.OnKill, 
            _config.NumberOfKillsRequiredToIncreasePierce);
        eventCounter.OnCountIncrement += IncreasePen;
    }
    
    private void IncreasePen(AbilityEventCounter counter, Damageable target, DamageDealer dealer, DamageHandler dmg, AbilitySO abilitySo)
    {
        dealer.Owner.Stats.AddValueToStat(StatType.AbilityProjectilePenetration, _config.ExtraPenPerKill);
        // Play Sound
        SoundManager.Instance.PlayAudioClip(SoundEffectType.ExperiencedHunterLevelUp);
        // Popup
        LevelManager.Instance.PopupsManager.SpawnGeneralPopup("PIERCE UP+", _config.TextPopupColor, dealer.Owner.transform.position, yOffset: true);
    }
}