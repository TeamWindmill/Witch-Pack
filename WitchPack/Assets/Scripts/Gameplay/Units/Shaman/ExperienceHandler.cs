public class ExperienceHandler
{
    public int ShamanLevel { get; private set; } = 1;
    public int CurrentEnergy { get; private set; }
    public int AvailableExpPoints => ShamanLevel - _usedExpPoints;
    public int MaxExpToNextLevel => _shamanLevels[ShamanLevel-1];
    public bool HasExpPoints => AvailableExpPoints > 0;
    private int _usedExpPoints = 1;
    private int[] _shamanLevels;

    public ExperienceHandler(ExperienceConfig config)
    {
        _shamanLevels = config.LevelValues;
    }

    public void GainExp(int value)
    {
        
    }
    
}