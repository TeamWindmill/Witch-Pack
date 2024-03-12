using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitEffectHandler : MonoBehaviour
{
    [SerializeField] private EffectVisual<StatusEffectType>[] _statusEffectVisuals;
    [SerializeField] private EffectVisual<CastingHandsEffectType>[] _castingHandsVisuals;

    

    public virtual void Init(BaseUnitConfig config)
    {
        DisableAllEffects();
    }

    public void PlayEffect<T>(T effectType) where T : Enum
    {
        switch (effectType)
        {
            case StatusEffectType statusEffectType:
                foreach (var effectVisual in _statusEffectVisuals)
                {
                    if (effectVisual.StatusEffectType == statusEffectType)
                    {                
                        effectVisual.GetGameObject().SetActive(true);                            
                        return;
                    }
                }
                break;
            case CastingHandsEffectType castingHandsEffectType:
                foreach (var effectVisual in _castingHandsVisuals)
                {
                    if (effectVisual.StatusEffectType == castingHandsEffectType)
                    {                
                        effectVisual.GetGameObject().SetActive(true);                            
                        return;
                    }
                }
                break;
        }
    }

    public void DisableEffect<T>(T effectType) where T : Enum
    {
        switch (effectType)
        {
            case StatusEffectType statusEffectType:
                foreach (var effectVisual in _statusEffectVisuals)
                {
                    if (effectVisual.StatusEffectType == statusEffectType)
                    {
                        effectVisual.SetOffAllVisualGameObjects();
                        return;
                    }
                }
                break;
            case CastingHandsEffectType castingHandsEffectType:
                foreach (var effectVisual in _castingHandsVisuals)
                {
                    if (effectVisual.StatusEffectType == castingHandsEffectType)
                    {
                        effectVisual.SetOffAllVisualGameObjects();
                        return;
                    }
                }
                break;
        }
    }

    public void DisableAllEffects()
    {
        foreach (var effectVisual in _statusEffectVisuals)
        {
            effectVisual.SetOffAllVisualGameObjects();
        }
        foreach (var effectVisual in _castingHandsVisuals)
        {
            effectVisual.SetOffAllVisualGameObjects();
        }
    }
}

[Serializable]
public struct EffectVisual<T> where T : Enum
{
    public T StatusEffectType;
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

public enum CastingHandsEffectType
{
    Orange,
    Yellow,
    Blue,
    Red,
    Turquoise,
    Green,
    GreenYellow,
}
