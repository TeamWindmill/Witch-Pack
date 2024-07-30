using System;
using Gameplay.Units.Stats;
using UnityEngine;

namespace Gameplay.Units.Damage_System
{
    public class DamageHandler
    {
        public Action<Damageable,DamageHandler> OnKill;
        public bool HasPopupColor => hasPopupColor;
        public Color PopupColor => popupColor;

        private Stat _finalDamage;
        private bool hasPopupColor;
        private Color popupColor;
        private bool _armorReduction;
        private float _armorReductionValue;
    

        public DamageHandler(float baseAmount)
        {
            _finalDamage = new Stat(StatType.BaseDamage, baseAmount);
        }


        public void AddMultiplierMod(float mod)
        {
            _finalDamage.AddMultiplier(mod);
        }


        public void AddFlatMod(int flatMod)
        {
            _finalDamage.AddModifier(flatMod);
        }

        public int GetDamage()
        {
            if (_armorReduction)
            {
                float damageReductionModifier = 100f / (_armorReductionValue + 100f);
                return (int)(_finalDamage.IntValue * damageReductionModifier);
            }
        
            return _finalDamage.IntValue;
        }

        public void SetPopupColor(Color color)
        {
            hasPopupColor = true;
            popupColor = color;
        }

        public void SetNoPopupColor()
        {
            hasPopupColor = false;
        }

        public void ApplyArmorReduction(int armorValue)
        {
            _armorReduction = true;
            _armorReductionValue = armorValue;
        }
    }
}
