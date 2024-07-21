using System;
using UnityEngine;


public class GameManager : MonoSingleton<GameManager>
{
    public static ISceneHandler SceneHandler { get; private set; }
    public LevelConfig CurrentLevelConfig { get; private set; }

    [SerializeField] private SceneHandler _sceneHandler;
    //[SerializeField] private PoolManager poolManager;

    // [SerializeField] private BaseUnitConfig shamanConf;
    // [SerializeField] private BaseUnitConfig enemyConf;
    // [SerializeField] private Shaman testShaman;
    // [SerializeField] private Enemy[] enemies;

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

    // public PoolManager PoolManager
    // {
    //     get => poolManager;
    // }

    void Start()
    {
        SceneHandler.LoadScene(SceneType.MainMenu);
        //InitUnits();
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
        //lock the cursor inside the screen
        Screen.lockCursor = true;
    }

    public void Quit()
    {
        Application.Quit();
    }

    // private void InitUnits()
    // {
    //     foreach (var item in enemies)
    //     {
    //         item.Init(enemyConf);
    //     }
    //     testShaman.Init(shamanConf);
    // }

}
