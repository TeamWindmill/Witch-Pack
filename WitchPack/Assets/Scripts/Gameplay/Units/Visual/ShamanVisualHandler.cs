
public class ShamanVisualHandler : UnitVisualHandler
{
    protected override void OnUnitDeath()
    {
        _baseUnit.gameObject.SetActive(false);
    }
}