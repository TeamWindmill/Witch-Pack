using UnityEngine;

[CreateAssetMenu(fileName = "ability", menuName = "Ability/PoisonIvy")]

public class PoisonIvySO : OffensiveAbilitySO
{
    [SerializeField] private float aoeScale = 1;
    [SerializeField] private float lastingTime;
    [SerializeField] private float poisonDuration;
    [SerializeField] private float poisonTickRate;
    [SerializeField] private int poisonDamage;
    [SerializeField] private Color poisonPopupColor;

    public float PoisonDuration { get => poisonDuration; }
    public float PoisonTickRate { get => poisonTickRate; }
    public int PoisonDamage { get => poisonDamage; }
    public Color PoisonPopupColor { get => poisonPopupColor; }
    public float AoeScale => aoeScale;
    public float LastingTime => lastingTime;
}
