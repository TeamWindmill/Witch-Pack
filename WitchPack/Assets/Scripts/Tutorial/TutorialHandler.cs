using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class TutorialHandler : MonoSingleton<TutorialHandler>
{
    [TabGroup("Data")][SerializeField] public TutorialData MovementData;
    [TabGroup("Data")][SerializeField] public TutorialData PowerStructureData;
    [TabGroup("Data")][SerializeField] public TutorialData UpgradeData;

    [TabGroup("Components")][SerializeField] TextMeshProUGUI _tutorialText;
    [TabGroup("Components")][SerializeField] VideoPlayer _videoPlayer;
    [TabGroup("Components")][SerializeField] GameObject _tutorialCanvas;

    [SerializeField] private CanvasLerper _canvasLerper;

    private bool _isActive;
    [TabGroup("Timings")][SerializeField]private float _clickToCancelDelay = 1;
    [TabGroup("Timings")][SerializeField]private float _levelStartDelay = 3;
    [TabGroup("Timings")][SerializeField]private float _onLevelUpTutorialDelay = 2;

    private void Start()
    {
        _canvasLerper.SetStartValue();
    }

    public void LevelStart(LevelHandler level)
    {
#if UNITY_EDITOR
        return;
#endif
        if (GameManager.Instance.TutorialPlayed) return;
        if(!level.Config.ShowTutorial) return;

        TimerManager.AddTimer(_levelStartDelay, PlayMovementTutorial);
    }

    public void PlayMovementTutorial()
    {
        PlayTutorialVideo(MovementData);
        GAME_TIME.Pause();
        GameManager.Instance.TutorialPlayed = true;
        TimerManager.AddTimer(_clickToCancelDelay, () => _isActive = true);
        LevelManager.Instance.OnLevelStart -= LevelStart;
        LevelManager.Instance.ShamanParty[0].Movement.OnDestinationReached += PlayPowerStructuresTutorial;

    }
    public void PlayPowerStructuresTutorial()
    { 
        PlayTutorialVideo(PowerStructureData);
        GAME_TIME.Pause();
        TimerManager.AddTimer(_clickToCancelDelay, () => _isActive = true);
        LevelManager.Instance.ShamanParty[0].Movement.OnDestinationReached -= PlayPowerStructuresTutorial;
        LevelManager.Instance.ShamanParty[0].EnergyHandler.OnShamanLevelUp += OnLevelUp;
    }

    private void OnLevelUp(int obj)
    {
        TimerManager.AddTimer(_onLevelUpTutorialDelay, PlayUpgradeTutorial);
        LevelManager.Instance.ShamanParty[0].EnergyHandler.OnShamanLevelUp -= OnLevelUp;
    }

    public void PlayUpgradeTutorial()
    {
        PlayTutorialVideo(UpgradeData);
        GAME_TIME.Pause();
        TimerManager.AddTimer(_clickToCancelDelay, () => _isActive = true);
    }
    private void PlayTutorialVideo(TutorialData data)
    {
        _tutorialText.text = data.TutorialText;
        _videoPlayer.clip = data.VideoClip;
        _canvasLerper.StartTransitionEffect();
    }

    private void Update()
    {
        if (!_isActive) return;
        
        if (Input.anyKeyDown)
        {
            _canvasLerper.EndTransitionEffect();
            GAME_TIME.Play();
            _isActive = false;
        }
    }
}
