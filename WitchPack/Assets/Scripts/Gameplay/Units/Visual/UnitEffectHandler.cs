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
    
    public virtual void PlayEffect(Effectable effectable, Affector affector, StatusEffect statusEffect)
    {
        foreach (var effectVisual in _statusEffectVisuals)
        {
            if (effectVisual.StatusEffectType == statusEffect.StatusEffectType)
            {
                if (effectVisual.PlayAllEffects)
                {
                    foreach (var go in effectVisual.visualGameObjects)
                    {
                        go.SetActive(true);
                    }
                }
                else
                {
                    effectVisual.GetGameObject().SetActive(true);                            
                }
                return;
            }
        }
        
    }
    public virtual void PlayEffect(CastingHandsEffectType effectType)
    {
        foreach (var effectVisual in _castingHandsVisuals)
        {
            if (effectVisual.StatusEffectType == effectType)
            {                
                effectVisual.GetGameObject().SetActive(true);                            
                return;
            }
        }
        
    }
    public virtual void DisableEffect(StatusEffect statusEffect)
    {
        foreach (var effectVisual in _statusEffectVisuals)
        {
            if (effectVisual.StatusEffectType == statusEffect.StatusEffectType)
            {
                effectVisual.SetOffAllVisualGameObjects();
                return;
            }
        }
    }
    public virtual void DisableEffect(CastingHandsEffectType effectType)
    {
        foreach (var effectVisual in _castingHandsVisuals)
        {
            if (effectVisual.StatusEffectType == effectType)
            {
                effectVisual.SetOffAllVisualGameObjects();
                return;
            }
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
    [SerializeField] public bool PlayAllEffects;
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
    YellowWhite,
    Blue,
    Red,
    Turquoise,
    Green,
    GreenYellow,
}
