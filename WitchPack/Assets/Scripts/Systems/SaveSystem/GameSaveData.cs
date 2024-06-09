using System.Collections.Generic;

public class GameSaveData
{
    public List<ShamanSaveData> ShamanRoster;
    public MapNode[] MapNodes; 

    public GameSaveData()
    {
    }

    public GameSaveData(List<ShamanSaveData> shamanRoster)
    {
        ShamanRoster = shamanRoster;
    } 
}