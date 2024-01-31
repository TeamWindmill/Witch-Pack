using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//https://youtu.be/OGnhLL4q_F8?si=zLv7HCJOjDBxjJIq&t=499 continue this
[Serializable]
public class SerializableDictionary<TKey, TValue>
{
    [SerializeField] private List<TKey> _keys = new List<TKey>();
    [SerializeField] private List<TValue> _values = new List<TValue>();

    private Dictionary<TKey, TValue> _dictionary = new Dictionary<TKey, TValue>();

    private bool modifyValues;
    public SerializableDictionary()
    {

    }
}
