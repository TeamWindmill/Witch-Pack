using System;
using System.Collections.Generic;
using Gameplay.Units.Stats;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Targeter
{
    public class Targeter<T> : MonoBehaviour where T : Component
    {
        [SerializeField] protected List<T> availableTargets = new List<T>();
        public Action<T> OnTargetAdded;
        public Action<T> OnTargetLost;

        public bool HasTarget => availableTargets.Count > 0;

        public List<T> AvailableTargets
        {
            get => availableTargets;
        }

        public void AddRadius(float value)
        {
            transform.parent.localScale = new Vector3(value, value, value);
        }

        public void SetRadius(float value)
        {
            transform.parent.localScale = new Vector3(value, value, value);
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            T possibleTarget = collision.GetComponent<T>();
            if (!ReferenceEquals(possibleTarget, null) && !availableTargets.Contains(possibleTarget))
            {
                availableTargets.Add(possibleTarget);
                OnTargetAdded?.Invoke(possibleTarget);
            }
        }

        protected virtual void OnTriggerExit2D(Collider2D collision)
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

    [Serializable]
    public struct TargetData
    {
        public TargetPriority Priority;
        public TargetModifier Modifier;

        [ShowIf(nameof(Priority), TargetPriority.Stat)]
        public StatType StatType;

        public bool AvoidCharmedTargets;
    }
}