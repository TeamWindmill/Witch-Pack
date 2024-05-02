using System;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoSingleton<GameManager>
{
    public bool TutorialPlayed;
    public bool[] LevelsCompleted = new bool[3];
    public List<ShamanConfig> ShamanRoster => _shamanRoster;

    public static ISceneHandler SceneHandler { get; private set; }
    public LevelConfig CurrentLevelConfig { get; private set; }

    [SerializeField] private SceneHandler _sceneHandler;
    [SerializeField] private List<ShamanConfig> _shamanRoster;

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
        private set => _cameraHandler = value;
    }


    protected override void Awake()
    {
        base.Awake();


        if (SceneHandler == null)
            SceneHandler = _sceneHandler;

        CameraHandler = FindObjectOfType<CameraHandler>(); //May need to change 
    }

    void Start()
    {
        SceneHandler.LoadScene(SceneType.MainMenu);
    }

    public void SetLevelConfig(LevelConfig levelConfig)
    {
        CurrentLevelConfig = levelConfig;
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
