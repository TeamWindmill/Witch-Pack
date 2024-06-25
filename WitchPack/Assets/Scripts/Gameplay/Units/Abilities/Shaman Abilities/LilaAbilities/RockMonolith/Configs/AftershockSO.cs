using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Lila/RockMonolith/Aftershock",fileName = "Aftershock")]
public class AftershockSO : RockMonolithSO
{
    [BoxGroup("Aftershock"), SerializeField] private int _damageReductionPerBounceInPercent;

    public int DamageReductionPerBounceInPercent => _damageReductionPerBounceInPercent;
}