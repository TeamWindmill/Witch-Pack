
public static class LevelExpCalculator
{
    public static int CalculateExpGainedFromLevel(ExpCalculatorConfig calculatorConfig, EndLevelStats levelStats)
    {
        var totalExp = 0;
        if (levelStats.Completed)
        {
            totalExp += (int)(calculatorConfig.CompletionExp * levelStats.ExpMultiplier);
            totalExp += calculatorConfig.WaveCompletedExp;
            if (levelStats.FirstTimeReward) totalExp += (int)(calculatorConfig.FirstTimeExp * levelStats.FirstTimeExpMultiplier);
        }
        else
        {
            totalExp += (int)(calculatorConfig.WaveCompletedExp * levelStats.WavesCompletedPercentage);
        }
        
        totalExp += calculatorConfig.CoreRemainingHealthExp * levelStats.CoreRemainingHPPercentage;
        
        return totalExp;
    }
}

public readonly struct EndLevelStats
{
    public readonly bool Completed;
    public readonly bool FirstTimeReward;
    public readonly int CoreRemainingHPPercentage;
    public readonly float WavesCompletedPercentage;
    public readonly float ExpMultiplier;
    public readonly float FirstTimeExpMultiplier;

    public EndLevelStats(bool completed, bool firstTimeReward, int coreRemainingHp, float wavesCompletedPercentage,float expMultiplier = 1,float firstTimeExpMultiplier = 1)
    {
        Completed = completed;
        FirstTimeReward = firstTimeReward;
        CoreRemainingHPPercentage = coreRemainingHp;
        WavesCompletedPercentage = wavesCompletedPercentage;
        ExpMultiplier = expMultiplier;
        FirstTimeExpMultiplier = firstTimeExpMultiplier;
    }
}