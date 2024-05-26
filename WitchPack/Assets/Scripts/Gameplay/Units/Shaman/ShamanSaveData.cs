using System;
using System.Collections.Generic;

[Serializable]
public class ShamanSaveData
{
    public ShamanConfig Config;
    public ExperienceHandler ExperienceHandler;
    public List<AbilityStatUpgrade> AbilityUpgrades;
    public ShamanSaveData(ShamanConfig config)
    {
        Config = config;
    }
    public ShamanSaveData(Shaman shaman)
    {
        Config = shaman.ShamanConfig;
    }
}