using UnityEngine;

namespace Gameplay.Units.Abilities.Shaman_Abilities.NadiaAbilities.Heal.Configs
{
    [CreateAssetMenu(fileName = "ability", menuName = "Ability/Nadia/Heal/Overheal")]
    public class OverhealSO : HealSO
    {
        [SerializeField] private int permanentMaxHealthBonus;
        public int PermanentMaxHealthBonus => permanentMaxHealthBonus;

    }
}
