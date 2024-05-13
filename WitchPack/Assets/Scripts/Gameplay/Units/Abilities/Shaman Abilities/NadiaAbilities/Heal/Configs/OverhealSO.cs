using UnityEngine;

[CreateAssetMenu(fileName = "ability", menuName = "Ability/Heal/Overheal")]
public class OverhealSO : HealSO
{
    [SerializeField] private int permanentMaxHealthBonus;
    public int PermanentMaxHealthBonus => permanentMaxHealthBonus;

}
