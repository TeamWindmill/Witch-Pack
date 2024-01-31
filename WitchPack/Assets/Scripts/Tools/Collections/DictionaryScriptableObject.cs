using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dictionary Object", menuName = "DictionarySO")]
public class DictionaryScriptableObject<TKey,TValue> : ScriptableObject
{
    [SerializeField] private List<TKey> _keys = new List<TKey>();
    [SerializeField] private List<TValue> _values = new List<TValue>();
}
