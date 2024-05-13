using UnityEngine;

[CreateAssetMenu(fileName = "Charm", menuName = "Ability/Charm")]
public class CharmSO : CastingAbilitySO
{
    [SerializeField] private Charmed _charmedState;
    public Charmed CharmedState => _charmedState;
}