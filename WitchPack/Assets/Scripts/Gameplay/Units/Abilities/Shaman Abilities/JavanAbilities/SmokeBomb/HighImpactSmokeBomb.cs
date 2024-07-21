public class HighImpactSmokeBomb : SmokeBomb
{
    private HighImpactSO Config;
    public HighImpactSmokeBomb(HighImpactSO config, BaseUnit owner) : base(config, owner)
    {
        Config = config;
    }

    protected override bool Cast(BaseUnit caster, IDamagable target)
    {
        HighImpactSmokeBombMono highImpact = LevelManager.Instance.PoolManager.HighImpactPool.GetPooledObject();
        highImpact.transform.position = target.GameObject.transform.position;
        highImpact.gameObject.SetActive(true);
        highImpact.SpawnBomb(this, caster);
        return true;
    }
}