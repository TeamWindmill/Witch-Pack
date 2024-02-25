using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class UnitEffectHandler : MonoBehaviour
{
    [SerializeField] private StatusEffectVisual[] _statusEffectVisuals;

    private Effectable _effectable;

    public void Init()
    {
        foreach (StatusEffectVisual effectVisual in _statusEffectVisuals)
        {
            effectVisual.SetOffAllVisualGameObjects();
        }
    }

    public void PlayEffect(Effectable effectable,StatusEffectType statusEffectType)
    {
        _effectable = effectable;
        foreach (StatusEffectVisual effectVisual in _statusEffectVisuals)
        {
            if (effectVisual.StatusEffectType == statusEffectType)
            {                
                effectVisual.GetGameObject().SetActive(true);                            
                return;
            }
        }
    }

    public void DisableEffect(StatusEffectType statusEffectType)
    {
        foreach (StatusEffectVisual effectVisual in _statusEffectVisuals)
        {
            if (effectVisual.StatusEffectType == statusEffectType)
            {
                effectVisual.SetOffAllVisualGameObjects();
                return;
            }
        }
    }

    public void DisableAllEffects()
    {
        foreach (StatusEffectVisual effectVisual in _statusEffectVisuals)
        {
            effectVisual.SetOffAllVisualGameObjects();
        }
    }
}

[Serializable]
public struct StatusEffectVisual
{
    public StatusEffectType StatusEffectType;
    [SerializeField] public List<GameObject> visualGameObjects;

    public GameObject GetGameObject()
    {
        int index = 0;
        if(visualGameObjects.Count > 1)
        {
            index = new System.Random().Next(0, visualGameObjects.Count); // Gets a random go from the list if there's more than one option
        }

        return visualGameObjects[index];
    }

    public void SetOffAllVisualGameObjects()
    {
        foreach (GameObject go in visualGameObjects)
        {
            go.SetActive(false);
        }
    }
}
