using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "WaveData")]
public class WaveData : MonoBehaviour
{
    private float spwanInterval;
    private float waveInterval;
    private EnemySpawnData[] spawnData;

    public EnemySpawnData[] SpawnData { get => spawnData; }
    public float WaveInterval { get => waveInterval; }
    public float SpwanInterval { get => spwanInterval; }
}

public struct EnemySpawnData
{
    public int Amount;
    public EnemyConfig enemyType;

}
