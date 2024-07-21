using UnityEngine;

[CreateAssetMenu(fileName = "ability", menuName = "Ability/Nadia/RootingVines/HealingWeeds")]
public class HealingWeedsSO : OffensiveAbilitySO
{
    [SerializeField] private float aoeScale = 1;
    [SerializeField] private float lastingTime;
    [SerializeField] private StatusEffectConfig[] healStatusEffects;
    public float AoeScale => aoeScale;
    public float LastingTime => lastingTime;

    public StatusEffectConfig[] HealStatusEffects => healStatusEffects;
}
