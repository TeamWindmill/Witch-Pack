using UnityEngine;

public class Enemy : BaseUnit
{
    [SerializeField] private EnemyConfig enemyConfig;
    [SerializeField] private ShamanTargeter shamanTargeter;
    [SerializeField] private CustomPath givenPath;
    //testing 
    private int pointIndex = 0;

    public override StatSheet BaseStats => enemyConfig.BaseStats;

    public override void Init(BaseUnitConfig givenConfig)
    {
        enemyConfig = givenConfig as EnemyConfig;
        base.Init(givenConfig);
        shamanTargeter.SetRadius(Stats.BonusRange);
        Movement.SetDest(givenPath.Waypoints[pointIndex].position);
        Movement.OnDestenationReached += SetNextDest;
    }

    public void SetPath(CustomPath path)
    {
        givenPath = path;
    }


    private void SetNextDest(Vector3 pos)
    {
        pointIndex++;
        if (givenPath.Waypoints.Count <= pointIndex)//if reached the end of the path target nexus 
        {
            gameObject.SetActive(false);
        }
        else
        {
            Movement.SetDest(givenPath.Waypoints[pointIndex].position);
        }

    }

    public EnemyConfig EnemyConfig { get => enemyConfig; }
    public ShamanTargeter ShamanTargeter { get => shamanTargeter; }
}
