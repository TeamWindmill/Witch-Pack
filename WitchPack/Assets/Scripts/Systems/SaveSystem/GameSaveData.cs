using System.Collections.Generic;

public class GameSaveData
{
    public List<ShamanSaveData> ShamanRoster;
    public MapNode[] MapNodes; 
    public MapNode CurrentNode; 
    public int LastLevelCompletedIndex; 

    public GameSaveData()
    {
    }

    public GameSaveData(List<ShamanSaveData> shamanRoster)
    {
        ShamanRoster = shamanRoster;
    } 
}