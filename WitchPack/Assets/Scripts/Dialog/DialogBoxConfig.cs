using System;
using UnityEngine;
using Sirenix.OdinInspector;

[Serializable]
public struct DialogBoxConfig 
{
    [SerializeField] private bool _isNarrationDialog;
    public bool IsCharacterDialog => !_isNarrationDialog;
    [SerializeField, ShowIf(nameof(IsCharacterDialog))] private BaseUnitConfig _speaker;
    [SerializeField,TextArea(5,10)] private string _dialogText;

    public BaseUnitConfig Speaker => _speaker;

    public string DialogText
    {
        get
        {
            if(_isNarrationDialog)
            {
                return "<i>" + _dialogText + "</i>"; // Make the text in italics if the text is a narration text
            }
            return _dialogText;
        }        
    }

}
