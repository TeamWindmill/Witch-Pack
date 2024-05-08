using System;

[Serializable]
public class ShamanSaveData
{
    public ShamanConfig Config;
    
    public ShamanSaveData(ShamanConfig config)
    {
        Config = config;
    }
    public ShamanSaveData(Shaman shaman)
    {
        Config = shaman.ShamanConfig;
    }
}