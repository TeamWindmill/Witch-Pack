
public static class LevelExpCalculator
{
    public static int CalculateExpGainedFromLevel(ExpCalculatorConfig calculatorConfig, EndLevelData levelData)
    {
        var totalExp = 0;
        if (levelData.Completed)
        {
            totalExp += calculatorConfig.CompletionExp;
            if (levelData.FirstTimeReward) totalExp += calculatorConfig.FirstTimeExp;
        }
        totalExp += calculatorConfig.CoreRemainingHealthExp * levelData.CoreRemainingHPPercentage;
        totalExp += (int)(calculatorConfig.WaveCompletedExp * levelData.WavesCompletedPercentage);
        
        return totalExp;
    }
}

public readonly struct EndLevelData
{
    public readonly bool Completed;
    public readonly bool FirstTimeReward;
    public readonly int CoreRemainingHPPercentage;
    public readonly float WavesCompletedPercentage;

    public EndLevelData(bool completed, bool firstTimeReward, int coreRemainingHp, float wavesCompletedPercentage)
    {
        Completed = completed;
        FirstTimeReward = firstTimeReward;
        CoreRemainingHPPercentage = coreRemainingHp;
        WavesCompletedPercentage = wavesCompletedPercentage;
    }
}