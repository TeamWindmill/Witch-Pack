using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Lila/OrbitalStones/OrbitalStones",fileName = "OrbitalStones")]
public class OrbitalStonesSO : OffensiveAbilitySO
{
    [SerializeField] private float _angularSpeed = 200f;
    [SerializeField] private float _radius = 1.5f;
    [SerializeField] private float _ellipseScale = 1.3f;
    [SerializeField] private int _stoneAmount;
    [SerializeField] private float _duration;

    public float AngularSpeed => _angularSpeed;
    public float Radius => _radius;
    public float EllipseScale => _ellipseScale;
    public int StoneAmount => _stoneAmount;
    public float Duration => _duration;
}