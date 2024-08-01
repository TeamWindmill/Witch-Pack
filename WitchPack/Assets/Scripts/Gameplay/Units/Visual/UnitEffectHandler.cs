using Gameplay.Units.Visual;
using UnityEngine;

public class UnitEffectHandler : MonoBehaviour
{
    [SerializeField] private EffectVisual<StatusEffectVisual>[] _statusEffectVisuals;
    [SerializeField] private EffectVisual<CastingHandsEffectType>[] _castingHandsVisuals;

    public virtual void Init(BaseUnitConfig config)
    {
        DisableAllEffects();
    }
    
    public virtual void PlayEffect(Effectable effectable, Affector affector, StatusEffect statusEffect)
    {
        foreach (var effectVisual in _statusEffectVisuals)
        {
            if (effectVisual.StatusEffectType == statusEffect.StatusEffectVisual)
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
    public virtual void PlayEffect(StatusEffectVisual statusEffectVisual)
    {
        foreach (var effectVisual in _statusEffectVisuals)
        {
            if (effectVisual.StatusEffectType == statusEffectVisual)
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
            if (effectVisual.StatusEffectType == statusEffect.StatusEffectVisual)
            {
                effectVisual.SetOffAllVisualGameObjects();
                return;
            }
        }
    }
    public virtual void DisableEffect(StatusEffectVisual statusEffectVisual)
    {
        foreach (var effectVisual in _statusEffectVisuals)
        {
            if (effectVisual.StatusEffectType == statusEffectVisual)
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
