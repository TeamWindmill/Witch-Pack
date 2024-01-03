using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Targeter<T> : MonoBehaviour where T : Component
{
    [SerializeField] private List<T> availableTargets = new List<T>();
    [SerializeField] private CircleCollider2D collider;

    public void AddRadius(Stat stat, float value)
    {
        if (stat == Stat.BaseRange)
        {
            collider.radius += value;
        }
    }
    public void SetRadius(float value)
    {
        collider.radius = value;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        T possibleTarget = collision.GetComponent<T>();
        if (!ReferenceEquals(possibleTarget, null) && !availableTargets.Contains(possibleTarget))
        {
            availableTargets.Add(possibleTarget);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        T possibleTarget = collision.GetComponent<T>();
        if (!ReferenceEquals(possibleTarget, null))
        {
            availableTargets.Remove(possibleTarget);
        }

    }

    public T GetClosestTarget()
    {
        if (availableTargets.Count <= 0)
        {
            return null;
        }
        T closest = availableTargets[0];
        float dist = Vector2.Distance(transform.position, closest.transform.position);
        foreach (var item in availableTargets)
        {
            if (Vector2.Distance(transform.position, item.transform.position) <= dist)
            {
                closest = item;
            }
        }
        return closest;
    }

}
