using System.Collections.Generic;

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