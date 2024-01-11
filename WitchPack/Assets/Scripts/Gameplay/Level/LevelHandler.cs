using System;
using UnityEngine;
using NavMeshPlus.Components;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] private Transform[] shamanSpawnPoints;
    [SerializeField] private CustomPath[] paths;
    [SerializeField] private ParticleSystem[] windEffectsParticleSystem;
    [SerializeField] private NavMeshSurface navMeshSurface;
    [SerializeField] private WaveHandler waveHandler;

   public ParticleSystem[] WindEffectsParticleSystem => windEffectsParticleSystem;

   public Transform[] ShamanSpawnPoints => shamanSpawnPoints;

   private bool _tempSlowMotion; //TEMP
   public void Init(Action<LevelHandler> onLevelLoad)
   {
      navMeshSurface.BuildNavMeshAsync();//bakes navmesh
      waveHandler.Init();
      onLevelLoad?.Invoke(this);
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.Space)) //TEMP
      {
         if(!_tempSlowMotion)
            SlowMotionManager.Instance.StartSlowMotionEffects();
         else
            SlowMotionManager.Instance.EndSlowMotionEffects();
         _tempSlowMotion = !_tempSlowMotion;
      }
   }

   public void TurnOffSpawnPoints()
   {
      foreach (var spawnPoint in shamanSpawnPoints)
      {
         spawnPoint.gameObject.SetActive(false);
      }
   }

    public CustomPath[] Paths { get => paths;}
    public WaveHandler WaveHandler { get => waveHandler; }
}
