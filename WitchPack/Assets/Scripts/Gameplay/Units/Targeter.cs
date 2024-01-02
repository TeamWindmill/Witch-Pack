using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Targeter<T> : MonoBehaviour where T: Component
{
    [SerializeField] private List<T> availableTargets = new List<T>();


    private void OnTriggerStay2D(Collider2D collision)
    {
        T possibleTarget = collision.GetComponent<T>();
        if (!ReferenceEquals(possibleTarget, null))
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
