using UnityEngine;


public class UNIT_HPBarConnector : MonoBehaviour
{
    [SerializeField] HP_Bar hP_Bar;

    [SerializeField] private GameObject _objWithUnit; //TEMP!
    BaseUnit _unit;

    public void Init(BaseUnit unit)
    {
        _unit = unit;
        hP_Bar.Init(_unit.Damageable.MaxHp);
    }

    public void Init(BaseUnit unit, UnitType entityType)
    {
        _unit = unit;
        hP_Bar.Init(_unit.Damageable.MaxHp, entityType);
    }

    public void SetBarToHealth(float value)
    {
        hP_Bar.SetBarValue(value);
    }
}