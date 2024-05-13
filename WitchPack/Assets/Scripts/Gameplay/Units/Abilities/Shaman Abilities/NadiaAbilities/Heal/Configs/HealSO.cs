using UnityEngine;

[CreateAssetMenu(fileName = "ability", menuName = "Ability/Heal/Heal")]
public class HealSO : CastingAbilitySO
{
    [SerializeField] protected int healAmount;
    public int HealAmount => healAmount;
}
