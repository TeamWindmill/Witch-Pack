using System;
using UnityEngine;

[Serializable]
public class MetaUpgradeConfig 
{
    [SerializeField] private string _valueName;
    [SerializeField] private string _name;
    [SerializeField] private int _skillPointsCost;
    [SerializeField] private bool _notWorking;

    public string Name => _name;
    public string ValueName => _valueName;
    public int SkillPointsCost => _skillPointsCost;
    public bool NotWorking => _notWorking;
}
