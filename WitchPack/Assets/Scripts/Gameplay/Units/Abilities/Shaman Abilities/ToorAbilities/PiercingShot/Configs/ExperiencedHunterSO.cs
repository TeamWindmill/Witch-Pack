using UnityEngine;

[CreateAssetMenu(fileName = "ability", menuName = "Ability/Toor/PiercingShot/ExperiencedHunter")]

public class ExperiencedHunterSO : PiercingShotSO
{
    [SerializeField] private int extraPenPerKill;
    [SerializeField] private int numberOfKillsRequiredToIncreasePierce;
    [SerializeField] private Color textPopupColor;

    public int ExtraPenPerKill => extraPenPerKill;
    public int NumberOfKillsRequiredToIncreasePierce => numberOfKillsRequiredToIncreasePierce;
    public Color TextPopupColor => textPopupColor;
}
