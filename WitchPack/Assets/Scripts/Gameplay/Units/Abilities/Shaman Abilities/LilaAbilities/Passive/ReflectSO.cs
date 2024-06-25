using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Lila/Passive/Reflect",fileName = "Reflect")]
public class ReflectSO : PassiveSO
{
    [SerializeField] private float _reflectedDamagePercent;

    public float ReflectedDamagePercent => _reflectedDamagePercent;
}