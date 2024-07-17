using UnityEngine;

[CreateAssetMenu(fileName = "HealingLight", menuName = "Ability/Lila/Passive/HealingLight")]
public class HealingLightSO : StatPassiveSO
{
    [SerializeField] private float _healPercentage;
    [SerializeField] private float _healInterval;
    public float HealPercentage => _healPercentage;
    public float HealInterval => _healInterval;
}