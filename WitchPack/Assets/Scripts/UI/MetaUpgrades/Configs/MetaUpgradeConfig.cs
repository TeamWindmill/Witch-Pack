using System;
using UnityEngine;

[Serializable]
public class MetaUpgradeConfig 
{
    [SerializeField] private string _valueName;
    [SerializeField] private string _name;

    public string Name => _name;
    public string ValueName => _valueName;
}

public enum Factor
{
    Add,
    Subtract,
    Multiply,
    Divide,
    None
}