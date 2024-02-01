using UnityEngine;

[CreateAssetMenu(fileName = "StatPassive", menuName = "Ability/Passive/Stat")]
public class StatPassive : Passive
{

    [SerializeField] private StatValue[] statIncreases;

    public override void SubscribePassive(BaseUnit owner)
    {
        foreach (StatValue increase in statIncreases)
        {
            owner.Stats.AddValueToStat(increase.Stat, increase.Value);
        }
    }

}

[System.Serializable]
public struct StatValue
{
    public StatType Stat;
    public float Value;
}

