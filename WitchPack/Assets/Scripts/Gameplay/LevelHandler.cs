using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] private Transform[] shamanSpawnPoints;
    [SerializeField] private CustomPath[] paths;
    [SerializeField] private ParticleSystem[] windEffectsParticleSystem;


    public Transform[] ShamanSpawnPoints => shamanSpawnPoints;

    public CustomPath[] Paths { get => paths;}

    public void TurnOffSpawnPoints()
    {
        foreach (var spawnPoint in shamanSpawnPoints)
        {
            spawnPoint.gameObject.SetActive(false);
        }
    }
}
