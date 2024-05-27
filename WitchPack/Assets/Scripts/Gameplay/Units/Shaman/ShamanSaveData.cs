using System;
using System.Collections.Generic;

[Serializable]
public class ShamanSaveData
{
    public ShamanConfig Config;
    public ExperienceHandler ExperienceHandler;
    public List<AbilityUpgrade> AbilityUpgrades = new();
    public ShamanSaveData(ShamanConfig config)
    {
        Config = config;
        ExperienceHandler = new ExperienceHandler(config.ExperienceConfig);
    }
}