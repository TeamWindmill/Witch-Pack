using UnityEngine;

[CreateAssetMenu(fileName = "ConditionalStatPassive", menuName = "Ability/Passive/ConditionalStat")]
public class ConditionalStatPassiveSO : StatPassiveSO
{
    [SerializeField] private StatusEffectConfig _conditionalStatusEffect;
    public StatusEffectConfig ConditionalStatusEffect => _conditionalStatusEffect;
}
