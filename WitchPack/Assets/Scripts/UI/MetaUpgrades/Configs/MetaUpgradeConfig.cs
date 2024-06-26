using System;
using UnityEngine;

[Serializable]
public class MetaUpgradeConfig 
{
    [SerializeField] private string _valueName;
    [SerializeField] private string _name;
    [SerializeField] private bool _notWorking;

    public string Name => _name;
    public string ValueName => _valueName;

    public bool NotWorking => _notWorking;
}
