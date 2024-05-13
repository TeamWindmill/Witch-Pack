using UnityEngine;

[CreateAssetMenu(fileName = "StatPassive", menuName = "Ability/Passive/Stat")]
public class StatPassiveSO : PassiveSO
{
    [SerializeField] protected StatValue[] statIncreases;
    public StatValue[] StatIncreases => statIncreases;
}

[System.Serializable]
public struct StatValue
{
    public StatType StatType;
    public float Value;
}

