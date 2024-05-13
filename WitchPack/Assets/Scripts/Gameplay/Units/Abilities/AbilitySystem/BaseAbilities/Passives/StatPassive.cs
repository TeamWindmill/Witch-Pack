public class StatPassive : PassiveAbility
{
    private StatPassiveSO _config;
    public StatPassive(StatPassiveSO config, BaseUnit owner) : base(config, owner)
    {
        _config = config;
    }

    public override void SubscribePassive()
    {
        foreach (StatValue increase in _config.StatIncreases)
        {
            Owner.Stats.AddValueToStat(increase.StatType, increase.Value);
        }
        //HeroSelectionUI.Instance.Show((Shaman)owner); // To show the updated stats in the UI
    }
}