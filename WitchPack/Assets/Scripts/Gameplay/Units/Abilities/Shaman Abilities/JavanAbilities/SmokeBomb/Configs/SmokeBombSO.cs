using UnityEngine;

[CreateAssetMenu(fileName = "SmokeBomb", menuName = "Ability/SmokeBomb/SmokeBomb")]
public class SmokeBombSO : OffensiveAbilitySO
{
    public float Duration => _duration;

    [SerializeField] private float _duration;
}