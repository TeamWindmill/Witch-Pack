using UnityEngine;

[CreateAssetMenu(fileName = "ability", menuName = "Ability/HealingWeeds")]
public class HealingWeedsSO : OffensiveAbilitySO
{
    [SerializeField] private float aoeScale = 1;
    [SerializeField] private float lastingTime;
    [SerializeField] private StatusEffectConfig speedBoost;
    [SerializeField] private StatusEffectConfig regenBoost;
    [SerializeField] private StatusEffectConfig root;
    public float AoeScale => aoeScale;
    public float LastingTime => lastingTime;
}
