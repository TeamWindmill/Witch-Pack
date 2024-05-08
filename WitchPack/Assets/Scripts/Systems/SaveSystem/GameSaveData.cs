using System;
using System.Collections.Generic;

[Serializable]
public class GameSaveData
{
    public List<ShamanSaveData> ShamanRoster;

    public GameSaveData()
    {
    }

    public GameSaveData(List<ShamanSaveData> shamanRoster)
    {
        ShamanRoster = shamanRoster;
    } 
}