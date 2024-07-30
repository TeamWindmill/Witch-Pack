using Configs;
using External_Assets.NavMeshPlus_master.NavMeshComponents.Scripts;
using Gameplay.Enviroment;
using Gameplay.PowerStructures;
using Gameplay.Units.Movement;
using Gameplay.Wave;
using Managers;
using Map;
using UnityEngine;

namespace Gameplay.Level
{
    public class LevelHandler : MonoBehaviour
    {
        public LevelConfig Config { get; private set; }
        public LevelSaveData LevelSaveData { get; private set; }
        public int ID { get; private set; }
        public EnviromentHandler EnviromentHandler => _EnviromentHandler;
        public Transform[] ShamanSpawnPoints => shamanSpawnPoints;
        public CoreTemple.CoreTemple CoreTemple => coreTemple;
        public CustomPath[] Paths => paths;
        public WaveHandler WaveHandler => waveHandler;
        public PowerStructure[] PowerStructures => powerStructures;

        [SerializeField] private Transform[] shamanSpawnPoints;
        [SerializeField] private CustomPath[] paths;
        [SerializeField] private CoreTemple.CoreTemple coreTemple;
        [SerializeField] private PowerStructure[] powerStructures;
        [SerializeField] private EnviromentHandler _EnviromentHandler;
        [SerializeField] private NavMeshSurface navMeshSurface;
        [SerializeField] private WaveHandler waveHandler;
        [SerializeField] private CameraLevelSettings cameraLevelSettings;

        private bool _tempSlowMotion; //TEMP

        public void Init(LevelConfig config, LevelSaveData levelSaveData)
        {
            Config = config;
            LevelSaveData = levelSaveData;
            ID = config.Number;
            GameManager.CameraHandler.SetCameraLevelSettings(cameraLevelSettings);
            GameManager.CameraHandler.ResetCamera();
            navMeshSurface.BuildNavMeshAsync(); //bakes navmesh
            coreTemple.Init();
            foreach (var powerStructure in powerStructures)
            {
                powerStructure.Init();
            }
        }

        public void StartLevel()
        {
            waveHandler.Init(Config);
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
            var startPos = cameraLevelSettings.OverrideCameraStartPos ? cameraLevelSettings.CameraStartPos : Vector3.zero;
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(Vector3.zero, cameraLevelSettings.CameraBorders);
            var cameraHeight = cameraLevelSettings.CameraStartZoom * 2;
            var cameraWidth = cameraHeight * GameManager.CameraHandler.MainCamera.aspect;
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(startPos, new Vector2(cameraWidth,cameraHeight));
        }

        private void OnValidate()
        {
            powerStructures = GetComponentsInChildren<PowerStructure>();
        }
    }
}