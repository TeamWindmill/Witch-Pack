using System;
using UnityEngine;

namespace Gameplay.Units.Energy_Exp.Exp
{
    [Serializable]
    public struct ExpCalculatorConfig 
    {
        [SerializeField] private int _firstTimeExp;
        [SerializeField] private int _completionExp;
        [SerializeField] private int _waveCompletedExp;
        [SerializeField] private int _coreRemainingHealthExp;

        public int FirstTimeExp => _firstTimeExp;
        public int CompletionExp => _completionExp;
        public int WaveCompletedExp => _waveCompletedExp;
        public int CoreRemainingHealthExp => _coreRemainingHealthExp;
    }
}