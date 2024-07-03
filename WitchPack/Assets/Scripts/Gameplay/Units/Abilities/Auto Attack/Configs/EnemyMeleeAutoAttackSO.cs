using UnityEngine;

[CreateAssetMenu(fileName = "EnemyMeleeAutoAttack", menuName = "Ability/AutoAttack/MeleeAutoAttack")]
public class EnemyMeleeAutoAttackSO : OffensiveAbilitySO
{
    [SerializeField] private float _meleeRange;

    public float MeleeRange => _meleeRange;
}