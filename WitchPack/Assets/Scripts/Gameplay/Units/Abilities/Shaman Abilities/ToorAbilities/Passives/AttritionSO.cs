using UnityEngine;

[CreateAssetMenu(fileName = "Attrition", menuName = "Ability/Toor/Attrition")]
public class AttritionSO : PassiveSO
{
    [SerializeField] private EventToCount eventToCount;
    [SerializeField] private int maxStacks;
    [SerializeField, Range(1, 100)] private float damageIncreasePerShot;

    public EventToCount EventToCount => eventToCount;
    public int MaxStacks => maxStacks;
    public float DamageIncreasePerShot => damageIncreasePerShot;
}

public enum EventToCount
{
    OnHit,
    OnKill
}
