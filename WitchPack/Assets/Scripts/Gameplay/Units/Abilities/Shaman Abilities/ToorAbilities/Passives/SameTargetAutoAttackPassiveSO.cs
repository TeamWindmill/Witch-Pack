using UnityEngine;

[CreateAssetMenu(fileName = "SameTargetAA", menuName = "Ability/Passive/SameTargetAA")]
public class SameTargetAutoAttackPassiveSO : PassiveSO
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
