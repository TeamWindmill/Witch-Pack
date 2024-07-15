using UnityEngine;

public class HealingLightSO : StatPassiveSO
{
    [SerializeField] private float _healPercentage;
    public float HealPercentage => _healPercentage;
}