using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private LevelConfig _levelConfig;
    
    public void Init(LevelConfig levelConfig)
    {
        _levelConfig = levelConfig;
    }
}
