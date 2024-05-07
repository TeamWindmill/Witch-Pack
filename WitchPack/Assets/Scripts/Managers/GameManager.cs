using System;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;


public class GameManager : MonoSingleton<GameManager>
{
    public LevelConfig CurrentLevelConfig { get; private set; }
    public static ISceneHandler SceneHandler { get; private set; }
    public GameSaveData SaveData { get; private set; }
    public ShamansManager ShamansManager => _shamansManager;

    public bool TutorialPlayed;
    public bool[] LevelsCompleted = new bool[3]; //temp

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

        LoadDataFromSave(SaveData); //need to load save from file
        _shamansManager.Init(SaveData);
    }
    void Start()
    {
        SceneHandler.LoadScene(SceneType.MainMenu);
    }

    public void SetLevelConfig(LevelConfig levelConfig)
    {
        CurrentLevelConfig = levelConfig;
    }

    private void LoadDataFromSave(GameSaveData saveData) //this is temp need to connect to a save system
    {
        if (saveData == null)
        {
            SaveData = new GameSaveData();
        }
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
