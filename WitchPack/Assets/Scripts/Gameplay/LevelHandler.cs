using System;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] private Transform[] shamanSpawnPoints;
    [SerializeField] private CustomPath[] paths;
    [SerializeField] private ParticleSystem[] windEffectsParticleSystem;


   public ParticleSystem[] WindEffectsParticleSystem => windEffectsParticleSystem;

   public Transform[] ShamanSpawnPoints => shamanSpawnPoints;

   private bool _tempSlowMotion; //TEMP

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

    

}
