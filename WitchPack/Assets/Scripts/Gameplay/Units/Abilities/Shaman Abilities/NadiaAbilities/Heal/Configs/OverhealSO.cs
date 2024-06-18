using UnityEngine;

[CreateAssetMenu(fileName = "ability", menuName = "Ability/Nadia/Heal/Overheal")]
public class OverhealSO : HealSO
{
    [SerializeField] private int permanentMaxHealthBonus;
    public int PermanentMaxHealthBonus => permanentMaxHealthBonus;

}
