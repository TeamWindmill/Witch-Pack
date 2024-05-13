using UnityEngine;

[CreateAssetMenu(fileName = "ability", menuName = "Ability/Heal/BlessingOfSwiftness")]
public class BlessingOfSwiftnessSO : HealSO
{
    [SerializeField] private StatusEffectConfig attackSpeedBoost;
    public StatusEffectConfig AttackSpeedBoost => attackSpeedBoost;
}
