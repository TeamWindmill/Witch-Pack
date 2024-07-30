using System;

namespace Gameplay.Units.Stats
{
    public class BooleanStat
    {
        public event Action<bool> OnStatChange;
        public BooleanStatType StatType;

        public bool Value
        {
            get => Value;
            set
            {
                OnStatChange?.Invoke(value);
                Value = value;
            }
        }

        public BooleanStat(BooleanStatType statType, bool value)
        {
            StatType = statType;
            Value = value;
        }
    }

    public enum BooleanStatType
    {
    
    }
}