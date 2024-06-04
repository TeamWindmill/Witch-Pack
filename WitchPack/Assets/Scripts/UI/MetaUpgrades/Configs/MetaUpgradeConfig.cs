using UnityEngine;

public class MetaUpgradeConfig : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private float _statValue;
    [SerializeField] private Factor _factor;

    public float StatValue => _statValue;
    public string Name => _name;
    public Factor Factor => _factor;
}

public enum Factor
{
    Add,
    Subtract,
    Multiply,
    Divide
}