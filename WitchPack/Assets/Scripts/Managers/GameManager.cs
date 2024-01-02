using System;
using UnityEngine;


public class GameManager : MonoSingleton<GameManager>
{
    public static ISceneHandler SceneHandler { get; private set; }

    [SerializeField] private SceneHandler _sceneHandler;
    [SerializeField] private PoolManager poolManager;

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

    public PoolManager PoolManager { get => poolManager; }

    private void Awake()
    {
        base.Awake();
        if (SceneHandler == null)
            SceneHandler = _sceneHandler;

        CameraHandler = FindObjectOfType<CameraHandler>(); //May need to change 

    }

    void Start()
    {
        //SceneHandler.LoadScene(SceneType.MainMenu);
    }

    #region Test

    [ContextMenu("LoadMap")]
    public void LoadScene()
    {
        SceneHandler.LoadScene(SceneType.Map);
    }


    #endregion

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
}