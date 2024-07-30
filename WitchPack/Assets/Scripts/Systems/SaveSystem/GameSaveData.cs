using System.Collections.Generic;
using Gameplay.Units.Shaman;
using Map;

namespace Systems.SaveSystem
{
    public class GameSaveData
    {
        public List<ShamanSaveData> ShamanRoster;
        public MapNode[] MapNodes; 
        public LevelSaveData[] LevelSaves; 
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
}