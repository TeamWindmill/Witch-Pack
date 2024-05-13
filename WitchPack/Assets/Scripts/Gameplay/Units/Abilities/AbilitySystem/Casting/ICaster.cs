public interface ICaster
{
    public CastingAbilitySO AbilitySo { get; }
    public float LastCast { get;}
    public bool CastAbility();
    public float GetCooldown();
    public bool CheckCastAvailable();
    public bool ContainsUpgrade(ICaster caster);
}