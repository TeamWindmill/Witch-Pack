using UnityEngine;

[CreateAssetMenu(fileName = "ability", menuName = "Ability/PiercingShot")]

public class PiercingShotSO : OffensiveAbilitySO
{
    [SerializeField] private PiercingShotType type;
    [SerializeField] private int penetration;
    [SerializeField] private int speed;
    [SerializeField] private int lifeTime;
    public int Penetration => penetration;
    public int Speed => speed;
    public int LifeTime => lifeTime;
    public PiercingShotType Type => type;
}

public enum PiercingShotType
{
    PiercingShot,
    QuickShot,
    Marksman,
    ExperiencedHunter
}
