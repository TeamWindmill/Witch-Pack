using System;
using UnityEngine;

[Serializable]
public struct ExpCalculatorConfig 
{
    [SerializeField] private int _completionExp;
    [SerializeField] private int _firstTimeExp;
    [SerializeField] private int _waveCompletedExp;
    [SerializeField] private int _coreRemainingHealthExp;

    public int CompletionExp => _completionExp;
    public int FirstTimeExp => _firstTimeExp;
    public int WaveCompletedExp => _waveCompletedExp;
    public int CoreRemainingHealthExp => _coreRemainingHealthExp;
}