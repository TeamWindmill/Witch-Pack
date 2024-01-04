using UnityEngine;

public class LevelHandler : MonoBehaviour
{
   [SerializeField] private Transform[] shamanSpawnPoints;
   [SerializeField] private ParticleSystem[] windEffectsParticleSystem;

   public Transform[] ShamanSpawnPoints => shamanSpawnPoints;

   public void TurnOffSpawnPoints()
   {
      foreach (var spawnPoint in shamanSpawnPoints)
      {
         spawnPoint.gameObject.SetActive(false);
      }
   }
}
