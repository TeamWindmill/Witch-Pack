using UnityEngine;

[CreateAssetMenu(menuName = "LevelExperienceConfig",fileName = "LevelExperienceConfig")]
public class ExpCalculatorConfig : ScriptableObject
{
    [SerializeField] private int _completionExp;
    [SerializeField] private int _firstTimeExp;
    [SerializeField] private int _waveCompletedExp;
    [SerializeField] private int _expPerCoreRemainingHealth;

    public int CompletionExp => _completionExp;
    public int FirstTimeExp => _firstTimeExp;
    public int WaveCompletedExp => _waveCompletedExp;
    public int ExpPerCoreRemainingHealth => _expPerCoreRemainingHealth;
}