using UnityEngine;
using NavMeshPlus.Components;

public class LevelHandler : MonoBehaviour
{
    public LevelConfig Config { get; private set; }
    public int ID { get; private set; }
    public EnviromentHandler EnviromentHandler => _EnviromentHandler;
    public Transform[] ShamanSpawnPoints => shamanSpawnPoints;
    public CoreTemple CoreTemple => coreTemple;
    public CustomPath[] Paths => paths;
    public WaveHandler WaveHandler => waveHandler;
    public PowerStructure[] PowerStructures => powerStructures;
    public ExpCalculatorConfig ExpCalculatorConfig => expCalculatorConfig;

    [SerializeField] private Transform[] shamanSpawnPoints;
    [SerializeField] private CustomPath[] paths;
    [SerializeField] private CoreTemple coreTemple;
    [SerializeField] private PowerStructure[] powerStructures;
    [SerializeField] private EnviromentHandler _EnviromentHandler;
    [SerializeField] private NavMeshSurface navMeshSurface;
    [SerializeField] private WaveHandler waveHandler;
    [SerializeField] private CameraLevelSettings cameraLevelSettings;
    [SerializeField] private ExpCalculatorConfig  expCalculatorConfig;
    
    private bool _tempSlowMotion; //TEMP

    public void Init(LevelConfig config)
    {
        Config = config;
        ID = config.Number;
        GameManager.Instance.CameraHandler.SetCameraLevelSettings(cameraLevelSettings);
        GameManager.Instance.CameraHandler.ResetCamera();
        navMeshSurface.BuildNavMeshAsync(); //bakes navmesh
        coreTemple.Init();
        foreach (var powerStructure in powerStructures)
        {
            powerStructure.Init();
        }
    }

    public void StartLevel()
    {
        waveHandler.Init();

    }

    public void TurnOffSpawnPoints()
    {
        foreach (var spawnPoint in shamanSpawnPoints)
        {
            if (!spawnPoint.gameObject.activeSelf) continue;
            spawnPoint.gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(Vector3.zero, cameraLevelSettings.CameraBorders);
    }

    private void OnValidate()
    {
        powerStructures = GetComponentsInChildren<PowerStructure>();
    }
}