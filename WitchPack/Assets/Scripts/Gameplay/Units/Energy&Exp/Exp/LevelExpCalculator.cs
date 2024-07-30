namespace Gameplay.Units.Energy_Exp.Exp
{
    public static class LevelExpCalculator
    {
        public static int CalculateExpGainedFromLevel(ExpCalculatorConfig calculatorConfig, EndLevelStats levelStats)
        {
            var totalExp = 0;
            if (levelStats.Completed)
            {
                totalExp += calculatorConfig.CompletionExp;
                totalExp += calculatorConfig.WaveCompletedExp;
                if (levelStats.FirstTime) totalExp += calculatorConfig.FirstTimeExp;
            }
            else
            {
                totalExp += (int)(calculatorConfig.WaveCompletedExp * levelStats.WavesCompletedPercentage);
            }
        
            totalExp += calculatorConfig.CoreRemainingHealthExp * levelStats.CoreRemainingHPPercentage;
        
            return (int)(totalExp * levelStats.ExpMultiplier);
        }
    }

    public readonly struct EndLevelStats
    {
        public readonly bool Completed;
        public readonly bool FirstTime;
        public readonly int CoreRemainingHPPercentage;
        public readonly float WavesCompletedPercentage;
        public readonly float ExpMultiplier;

        public EndLevelStats(bool completed, bool firstTime, int coreRemainingHp, float wavesCompletedPercentage,float expMultiplier = 1)
        {
            Completed = completed;
            FirstTime = firstTime;
            CoreRemainingHPPercentage = coreRemainingHp;
            WavesCompletedPercentage = wavesCompletedPercentage;
            ExpMultiplier = expMultiplier;
        }
    }
}