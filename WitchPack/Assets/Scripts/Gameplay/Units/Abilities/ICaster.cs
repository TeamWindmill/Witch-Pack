public interface ICaster
{
    public BaseAbility Ability { get; }
    public float LastCast { get; set; }
    public bool CastAbility();
    public float GetCooldown();
}