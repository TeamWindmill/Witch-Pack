using UnityEngine;

[CreateAssetMenu(fileName = "ability", menuName = "Ability/RootingVines")]

public class RootingVinesSO : OffensiveAbilitySO
{
    [SerializeField] private float lastingTime;
    [SerializeField] private float _aoeScale = 1;

    public float LastingTime => lastingTime;
    public float AoeScale => _aoeScale;
}
