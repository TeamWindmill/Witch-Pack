using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Units.Visual
{
    public class EffectHandler<T> : MonoBehaviour where T : Enum
    {
        [SerializeField] private EffectVisual<T>[] _effectsVisuals;

        public virtual void PlayEffect(T effectType)
        {
            foreach (var effectVisual in _effectsVisuals)
            {
                if (effectVisual.StatusEffectType.Equals(effectType))
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

        public virtual void DisableEffect(T effectType)
        {
            foreach (var effectVisual in _effectsVisuals)
            {
                if (effectVisual.StatusEffectType.Equals(effectType))
                {
                    effectVisual.SetOffAllVisualGameObjects();
                    return;
                }
            }
        }

        public virtual void DisableAllEffects()
        {
            foreach (var effectVisual in _effectsVisuals)
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
            if (visualGameObjects.Count > 1)
            {
                index = new System.Random().Next(0,
                    visualGameObjects.Count); // Gets a random go from the list if there's more than one option
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
}