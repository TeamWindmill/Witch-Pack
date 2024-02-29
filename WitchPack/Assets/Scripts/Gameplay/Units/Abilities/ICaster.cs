public interface ICaster
{
    public OffensiveAbility Ability { get; }
    public float LastCast { get;}
    public bool CastAbility();
    public float GetCooldown();
    public bool CheckCastAvailable();
}