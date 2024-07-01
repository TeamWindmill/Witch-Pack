using System;
using UnityEngine;


public class GameManager : MonoSingleton<GameManager>
{
    public LevelConfig CurrentLevelConfig { get; private set; }
    public static ISceneHandler SceneHandler { get; private set; }
    public ShamansManager ShamansManager => _shamansManager;

    public static GameSaveData SaveData;
    public bool TutorialPlayed;

    [SerializeField] private ShamansManager _shamansManager;
    [SerializeField] private SceneHandler _sceneHandler;

    private CameraHandler _cameraHandler;

    public CameraHandler CameraHandler
    {
        get
        {
            if (_cameraHandler != null) return _cameraHandler;

            if (Camera.main != null)
                _cameraHandler = FindObjectOfType<CameraHandler>();
            else
                throw new Exception("Can not find a valid camera");

            return _cameraHandler;
        }
    }

    protected override void Awake()
    {
        base.Awake();

        if (SceneHandler == null)
            SceneHandler = _sceneHandler;

        SaveData = LoadDataFromSave(); //need to load save from file
        _shamansManager.Init(SaveData);
        UIManager.Init();
    }

    void Start()
    {
        SceneHandler.LoadScene(SceneType.MainMenu);
    }

    public void SetLevelConfig(LevelConfig levelConfig)
    {
        CurrentLevelConfig = levelConfig;
    }

    private GameSaveData LoadDataFromSave() //this is temp need to connect to a save system
    {
        return new GameSaveData();
    }

    private void OnValidate()
    {
        if (_sceneHandler == null)
            _sceneHandler = FindObjectOfType<SceneHandler>();
    }

    private void OnMouseDown()
    {
        Screen.lockCursor = true;
    }

    public void Quit()
    {
        Application.Quit();
    }
}