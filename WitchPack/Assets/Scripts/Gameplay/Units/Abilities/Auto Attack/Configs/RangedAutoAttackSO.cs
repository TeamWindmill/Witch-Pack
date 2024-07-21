using UnityEngine;

public abstract class RangedAutoAttackSO : OffensiveAbilitySO
{
    [SerializeField] private float _speed;

    public float Speed => _speed;
}