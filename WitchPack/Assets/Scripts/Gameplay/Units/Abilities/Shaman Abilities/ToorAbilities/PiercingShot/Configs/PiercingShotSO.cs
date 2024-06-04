using UnityEngine;

[CreateAssetMenu(fileName = "ability", menuName = "Ability/PiercingShot")]

public class PiercingShotSO : OffensiveAbilitySO
{
    [SerializeField] private int penetration;
    [SerializeField] private int speed;
    [SerializeField] private int lifeTime;
    public int Penetration => penetration;
    public int Speed => speed;
    public int LifeTime => lifeTime;
}
