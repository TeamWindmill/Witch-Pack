using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogSequence",menuName = "Dialog/DialogSequence")]
public class DialogSequence : ScriptableObject
{
    [SerializeField] private string _title;
    [SerializeField] private DialogBoxConfig[] _sequence;

    public DialogBoxConfig[] Sequence => _sequence;

    public DialogBoxConfig this[int index]
    {
        get => _sequence[index];
    }
}
