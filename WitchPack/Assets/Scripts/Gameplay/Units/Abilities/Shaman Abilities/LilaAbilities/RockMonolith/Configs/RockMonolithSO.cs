
using UnityEngine;

public class RockMonolithSO : OffensiveAbilitySO
{
    [SerializeField] private float _duration;
    [SerializeField] private float _movementSpeedSlow;
    [SerializeField] private float _armorGain;
    [SerializeField] private float _tauntRadius;

    public float Duration => _duration;
    public float MovementSpeedSlow => _movementSpeedSlow;
    public float ArmorGain => _armorGain;
    public float TauntRadius => _tauntRadius;
}