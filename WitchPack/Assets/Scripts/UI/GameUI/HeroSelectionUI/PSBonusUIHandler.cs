using System;
using Gameplay.Units.Stats;
using UnityEngine;

namespace UI.GameUI.HeroSelectionUI
{
    public class PSBonusUIHandler : MonoBehaviour
    {
        [SerializeField] private PSBonusUI[] _bonusBlocks;

        public void Show(UnitStats stats)
        {
            foreach (var bonusBlock in _bonusBlocks)
            {
                var currentValue = stats.GetStatValue(bonusBlock.StatBonusType);
                var baseValue = stats.GetBaseStatValue(bonusBlock.StatBonusType);
                var bonusValue = MathF.Round((currentValue / baseValue - 1) * 100);
                if (bonusValue <= 0) continue;
                bonusBlock.Show(bonusValue);
            }
        }

        public void Hide()
        {
            foreach (var bonusBlock in _bonusBlocks)
            {
                bonusBlock.Hide();
            }
        }
    }
}