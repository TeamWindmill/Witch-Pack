using System;
using System.Collections.Generic;
using Gameplay.Units.Stats;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UI.Popups
{
    [CreateAssetMenu(fileName = "PopupsColorsDictionary", menuName = "PopupsColorsDictionary")]
    public class PopupsColorsDictionary : ScriptableObject
    {
        [SerializeField, TabGroup("Numbers")] private Color defaultColor;
        [SerializeField, TabGroup("Numbers")] private Color shamanCritColor;
        [SerializeField, TabGroup("Numbers")] private Color enemyAutoAttackColor;
        [SerializeField, TabGroup("Numbers")] private Color enemyCritAutoAttackColor;
        [SerializeField, TabGroup("Numbers")] private Color healColor;

        [SerializeField, TabGroup("Status Effects")] private List<StatusEffectTypeVisualData> statusEffectsVisuals;

        public Color DefaultColor { get => defaultColor; }
        public Color ShamanCritColor { get => shamanCritColor; }
        public Color EnemyAutoAttackColor { get => enemyAutoAttackColor; }
        public Color EnemyCritAutoAttackColor { get => enemyCritAutoAttackColor; }
        public Color HealColor { get => healColor; }

        public StatusEffectTypeVisualData GetData(StatusEffectVisual givenVisual)
        {
            foreach (StatusEffectTypeVisualData data in statusEffectsVisuals)
            {

                if (data.StatusEffectVisual == givenVisual)
                {
                    return data;
                }
            }

            throw new Exception("Status Effect Type Not Found");
        }

        public StatusEffectTypeVisualData GetData(Color givenColor)
        {
            foreach (StatusEffectTypeVisualData data in statusEffectsVisuals)
            {
                if (data.Color == givenColor)
                {
                    return data;
                }
            }

            throw new Exception("Color Not Found");
        }
    }


    [Serializable]
    public class StatusEffectTypeVisualData
    {
        [SerializeField] private StatusEffectVisual _statusEffectVisual;
        [SerializeField] private Color color;
        [SerializeField] private string name;

        public StatusEffectVisual StatusEffectVisual { get => _statusEffectVisual; }
        public Color Color { get => color; }
        public string Name { get => name; }
    }
}