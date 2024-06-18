using UnityEngine;

[CreateAssetMenu(fileName = "ability", menuName = "Ability/Nadia/RootingVines/HealingWeeds")]
public class HealingWeedsSO : OffensiveAbilitySO
{
    [SerializeField] private float aoeScale = 1;
    [SerializeField] private float lastingTime;
    [SerializeField] private StatusEffectConfig speedBoost;
    [SerializeField] private StatusEffectConfig regenBoost;
    [SerializeField] private StatusEffectConfig root;
    public float AoeScale => aoeScale;
    public float LastingTime => lastingTime;

    public StatusEffectConfig SpeedBoost => speedBoost;

    public StatusEffectConfig RegenBoost => regenBoost;

    public StatusEffectConfig Root => root;
}
