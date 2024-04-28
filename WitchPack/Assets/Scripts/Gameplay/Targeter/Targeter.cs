using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Targeter<T> : MonoBehaviour where T : Component
{
    [SerializeField] private List<T> availableTargets = new List<T>();
    public event Action<T> OnTargetAdded;
    public event Action<T> OnTargetLost;

    public bool HasTarget => availableTargets.Count > 0;

    public List<T> AvailableTargets { get => availableTargets; }

    public void AddRadius(StatType statType, float value)
    {
        if (statType == StatType.BaseRange)
        {
            transform.parent.localScale = new Vector3(value, value, value);
        }
    }
    public void SetRadius(float value)
    {
        transform.parent.localScale = new Vector3(value, value, value);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        T possibleTarget = collision.GetComponent<T>();
        if (!ReferenceEquals(possibleTarget, null) && !availableTargets.Contains(possibleTarget))
        {
            availableTargets.Add(possibleTarget);
            OnTargetAdded?.Invoke(possibleTarget);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        T possibleTarget = collision.GetComponent<T>();
        if (!ReferenceEquals(possibleTarget, null))
        {
            availableTargets.Remove(possibleTarget);
            OnTargetLost?.Invoke(possibleTarget);
        }
    }
}

public enum TargetPriority
{
    Distance,
    DistnaceToCore,
    Stat,
    Threatened,
    CurrentHP,
    CurrentHPPrecentage,
    Random
}

public enum TargetModifier
{
    Most,
    Least
}

[System.Serializable]
public struct TargetData
{
    public TargetPriority Priority;
    public TargetModifier Modifier;
    [ShowIf(nameof(Priority), TargetPriority.Stat)]public StatType StatType;
    public bool AvoidCharmedTargets;
}
