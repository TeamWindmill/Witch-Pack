using UnityEngine;

public class GroundCollider : MonoBehaviour
{
    private BaseUnit _unit;

    public BaseUnit Unit => _unit;

    public void Init(BaseUnit unit)
    {
        _unit = unit;
    }
}
