public interface ICaster
{
    public BaseAbility Ability { get; }
    public float LastCast { get;}
    public bool CastAbility();
    public float GetCooldown();
}