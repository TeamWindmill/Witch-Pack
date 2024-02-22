using UnityEngine;

[CreateAssetMenu(fileName = "StatPassive", menuName = "Ability/Passive/Stat")]
public class StatPassive : Passive
{

    [SerializeField] protected StatValue[] statIncreases;

    public override void SubscribePassive(BaseUnit owner)
    {
        foreach (StatValue increase in statIncreases)
        {
            owner.Stats.AddValueToStat(increase.StatType, increase.Value);
        }
        //HeroSelectionUI.Instance.Show((Shaman)owner); // To show the updated stats in the UI
    }

}

[System.Serializable]
public struct StatValue
{
    public StatType StatType;
    public float Value;
}

