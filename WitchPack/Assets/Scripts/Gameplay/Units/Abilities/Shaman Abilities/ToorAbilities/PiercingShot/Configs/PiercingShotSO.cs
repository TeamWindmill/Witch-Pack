using UnityEngine;

[CreateAssetMenu(fileName = "ability", menuName = "Ability/PiercingShot")]

public class PiercingShotSO : OffensiveAbilitySO
{
    [SerializeField] private int penetration;
    public int Penetration { get => penetration; }

}
