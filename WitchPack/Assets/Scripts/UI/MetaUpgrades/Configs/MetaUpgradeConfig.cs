using System;
using UnityEngine;

[Serializable]
public class MetaUpgradeConfig 
{
    [SerializeField] private string _name;
    [SerializeField] private string _valueName;

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