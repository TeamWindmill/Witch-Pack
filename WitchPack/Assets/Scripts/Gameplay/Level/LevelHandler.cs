using System;
using UnityEngine;
using NavMeshPlus.Components;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] private Transform[] shamanSpawnPoints;
    [SerializeField] private CustomPath[] paths;
    [SerializeField] private PowerStructure[] powerStructures;
    [SerializeField] private ParticleSystem[] windEffectsParticleSystem;
    [SerializeField] private NavMeshSurface navMeshSurface;
    [SerializeField] private WaveHandler waveHandler;
    [SerializeField] private CameraLevelSettings cameraLevelSettings;

   public ParticleSystem[] WindEffectsParticleSystem => windEffectsParticleSystem;
   public Transform[] ShamanSpawnPoints => shamanSpawnPoints;
   public CustomPath[] Paths { get => paths;}
   public WaveHandler WaveHandler { get => waveHandler; }

   private bool _tempSlowMotion; //TEMP
   public void Init()
   {
      GameManager.Instance.CameraHandler.SetCameraLevelSettings(cameraLevelSettings);
      GameManager.Instance.CameraHandler.ResetCamera();
      navMeshSurface.BuildNavMeshAsync();//bakes navmesh
      waveHandler.Init();
      foreach (var powerStructure in powerStructures)
      {
         powerStructure.Init();
      }
   }
   public void TurnOffSpawnPoints()
   {
      foreach (var spawnPoint in shamanSpawnPoints)
      {
         if(!spawnPoint.gameObject.activeSelf) return;
         spawnPoint.gameObject.SetActive(false);
      }
   }
    private void OnDrawGizmos()
    {
       Gizmos.DrawWireCube(Vector3.zero, cameraLevelSettings.CameraBorders);
    }
}
