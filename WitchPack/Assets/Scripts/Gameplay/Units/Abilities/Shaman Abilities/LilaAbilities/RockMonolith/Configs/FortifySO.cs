using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Lila/RockMonolith/Fortify",fileName = "Fortify")]
public class FortifySO : RockMonolithSO
{
    [BoxGroup("Fortify"),SerializeField] private int _permanentArmorOnExplosion;
    public int PermanentArmorOnExplosion => _permanentArmorOnExplosion;
}