using System;
using UnityEngine;

[Serializable]
public struct DialogBoxConfig 
{
    [SerializeField] private BaseUnitConfig _speaker;
    [SerializeField,TextArea(5,10)] private string _dialogText;

    public BaseUnitConfig Speaker => _speaker;
    public string DialogText => _dialogText;
}
